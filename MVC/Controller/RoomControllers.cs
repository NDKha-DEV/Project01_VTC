using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;
using Model;
using UI;

namespace Controller
{
    public static class RoomController
    {
        static ConsoleUI uI = new ConsoleUI();
        public static void ManagementRoom()
        {
            while (true)
            {
                AnsiConsole.Clear();
                Menu.RoomMenu();
                AnsiConsole.Markup("[bold green]Enter your choice: [/]");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddRoom();
                        break;
                    case "2":
                        UpdateRoom();
                        break;
                    // case "3":
                    //     DeleteRoom();
                    //     break;
                    case "3":
                        ShowRoomByRoomID();
                        break;
                    case "0":
                        return;
                    default:
                        AnsiConsole.MarkupLine("[bold red]Invalid choice, please enter a valid choice again![/]");
                        break;
                }
            }
        }
        // Controller add room
        public static void AddRoom()
        {
            try
            {
                AnsiConsole.Clear();
                uI.Title("Add Room");
                Console.WriteLine();
                using (var db = new HotelContext())
                {
                    Room room = new Room();

                    // Get room name
                    AnsiConsole.Markup("[bold green]Enter room number: [/]");
                    string roomnumber = Console.ReadLine();
                    while (string.IsNullOrEmpty(roomnumber))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room number.[/]");
                        AnsiConsole.Markup("[bold green]Enter room number: [/]");
                        roomnumber = Console.ReadLine();
                    }
                    room.RoomNumber = roomnumber;

                    // Get room price per night
                    AnsiConsole.Markup("[bold green]Enter room price per night: [/]");
                    decimal price;
                    while (!decimal.TryParse(Console.ReadLine(), out price))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid price.[/]");
                        AnsiConsole.Markup("[bold green]Enter room price: [/]");
                    }
                    room.PricePerNight = price;

                    // Get room description
                    AnsiConsole.Markup("[bold green]Enter room description: [/]");
                    string description = Console.ReadLine();
                    while (string.IsNullOrEmpty (description))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room description.[/]");
                        AnsiConsole.Markup("[bold green]Enter room description: [/]");
                        description = Console.ReadLine();
                    }
                    room.Description = description;

