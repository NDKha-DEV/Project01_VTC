using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Spectre.Console;
using UI;

public static class CheckingRoomController
{
    public static void CheckingRoom()
    {
        using var db = new HotelContext();
        ConsoleUI uI = new ConsoleUI();
        AnsiConsole.Clear();
        // uI.Title("Checking Room");
        // Console.WriteLine("");
        // Get room type
        uI.GreenMessage("Enter room type to check(Double/Quad): ");
        string roomtype = Console.ReadLine();
        while (string.IsNullOrEmpty(roomtype) || (roomtype != "Double" && roomtype != "Quad"))
        {
            AnsiConsole.MarkupLine("[underline red]Invalid input! Please enter a valid input![/]");
            // Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            // Console.ReadKey();
            uI.GreenMessage("Enter room type to check(Double/Quad): ");
            roomtype = Console.ReadLine();
        }
        //var numbernight = AnsiConsole.Ask<int>("[bold green]Enter the number of night: [/]");
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
        
        var rooms = db.Rooms.Where(u => u.RoomType == roomtype).ToList(); // get all rooms
        var bookedRoomsId = db.Bookings.Where(x => (x.RoomType != roomtype) || (x.RoomType == roomtype && ((x.CheckInDate >= check_in_date && x.CheckInDate < check_out_date) || (x.CheckOutDate > check_in_date && x.CheckOutDate <= check_out_date) )))
                            .Select(x => x.RoomId)
                            .ToList();
        //var bookedRooms = db.Rooms.Where(r => bookedRoomsId.Contains(r.RoomId)).ToList();
        var unbookedRooms = GetUnBookedRooms(rooms, bookedRoomsId);
        if (unbookedRooms.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]There are no rooms available!,.[/]");
            AnsiConsole.MarkupLine("[bold yellow]Do you want to search again? (Y/N): ");
            string confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    CheckingRoom();
                }
                else if (confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                }
        }
        else
        {
            // AnsiConsole.MarkupLine($"[bold green]There are {unbookedRooms.Count} rooms.[/]");
            int pageSize = 5;
            int currentPage = 1;
            int totalPages = (int)Math.Ceiling((double)unbookedRooms.Count / pageSize);

            while (true)
            {
                Console.Clear();
                AnsiConsole.MarkupLine($"[bold green]Found {unbookedRooms.Count} rooms[/]");
                var table = new Table()
                {
                    Title = new TableTitle($"[bold yellow]Page {currentPage}/{totalPages}[/]"),
                };
                table.AddColumn("[bold]ID[/]");
                table.AddColumn("[bold]Room Number[/]");
                table.AddColumn("[bold]Room Type[/]");
                table.AddColumn("[bold]Description[/]");
                table.AddColumn("[bold]Price Per Night[/]");
                Console.WriteLine();
                Console.WriteLine();

                for (int i = (currentPage - 1) *pageSize; i < currentPage * pageSize && i < unbookedRooms.Count; i++)
                {
                    var room = unbookedRooms[i];
                    table.AddRow(
                        room.RoomId.ToString(),
                        room.RoomNumber,
                        room.RoomType,
                        room.Description,
                        room.PricePerNight.ToString()
                    );
                }
                table.Expand();
                AnsiConsole.Write(table);

                Console.WriteLine();
                AnsiConsole.MarkupLine("[bold]Press '[/][bold red]CTRL + B[/][bold]' for previous page, '[/][bold red]CRT +N[/][bold]' for next page[/]");
                AnsiConsole.MarkupLine("[bold]Press '[/][bold red]CTRL + M[/][bold]' to booking[/]");                
                AnsiConsole.MarkupLine("[bold]Press [yellow]ESC[/] key to exit.[/]");

                var keyInfo = Console.ReadKey(true);
                if ((keyInfo.Modifiers & ConsoleModifiers.Control) != 0)
                {
                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.B:
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
                        case ConsoleKey.M:
                            BookingController.AddBooking();
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
    private static List<Room> GetUnBookedRooms(List<Room> rooms, List<int> bookedRoomsId)
    {
        var unbookedRooms = rooms.Where(r => !bookedRoomsId.Contains(r.RoomId)).ToList();
        return unbookedRooms;
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
