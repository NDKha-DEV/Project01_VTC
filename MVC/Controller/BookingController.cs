using System;
using System.Collections.Generic;
using System.Globalization;
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
                    CheckingRoomController.CheckingRoom();
                    // AddBooking();
                    break;
                case "2":
                    UpdateBooking();
                    break;
                // case "3":
                //     DeleteBooking();
                //     break;
                // case "4":
                //     ShowBookingList();
                //     AnsiConsole.MarkupLine("[bold green]Press any key to go back![/]");
                //     Console.ReadKey();
                //     break;
                case "3":
                    SearchBookingByCustomerID();
                    break;
                case "0":
                    Console.Clear();
                    return;
                    
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid choice! Please enter a valid choice![/]");
                    //Console.ReadKey();
                    break;
            }
        }
    }

    public static void AddBooking()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        AnsiConsole.Clear();
        uI.Title("Add Booking");
        Booking booking = new Booking();
        int roomId;
        int customerId;
        // Get customer ID
        uI.GreenMessage("Enter customer ID to add booking: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter customer ID to add booking: ");
        }
        var customer = db.Customers.FirstOrDefault(r => r.CustomerId == customerId);
        if (customer == null)
        {
            uI.RedMessage("Customer not found!");
            CustomerController.SearchingCustomer(); 
            AddBooking();
        }        
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
        
        DateTime? check_in_date;
        DateTime? check_out_date;
        // Enter check in date
        do
        {
            Console.WriteLine();
            var date_checkin = AnsiConsole.Ask<string>("[underline green]Enter check in date(dd/MM/yyyy): [/]");
            // Console.WriteLine();
            check_in_date = EnterDate(date_checkin);
            if (check_in_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            } else if (check_in_date < DateTime.Today)
            {
                AnsiConsole.MarkupLine("[red]Check in date must be in the future![/]");
                check_in_date = null;
            }
        } while (check_in_date == null);
        // Enter check out date
        do
        {
            Console.WriteLine();
            var date_checkout = AnsiConsole.Ask<string>("[underline green]Enter check out date(dd/MM/yyyy): [/]");
            Console.WriteLine();
            check_out_date = EnterDate(date_checkout);
            if (check_out_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            } else if (check_out_date <= check_in_date)
            {
                AnsiConsole.MarkupLine("[red]Check out date must be after check in date![/]");
                check_out_date = null;
            } else if ((check_out_date.Value - check_in_date.Value).TotalDays > 10)
            {
                AnsiConsole.MarkupLine("[red]The minimum stay must be 10 days![/]");
                check_out_date = null; 
            }
        } while (check_out_date == null);
        //Confirm
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
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
                AnsiConsole.Markup("[bold yellow]Do you want to add more? (Y/N): [/]");
                confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    AddBooking();
                }
                else if (confirm.ToUpper() == "N")
                {
                    ManagementBooking();
                    break;
                }
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Booking not added![/]");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to add more? (Y/N): [/]");
                confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    AddBooking();
                }
                else if (confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                }
                // uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    private static void UpdateBooking()
    {
        ConsoleUI uI = new ConsoleUI();
        AnsiConsole.Clear();
        // Menu.UpdateBillMenu();
        uI.ApplicationLogoBeforeLogin();
        using var db = new HotelContext();
        uI.GreenMessage("Enter Booking ID to update(Enter 0 to Exit): ");
        int bookingId;
        while (!int.TryParse(Console.ReadLine(), out bookingId))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer ID.[/]");
            uI.GreenMessage("Enter Booking ID to update(Enter 0 to Exit): ");
        } 
        if (bookingId == 0)
        {
            return;
        }
        // Find the booking ID
        var booking = db.Bookings.FirstOrDefault(u => u.BookingId == bookingId);
        if (booking == null)
        {
            AnsiConsole.MarkupLine($"[bold red]Booking with ID {bookingId} not found.[/]");
            AnsiConsole.MarkupLine("[yellow]Press any key to go back![/]");
            Console.ReadKey();
            return;
        }
        Menu.UpdateBookingMenu();
        uI.GreenMessage("Enter your choice: ");
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                UpdateCustomerID(booking);
                break;
            case "2":
                UpdateDate(booking);
                break;
            case "0":
                return;
                break;
            
        }
        db.SaveChanges();
    }

    private static void UpdateCustomerID(Booking booking)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("UpdateDescription");
        Console.WriteLine("");
        using var db = new HotelContext();
        // Get new customer ID
        int customerId;
        uI.GreenMessage("Enter new customer ID: ");
        while (!int.TryParse(Console.ReadLine(), out customerId) || customerId <= 0)
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter new customer ID: ");
        }
        var customer = db.Customers.FirstOrDefault(r => r.CustomerId == customerId);
        if (customer == null)
        {
            uI.RedMessage("Customer not found!");
            return;
        }
        // Confirm
        AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                booking.Customer_id = customerId;
                uI.GreenMessage("Booking updated successfully!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Booking not updated![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }

    private static void UpdateDate(Booking booking)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("UpdateDescription");
        Console.WriteLine("");
        using var db = new HotelContext();
        DateTime? check_in_date;
        DateTime? check_out_date;
        // Enter check in date
        do
        {
            Console.WriteLine();
            var date_checkin = AnsiConsole.Ask<string>("[underline green]Enter new check in date(dd/MM/yyyy): [/]");
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
            var date_checkout = AnsiConsole.Ask<string>("[underline green]Enter new check out date(dd/MM/yyyy): [/]");
            Console.WriteLine();
            check_out_date = EnterDate(date_checkout);
            if (check_out_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            }
        } while (check_out_date == null);

        // Confirm
        AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
        string confirm = Console.ReadLine();
        switch (confirm.ToUpper())
        {
            case "Y":
                booking.CheckInDate = (DateTime)check_in_date;
                booking.CheckOutDate = (DateTime)check_out_date;
                uI.GreenMessage("Booking updated successfully!");
                uI.PressAnyKeyToContinue();
                break;
            case "N":
                AnsiConsole.MarkupLine("[bold yellow]Booking not updated![/]");
                uI.PressAnyKeyToContinue();
                break;
            default:
                uI.RedMessage("Invalid input! Please enter a valid input!");
                break;
        }
    }
    // private static void DeleteBooking()
    // {
    //     ConsoleUI uI = new ConsoleUI();
    //     Console.Clear();
    //     uI.Title("Delete Booking");
    //     int bookingId;
    //     // Get booking ID
    //     uI.GreenMessage("Enter booking ID to delete: ");
    //     while (!int.TryParse(Console.ReadLine(), out bookingId) || bookingId <= 0)
    //     {
    //         AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
    //         AnsiConsole.Markup("[underline green]Enter booking ID to delete: [/]");
    //     }
    //     using var db = new HotelContext();
    //     // Find booking by ID
    //     var booking = db.Bookings.FirstOrDefault(r => r.BookingId == bookingId);
    //     if (booking == null)
    //     {
    //         uI.RedMessage("Booking not found!");
    //         return;
    //     }
    //     AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
    //     string confirm = Console.ReadLine();
    //     switch (confirm.ToUpper())
    //     {
    //         case "Y":
    //             db.Bookings.Remove(booking);
    //             db.SaveChanges();
    //             uI.GreenMessage("Booking deleted successfully!");
    //             Console.WriteLine();
    //             uI.PressAnyKeyToContinue();
    //             break;
    //         case "N":
    //             AnsiConsole.MarkupLine("[bold yellow]Booking not deleted![/]");
    //             uI.PressAnyKeyToContinue();
    //             break;
    //         default:
    //             uI.RedMessage("Invalid input! Please enter a valid input!");
    //             break;
    //     }

    // }

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
            Console.WriteLine();
            AnsiConsole.Markup("[bold yellow]Do you want to search more? (Y/N): [/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                SearchBookingByCustomerID();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                Console.ReadKey();
                return;
            }
            // AnsiConsole.Markup("[underline yellow]Press any key to go back![/]");
            // Console.ReadKey();
            // return;
        } else {
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
    }

    private static DateTime? EnterDate(string date_checkin)
    {
        DateTime date;
        if (DateTime.TryParseExact(date_checkin, "dd/MM/yyyy", CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
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