                    // Get room type
                    AnsiConsole.Markup("[bold green]Enter room type(Double/Quad): [/]");
                    string type = Console.ReadLine();
                    while (string.IsNullOrEmpty ((type)))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room type.[/]");
                        AnsiConsole.Markup("[bold green]Enter room type(Double/Quad): [/]");
                        type = Console.ReadLine();
                    }
                    room.RoomType = type;

                    // Confirm adding room
                    AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
                    string confirm = Console.ReadLine();
                    if (confirm.ToUpper() == "Y")
                    {
                        db.Rooms.Add(room);
                        db.SaveChanges();
                        AnsiConsole.MarkupLine("[bold green]Room added successfully!, press any key to go back![/]");
                        Console.ReadKey();
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[bold yellow]Room not added!, press any key to go back![/]");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
            }
        }

        // Controller update room
        public static void UpdateRoom()
        {
            try
            {
                AnsiConsole.Clear();
                uI.Title("Update Room");
                Console.WriteLine();
                using (var db = new HotelContext())
                {
                    int roomId;
                    AnsiConsole.Markup("[bold green]Enter room ID to update: [/]");
                    while (!int.TryParse(Console.ReadLine(), out roomId))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room ID.[/]");
                        AnsiConsole.Markup("[bold green]Enter room ID to update: [/]");
                    }
                    // Find the room by ID
                    var room = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                    if (room == null)
                    {
                        AnsiConsole.MarkupLine("[bold red]Room not found![/]");
                        Console.ReadKey();
                        return;
                    }
                    //Menu.RoomUpdateMenu(roomId);
                    AnsiConsole.Markup("[bold green]Enter your choice: [/]");
                    string choice =  Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            UpdateRoomNumber(room);
                            break;
                        case "2":
                            UpdateRoomPrice(room);
                            break;
                        case "3":
                            UpdateRoomDescription(room);
                            break;
                        case "4":
                            UpdateRoomType(room);
                            break;
                        // case "5":
                        //     UpdateRoomValiable(room);
                        //     break;
                        case "0":
                            break;
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
            }
        }

        private static void UpdateRoomNumber(Room room)
        {
            AnsiConsole.Clear();
            uI.Title("Update Room Number");
            Console.WriteLine();
            AnsiConsole.Markup("[bold green]Enter new room number: [/]");
            string roomnumber = Console.ReadLine();
            while (string.IsNullOrEmpty(roomnumber))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room number.[/]");
                AnsiConsole.Markup("[bold green]Enter new room number: [/]");
                roomnumber = Console.ReadLine();
            }
            // Confirm updating room number
            AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                room.RoomNumber = roomnumber;
                AnsiConsole.MarkupLine("[bold green]Room number updated successfully!, press any key to go back![/]");
                Console.ReadKey();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Room number not updated!, press any key to go back![/]");
                Console.ReadKey();
            }
        }

        private static void UpdateRoomPrice(Room room)
        {
            AnsiConsole.Clear();
            uI.Title("Update Room Price");
            Console.WriteLine();
            AnsiConsole.Markup("[bold green]Enter new room price per night: [/]");
            decimal price;
            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid price.[/]");
                AnsiConsole.Markup("[bold green]Enter new room price per night: [/]");
            }
            // Confirm updated room price
            AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                room.PricePerNight = price;
                AnsiConsole.MarkupLine("[bold green]Room price updated successfully!, press any key to go back![/]");
                Console.ReadKey();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Room price not updated!, press any key to go back![/]");
                Console.ReadKey();
            }
        }

        private static void UpdateRoomDescription(Room room)
        {
            AnsiConsole.Clear();
            uI.Title("Update Room Description");
            Console.WriteLine();
            AnsiConsole.Markup("[bold green]Enter new room description: [/]");
            string description = Console.ReadLine();
            while (string.IsNullOrEmpty(description))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room description.[/]");
                AnsiConsole.Markup("[bold green]Enter new room description: [/]");
                description = Console.ReadLine();
            }
            //Confirm updating room description
            AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N)[/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                room.Description = description;
                AnsiConsole.MarkupLine("[bold green]Room updated successfully!, press any key to go back![/]");
                Console.ReadKey();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Room description not updated!, press any key to go back![/]");
                Console.ReadKey();
            }
        }

        private static void UpdateRoomType(Room room)
        {
            AnsiConsole.Clear();
            uI.Title("Update Room Type");
            Console.WriteLine();
            AnsiConsole.Markup("[bold green]Enter new room type(Double/Quad): [/]");
            string type = Console.ReadLine();
            while (string.IsNullOrEmpty(type))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room type.[/]");
                AnsiConsole.Markup("[bold green]Enter new room type(Double/Quad): [/]");
                type = Console.ReadLine();
            }
            // Confirm update room type
            AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                room.RoomType = type;
                AnsiConsole.MarkupLine("[bold green]Room type updated successfully!, press any key to go back![/]");
                Console.ReadKey();
            }
            else if (confirm.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Room type not updated!, press any key to go back![/]");
                Console.ReadKey();
            }
        }

        // Controller delete room
        // public static void DeleteRoom()
        // {
        //     try
        //     {
        //         using (var db = new HotelContext())
        //         {
        //             Console.Clear();
        //             var Panel = new Panel("[bold green]Delete Room[/]");
        //             int roomId;
        //             AnsiConsole.MarkupLine("[bold green]Enter room ID to delete:[/]");
        //             while (!int.TryParse(Console.ReadLine(), out roomId))
        //             {
        //                 AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room ID.[/]");
        //                 AnsiConsole.MarkupLine("[bold green]Enter room ID to update:[/]");
        //             }
        //             // Find the room by ID
        //             var room = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
        //             if (room == null)
        //             {
        //                 AnsiConsole.MarkupLine("[bold red]Room not found![/]");
        //                 Console.ReadKey();
        //                 return;
        //             }
        //             AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to delete this room? (Y/N):[/]");
        //             string confirm = Console.ReadLine();
        //             if (confirm.ToUpper() == "Y")
        //             {
        //                 db.Rooms.Remove(room);
        //                 db.SaveChanges();
        //                 AnsiConsole.MarkupLine("[bold green]Room deleted successfully!, press any key to go back![/]");
        //                 Console.ReadKey();
        //             }
        //             else if (confirm.ToUpper() == "N")
        //             {
        //                 AnsiConsole.MarkupLine("[bold yellow]Room not deleted!, press any key to go back![/]");
        //                 Console.ReadKey();
        //             }
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
        //     }
        // }

        // Searching room
        public static Table ShowRoomByRoomID()
        {
            //try
            //{
                using (var db = new HotelContext())
                {
                    AnsiConsole.Clear();
                    uI.Title("Show Room By Room ID");
                    Console.WriteLine();
                    int roomId;
                    AnsiConsole.Markup("[bold green]Enter room ID to update:[/]");
                    while (!int.TryParse(Console.ReadLine(), out roomId))
                    {
                        AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid room ID.[/]");
                        AnsiConsole.MarkupLine("[bold green]Enter room ID to update:[/]");
                    }
                    // Find the room by ID
                    var room = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
                    if (room == null)
                    {
                        AnsiConsole.MarkupLine("[bold red]Room not found![/]");
                        Console.ReadKey();
                        return null;
                    }
                    var table = new Table();
                    table.AddColumn("ID");
                    table.AddColumn("RoomNumber");
                    table.AddColumn("Type");
                    table.AddColumn("PricePerNight");
                    table.AddColumn("Description");
                    table.AddRow(room.RoomId.ToString(), room.RoomNumber, room.RoomType, room.PricePerNight.ToString(), room.Description);
                    table.Expand();
                    return table;            
                }
            //}
            //catch (Exception ex)
            //{
                //AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
            //}                        
        }
    }
}
