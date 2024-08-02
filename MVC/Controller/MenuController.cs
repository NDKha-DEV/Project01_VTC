using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Controller;
using Microsoft.EntityFrameworkCore;
using Model;
using Spectre.Console;

public static class MenuController
{
    public static void DefaultMenuController()
    {
        while (true)
        {
            Menu.DefaultMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    RoomController.ManagementRoom();
                    break;
                case "2":
                    UserController.Register();
                    break;
                case "0":
                    UserController.Logout();
                    break;
                default:
                    Console.WriteLine("");
                    AnsiConsole.MarkupLine("[bold yellow]Function does not exist, press any key to continue[/]");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void MainMenuController()
    {
        while (true)
        {
            Menu.MainMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    CustomerController.ManagementCustomer();
                    break;
                // case "2":
                //     Console.Clear();
                //     CheckingRoomController.CheckingRoom();
                //     break;
                case "2":
                    Console.Clear();
                    BookingController.ManagementBooking();
                    break;
                case "3":
                    Console.Clear();
                    BillController.ManagmentBill();
                    break;
                // case "5":
                //     Console.Clear();
                //     UserController.EditProfileController();
                //     break;
                case "0":
                    //Console.Clear();
                    UserController.Logout();
                    break;
                default:
                    Console.WriteLine("");
                    AnsiConsole.MarkupLine("[bold yellow]Function does not exist, press any key to continue[/]");
                    Console.ReadKey();
                    Console.Clear();
                    break;
            }
        }
    }

    
}
