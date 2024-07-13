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
            Console.Clear();
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
                case "3":
                    DeleteBill();
                    break;
                case "4":
                    SearchBillByCustomerID();
                    break;
                case "5":
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
            AnsiConsole.MarkupLine("[bold green]Enter customer ID to add bill: [/]");
        }
        var customer = db.Customers.FirstOrDefault(r => r.CustomerId == customerId);
        if (customer == null)
        {
            uI.RedMessage("Customer not found!");
            return;
        }        
        // Get room type
        uI.GreenMessage("Enter room type: ");
        string roomtype = Console.ReadLine();
        while (string.IsNullOrEmpty(roomtype))
        {
            uI.RedMessage("Invalid input! Please enter a valid input!");
            uI.GreenMessage("Enter room type: ");
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
        if (roomtype == "double")
        {
            priceroom = 600;
        }
        else if (roomtype == "quad")
        {
            priceroom = 1000;
        }
        var total_amout = numbernight * number_of_rooms * priceroom;
        //Confirm
        AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to add this bill? (Y/N):[/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                bill.Customer_id = customerId;
                bill.Description = description;
                bill.Total_amount = total_amout;
                db.SaveChanges();
                uI.GreenMessage("Bill added successfully!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Bill not added![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    private static void UpdateBill()
    {

    }

    private static void DeleteBill()
    {
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Delete Bill");
        int billId;
        // Get bill ID
        uI.GreenMessage("Enter bill ID to delete: ");
        while (!int.TryParse(Console.ReadLine(), out billId) || billId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter bill ID to delete: [/]");
        }
        using var db = new HotelContext();
        // Find bill by ID
        var bill = db.Bills.FirstOrDefault(r => r.BillId == billId);
        if (bill == null)
        {
            uI.RedMessage("Bill not found!");
            return;
        }
        AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to delete this bill? (Y/N):[/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                db.Bills.Remove(bill);
                db.SaveChanges();
                uI.GreenMessage("Bill deleted successfully!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Bill not deleted![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }

    }

    private static void SearchBillByCustomerID()
    {
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Search Bill By ID");
        // Get customer ID
        uI.GreenMessage("Enter customer ID to search: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.MarkupLine("[bold green]Enter customer ID to search: [/]");
        }
        using var db = new HotelContext();
        var bill = db.Bills.FirstOrDefault(r => r.Customer_id == customerId);
        if (bill == null)
        {
            uI.RedMessage("There are no bill with this ID!");
            return;
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
        }
    }

    private static void PayBill()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Search Bill By ID");
        // Get customer ID
        uI.GreenMessage("Enter customer ID to search: ");
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
            return;
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
        }
        // ConsoleUI uI = new ConsoleUI();
        // confirm
        AnsiConsole.Markup("[bold yellow]Are you sure you want to pay this bill? (Y/N): [/]");
        string confirm = Console.ReadLine();

        while (string.IsNullOrEmpty(confirm))
        {
            AnsiConsole.Markup("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            uI.PressAnyKeyToContinue();
            AnsiConsole.Markup("[bold yellow]Are you sure you want to pay this bill? (Y/N): [/]");
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
                AnsiConsole.Markup("[bold yellow]Are you sure you want to pay this bill? (Y/N): [/]");
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
                uI.GreenMessage("Bill paid successfully!!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Cancelled!![/]");
                uI.PressAnyKeyToContinue();
                break;
        }

    }

}