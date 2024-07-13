using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model;
using Spectre.Console;

public static class CheckingRoomController
{
    public static void CheckingRoom()
    {
        var roomtype = AnsiConsole.Ask<string>("[bold green]Enter room type to check: [/]");
        //var numbernight = AnsiConsole.Ask<int>("[bold green]Enter the number of night: [/]");
        DateTime? check_in_date;
        DateTime? check_out_date;
        // Enter check in date
        do
        {
            var date_checkin = AnsiConsole.Ask<string>("[bold green]Enter date check in(YYYY/MM/DD): [/]");
            check_in_date = EnterDate(date_checkin);
            if (check_in_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            }
        } while (check_in_date == null);
        // Enter check out date
        do
        {
            var date_checkout = AnsiConsole.Ask<string>("[bold green]Enter date check out(YYYY/MM/DD): [/]");
            check_out_date = EnterDate(date_checkout);
            if (check_out_date == null)
            {
                AnsiConsole.MarkupLine("[red]Invalid input! Please enter a valid input![/]");
            }
        } while (check_out_date == null);
        
        using var db = new HotelContext();
        var rooms = db.Rooms.ToList(); // get all rooms
        var bookedRoomsId = db.Bookings.Where(x => x.RoomType == roomtype && ((x.CheckInDate >= check_in_date && x.CheckInDate < check_out_date) || (x.CheckOutDate > check_in_date && x.CheckOutDate <= check_out_date)))
                            .Select(x => x.RoomId)
                            .ToList();
        //var bookedRooms = db.Rooms.Where(r => bookedRoomsId.Contains(r.RoomId)).ToList();
        var unbookedRooms = GetUnBookedRooms(rooms, bookedRoomsId);
        if (unbookedRooms.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold yellow]There are no rooms available!, press any key to go back.[/]");
            Console.ReadKey();
        }
        else
        {
            AnsiConsole.MarkupLine("[bold green]List unbooked rooms ID in this time: [/]");
            foreach (var room in unbookedRooms)
            {
                Console.WriteLine(room.RoomId);
            }
            BillController.priceroom = unbookedRooms[0].PricePerNight;
            AnsiConsole.MarkupLine("[bold green]Press any key to go back![/]");
            Console.ReadKey();
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