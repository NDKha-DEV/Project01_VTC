using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model;
using Spectre.Console;
using UI;

public static class BookingController
{
    public static void ManagementBooking()
    {
        while (true)
        {
            Menu.BookingMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddBooking();
                    break;
                case "2":
                    UpdateBooking();
                    break;
                case "3":
                    DeleteBooking();
                    break;
                // case "4":
                //     ShowBookingList();
                //     AnsiConsole.MarkupLine("[bold green]Press any key to go back![/]");
                //     Console.ReadKey();
                //     break;
                case "4":
                    SearchBookingByCustomerID();
                    break;
                case "0":
                    Console.Clear();
                    return;
                    break;
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid choice! Please enter a valid choice![/]");
                    //Console.ReadKey();
                    break;
            }
        }
    }

    private static void AddBooking()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Add Booking");
        Booking booking = new Booking();
        int roomId;
        // Get room ID
        uI.GreenMessage("Enter room ID to add booking: ");
        while (!int.TryParse(Console.ReadLine(), out roomId) || roomId <= 0)
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter room ID to add booking: ");
        }
        int customerId;
        // Get customer ID
        uI.GreenMessage("Enter customer ID: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter customer ID: ");
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
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter room type: ");
            roomtype = Console.ReadLine();
        }
        
        DateTime? check_in_date;
        DateTime? check_out_date;
        // Enter check in date
        do
        {
            Console.WriteLine();
            var date_checkin = AnsiConsole.Ask<string>("[underline green]Enter check in date(YYYY/MM/DD): [/]");
            // Console.WriteLine();
            check_in_date = EnterDate(date_checkin);
            if (check_in_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            }
        } while (check_in_date == null);
        // Enter check out date
        do
        {
            Console.WriteLine();
            var date_checkout = AnsiConsole.Ask<string>("[underline green]Enter check out date(YYYY/MM/DD): [/]");
            Console.WriteLine();
            check_out_date = EnterDate(date_checkout);
            if (check_out_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            }
        } while (check_out_date == null);
        //Confirm
        AnsiConsole.Markup("[bold yellow]Are you sure you want to add booking? (Y/N): [/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                booking.Customer_id = customerId;
                booking.RoomId = roomId;
                booking.RoomType = roomtype;
                booking.CheckInDate = (DateTime)check_in_date;
                booking.CheckOutDate = (DateTime)check_out_date;
                db.Bookings.Add(booking);
                db.SaveChanges();
                uI.GreenMessage("Booking added successfully!");
                Console.WriteLine();
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Booking not added![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    private static void UpdateBooking()
    {

    }

    private static void DeleteBooking()
    {
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Delete Booking");
        int bookingId;
        // Get booking ID
        uI.GreenMessage("Enter booking ID to delete: ");
        while (!int.TryParse(Console.ReadLine(), out bookingId) || bookingId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.Markup("[underline green]Enter booking ID to delete: [/]");
        }
        using var db = new HotelContext();
        // Find booking by ID
        var booking = db.Bookings.FirstOrDefault(r => r.BookingId == bookingId);
        if (booking == null)
        {
            uI.RedMessage("Booking not found!");
            return;
        }
        AnsiConsole.Markup("[bold yellow]Are you sure you want to delete this booking? (Y/N): [/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                db.Bookings.Remove(booking);
                db.SaveChanges();
                uI.GreenMessage("Booking deleted successfully!");
                Console.WriteLine();
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Booking not deleted![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }

    }

    private static void ShowBookingList()
    {

    }

    private static void SearchBookingByCustomerID()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        Console.Clear();
        uI.Title("Search By Customer ID");
        int customerId;
        // Get customer ID
        uI.GreenMessage("Enter customer ID to search: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.Markup("[underline green]Enter customer ID to search: [/]");
        }
        var bookings = db.Bookings.Where(x => x.Customer_id == customerId).ToList();
        // Console.Clear();
        if (bookings.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]There are no booking with this ID![/]");
            AnsiConsole.Markup("[underline yellow]Press any key to go back![/]");
            Console.ReadKey();
            return;
        }
        int pageSize = 5;
        int currentPage = 1;
        int totalPages = (int)Math.Ceiling((double)bookings.Count / pageSize);

        while (true)
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"[bold green]Found {bookings.Count} bookings[/]");
            var table = new Table()
            {
                Title = new TableTitle($"[bold yellow]Page {currentPage}/{totalPages}[/]"),
            };
            table.AddColumn("[bold]ID[/]");
            table.AddColumn("[bold]Room ID[/]");
            table.AddColumn("[bold]Room type[/]");
            table.AddColumn("[bold]Customer ID[/]");
            table.AddColumn("[bold]Check In Date[/]");
            table.AddColumn("[bold]Check Out Date[/]");
            Console.WriteLine();
            Console.WriteLine();

            for (int i = (currentPage - 1) *pageSize; i < currentPage * pageSize && i < bookings.Count; i++)
            {
                var booking = bookings[i];
                table.AddRow(
                    booking.BookingId.ToString(),
                    booking.RoomId.ToString(),
                    booking.RoomType,
                    booking.Customer_id.ToString(),
                    booking.CheckInDate.ToString(),
                    booking.CheckOutDate.ToString()
                );
            }
            table.Expand();
            AnsiConsole.Write(table);

            Console.WriteLine();
            AnsiConsole.MarkupLine("[bold]Press '[/][bold red]CRT + P[/][bold]' for previous page, '[/][bold red]CRT +N[/][bold]' for next page[/]");
            AnsiConsole.MarkupLine("[bold]Press [yellow]ESC[/] key to exit.[/]");

            var keyInfo = Console.ReadKey(true);
            if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
            {
                switch (keyInfo.Key)
                {
                    case ConsoleKey.P:
                        if (currentPage > 1)
                        {
                            currentPage--;
                            table.Rows.Clear();
                        }
                        break;
                    case ConsoleKey.N:
                        if (currentPage < totalPages)
                        {
                            currentPage++;
                            table.Rows.Clear();
                        }
                        break;
                }
            }
            else if (keyInfo.Key == ConsoleKey.Escape)
            {
                return;
            }
        }
    }

    private static DateTime? EnterDate(string date_checkin)
    {
        DateTime date;
        if (DateTime.TryParseExact(date_checkin, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out date))
        {
            // Tạo một đối tượng DateTime mới với ngày được parse và giờ là 14:00:00
            DateTime time = new DateTime(date.Year, date.Month, date.Day, 14, 0, 0);
            return time;
        }
        else
        {
            //AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            return null;
        }
    }
}