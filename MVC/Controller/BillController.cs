using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Spectre.Console;
using UI;

public static class BillController
{
    public static int customerId;
    public static decimal priceroom;
    public static void ManagmentBill()
    {
        while (true)
        {
            AnsiConsole.Clear();
            Menu.BillMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBill();
                    break;
                case "2":
                    UpdateBill();
                    break;
                // case "3":
                //     DeleteBill();
                //     break;
                case "3":
                    SearchBillByCustomerID();
                    break;
                case "4":
                    PayBill();
                    break;
                case "0":
                    Console.Clear();
                    return;
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid choice! Please enter a valid choice again![/]");
                    break;
            }
        }
    }

    private static void AddBill()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Add Bill");
        Bill bill = new Bill();
        // Get customer ID
        uI.GreenMessage("Enter customer ID to add bill: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            uI.GreenMessage("Enter customer ID to add bill: ");
        }
        var customer = db.Customers.FirstOrDefault(r => r.CustomerId == customerId);
        if (customer == null)
        {
            uI.RedMessage("Customer not found!");
            CustomerController.SearchingCustomer(); 
            AddBill();        
        }        
        // Get room type
        uI.GreenMessage("Enter room type(Double/Quad): ");
        string roomtype = Console.ReadLine();
        while (string.IsNullOrEmpty(roomtype) || (roomtype != "Double" && roomtype != "Quad"))
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            // Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter room type(Double/Quad): ");
            roomtype = Console.ReadLine();
        }
        // Get number night
        uI.GreenMessage("Enter number night: ");
        int numbernight;
        while (!int.TryParse(Console.ReadLine(), out numbernight) || numbernight <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter number night: [/]");
        }
        // Get number of rooms
        uI.GreenMessage("Enter number of rooms: ");
        int number_of_rooms;
        while (!int.TryParse(Console.ReadLine(), out number_of_rooms) || number_of_rooms <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter number of rooms: [/]");
        }
        var description = $"{roomtype}, {numbernight.ToString()} nights, {number_of_rooms.ToString()} rooms";
        var room = db.Rooms.Where(r => r.RoomType == roomtype).ToList();
        priceroom = room[0].PricePerNight;
        var total_amout = numbernight * number_of_rooms * priceroom;
        //Confirm
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                // bill.roomtype = roomtype;
                // bill.numberofrooms = number_of_rooms;
                // bill.numberofnights = numbernight;
                bill.Customer_id = customerId;
                bill.Description = description;
                bill.Billstatus = "Pending";
                bill.Total_amount = total_amout;
                db.Bills.Add(bill);
                db.SaveChanges();
                uI.GreenMessage("Bill added successfully!");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to add more? (Y/N): [/]");
                confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    AddBill();
                }
                else if (confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                }
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Bill not added![/]");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to add more? (Y/N): [/]");
                confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    AddBill();
                }
                else if (confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                }
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    private static void UpdateBill()
    {
        ConsoleUI uI = new ConsoleUI();
        AnsiConsole.Clear();
        // Menu.UpdateBillMenu();
        uI.ApplicationLogoBeforeLogin();
        using var db = new HotelContext();
        uI.GreenMessage("Enter Bill ID to update(Enter 0 to Exit): ");
        int billId;
        while (!int.TryParse(Console.ReadLine(), out billId))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer ID.[/]");
            uI.GreenMessage("Enter Bill ID to update: ");
        } 
        if (billId == 0)
        {
            return;
        }
        // Find the bill ID
        var bill = db.Bills.FirstOrDefault(u => u.BillId == billId);
        if (bill == null)
        {
            AnsiConsole.MarkupLine($"[bold red]Booking with ID {billId} not found.[/]");
            AnsiConsole.MarkupLine("[yellow]Press any key to go back![/]");
            Console.ReadKey();
            return;
        }
        Menu.UpdateBillMenu();
        uI.GreenMessage("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                UpdateDescription(bill);
                break;
            case "0":
                return;
                break;
            
        }
        db.SaveChanges();
        
    }

    private static void UpdateDescription(Bill bill)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("UpdateDescription");
        Console.WriteLine("");
        // Get room type
        uI.GreenMessage("Enter new room type: ");
        string roomtype = Console.ReadLine();
        while (string.IsNullOrEmpty(roomtype))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer name.[/]");
            uI.GreenMessage("Enter new room type: ");
            roomtype = Console.ReadLine();
        }
        // Get number night
        uI.GreenMessage("Enter new number night: ");
        int numbernight;
        while (!int.TryParse(Console.ReadLine(), out numbernight) || numbernight <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            uI.GreenMessage("Enter new number night: ");
        }
        // Get number of rooms
        uI.GreenMessage("Enter new number of rooms: ");
        int number_of_rooms;
        while (!int.TryParse(Console.ReadLine(), out number_of_rooms) || number_of_rooms <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            uI.GreenMessage("Enter new number of rooms: ");
        }
        var description = $"{roomtype}, {numbernight.ToString()} nights, {number_of_rooms.ToString()} rooms";
        using var db = new HotelContext();
        var room = db.Rooms.Where(r => r.RoomType == roomtype).ToList();
        priceroom = room[0].PricePerNight;
        decimal total_amout = numbernight * number_of_rooms * priceroom;

        // Confirm
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                bill.Description = description;
                bill.Total_amount = total_amout;
                uI.GreenMessage("Bill updated successfully!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Bill not updated![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    // private static void DeleteBill()
    // {
    //     ConsoleUI uI = new ConsoleUI();
    //     Console.Clear();
    //     uI.Title("Delete Bill");
    //     int billId;
    //     // Get bill ID
    //     uI.GreenMessage("Enter bill ID to delete: ");
    //     while (!int.TryParse(Console.ReadLine(), out billId) || billId <= 0)
    //     {
    //         AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
    //         AnsiConsole.MarkupLine("[bold green]Enter bill ID to delete: [/]");
    //     }
    //     using var db = new HotelContext();
    //     // Find bill by ID
    //     var bill = db.Bills.FirstOrDefault(r => r.BillId == billId);
    //     if (bill == null)
    //     {
    //         uI.RedMessage("Bill not found!");
    //         return;
    //     }
    //     AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to delete this bill? (Y/N):[/]");
    //     string confirm = Console.ReadLine();
    //     switch (confirm.ToUpper())
    //     {
    //         case "Y":
    //             db.Bills.Remove(bill);
    //             db.SaveChanges();
    //             uI.GreenMessage("Bill deleted successfully!");
    //             uI.PressAnyKeyToContinue();
    //             break;
    //         case "N":
    //             AnsiConsole.MarkupLine("[bold yellow]Bill not deleted![/]");
    //             uI.PressAnyKeyToContinue();
    //             break;
    //         default:
    //             uI.RedMessage("Invalid input! Please enter a valid input!");
    //             break;
    //     }

    // }

    private static void SearchBillByCustomerID()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        AnsiConsole.Clear();
        uI.Title("Search Bill By ID");
        // Get customer ID
        uI.GreenMessage("Enter customer ID to search(Enter 0 to Exit): ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId < 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter customer ID to search(Enter 0 to Exit): [/]");
        }
        if (customerId == 0)
        {
            return;
        }
        var bill = db.Bills.FirstOrDefault(r => r.Customer_id == customerId);
        if (bill == null)
        {
            uI.RedMessage("There are no bill with this ID!");
            Console.WriteLine();
            AnsiConsole.Markup("[bold yellow]Do you want to search more? (Y/N): [/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                SearchBillByCustomerID();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                Console.ReadKey();
                return;
            }
        }
        else
        {
            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("Total_Amount");
            table.AddRow(
                bill.BillId.ToString(),
                bill.Description,
                bill.Billstatus,
                bill.Total_amount.ToString()+" VND"
            );
            table.Expand();
            AnsiConsole.Write(table);
            uI.PressAnyKeyToContinue();
        }
    }

    private static void PayBill()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Pay Bill");
        // Get customer ID
        uI.GreenMessage("Enter customer ID to search bill: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter customer ID to search: [/]");
        }
        // using var db = new HotelContext();
        var bill = db.Bills.FirstOrDefault(r => r.Customer_id == customerId);
        if (bill == null)
        {
            uI.RedMessage("There are no bill with this ID!");
            Console.WriteLine();
            AnsiConsole.Markup("[bold yellow]Do you want to pay more? (Y/N): [/]");
            string Confirm = Console.ReadLine();
            if (Confirm.ToUpper() == "Y")
            {
                PayBill();
            }
            else if (Confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                // Console.ReadKey();
                return;
            }
        }
        else
        {
            var table = new Table();
            table.AddColumn("ID:");
            table.AddColumn("Description");
            table.AddColumn("Status");
            table.AddColumn("Total_Amount");
            table.AddRow(
                bill.BillId.ToString(),
                bill.Description,
                bill.Billstatus,
                bill.Total_amount.ToString()
            );
            table.Expand();
            AnsiConsole.Write(table);
            uI.PressAnyKeyToContinue();
        // }
        // ConsoleUI uI = new ConsoleUI();
        // confirm
        AnsiConsole.Markup("[bold yellow]Do you want to pay this bill? (Y/N): [/]");
        string confirm = Console.ReadLine();

        while (string.IsNullOrEmpty(confirm))
        {
            AnsiConsole.Markup("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            uI.PressAnyKeyToContinue();
            AnsiConsole.Markup("[bold yellow]Do you want to pay this bill? (Y/N): [/]");
            confirm = Console.ReadLine();
        }

        while (true)
        {
            if (confirm.ToUpper() == "Y" || confirm.ToUpper() == "N")
            {
                break;
            }
            else
            {
                AnsiConsole.Markup("[underline red]Invalid input! Please enter a valid input![/]");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                uI.PressAnyKeyToContinue();
                AnsiConsole.Markup("[bold yellow]Do you want to pay this bill? (Y/N): [/]");
                confirm = Console.ReadLine();
            }
        }

        switch (confirm.ToUpper())
        {
            case "Y":
                var idbill = AnsiConsole.Ask<int>("[bold green]Enter bill id to pay: [/]");
                bill = db.Bills.FirstOrDefault(b => b.BillId == idbill);
                if (bill == null)
                {
                    uI.RedMessage("Bill not found!");
                    //uI.PressAnyKeyToContinue();
                    return;
                }
                bill.Billstatus = "Paid";
                bill.Payment_date = DateTime.Now;
                db.SaveChanges();
                // DeleteBill();
                uI.GreenMessage("Bill paid successfully!!");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to pay more? (Y/N): [/]");
                string Confirm = Console.ReadLine();
                if (Confirm.ToUpper() == "Y")
                {
                    PayBill();
                }
                else if (Confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    // Console.ReadKey();
                    return;
                }                
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Cancelled!![/]");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to pay more? (Y/N): [/]");
                Confirm = Console.ReadLine();
                if (Confirm.ToUpper() == "Y")
                {
                    PayBill();
                }
                else if (Confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                    return;
                }
                break;
        }
        }
    }

}
