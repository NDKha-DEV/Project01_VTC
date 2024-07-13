using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Model;
using Spectre.Console;
using UI;

public static class UserController
{
    public static User user;

    // User login
    public static void Login()
    {
        bool loginSuccess = false;
        do
        {
            Console.Clear();
            //Menu.LoginMenu();
            ConsoleUI uI = new ConsoleUI();
            //uI.ApplicationLogoBeforeLogin();
            uI.Title("LOGIN");
            uI.GreenMessage("Input User name and password to LOGIN or input User Name = 0 to EXIT.");
            Console.WriteLine();

            using (var context = new HotelContext())
            {
                // Get login information from the ueser
                var username = AnsiConsole.Ask<string>("[bold Aqua]->[/] [bold Aqua]Username:[/] ");
                if (username == "0")
                {
                    break;
                }
                var password = AnsiConsole.Prompt(
                    new TextPrompt<string>("[bold Aqua]->[/] [bold Aqua]Password:[/] ")
                    .Secret());
                
                // Find the user in the database
                var user = context.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    AnsiConsole.Markup("[bold red]Username does not exist, input any key to go back![/]");
                    Console.ReadKey();
                }
                else
                {
                    // check the password
                    if (user.Password != password)
                    {
                        AnsiConsole.Markup("[bold red]Invalid password, input any key to go back![/]");
                        Console.ReadKey();
                    }
                    else
                    {
                        UserController.user = user;
                        loginSuccess = true;
                        MenuController.MainMenuController();
                    }
                }
            }
        } while (!loginSuccess);
    }

    //User registration
    public static void Register()
    {
        using var db = new HotelContext();
        var user = new User();
        ConsoleUI uI = new ConsoleUI();
        user.FullName = AnsiConsole.Ask<string>("[bold green]Enter full name: [/]");
        string email;
        User existingUser; // Declare the existingUser variable
        string username;
        do
        {
            email = AnsiConsole.Ask<string>("[bold green]Enter email: [/]");
            existingUser = db.Users.FirstOrDefault(u => u.Email == email);
            if (existingUser != null)
            {
                AnsiConsole.MarkupLine("[bold red]Email already exists![/]");
            }
            else if (!IsValidEmail(email))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid email format![/]");
            }
        } while (existingUser != null || !IsValidEmail(email));
        user.Email = email;
        user.Address = AnsiConsole.Ask<string>("[bold green]Enter address: [/]");
        while (string.IsNullOrEmpty(user.Address))
        {
            AnsiConsole.MarkupLine("[bold red]Address cannot be empty![/]");
            user.Address = AnsiConsole.Ask<string>("[bold green]Enter address: [/]");
        }
        do
        {
            username = AnsiConsole.Ask<string>("[bold green]Enter username: [/]");
            existingUser = db.Users.FirstOrDefault(x => x.Username == username);
            if (existingUser != null)
            {
                AnsiConsole.MarkupLine("[bold red]Username already exists![/]");
            }
        } while (existingUser != null);
        user.Username = username;
        string password;
        string passwordConfirm;
        do
        {
            password = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold green]Enter password: [/]")
            .Secret());
            passwordConfirm = AnsiConsole.Prompt(
                new TextPrompt<string>("[bold green]Confirm password: [/]")
            .Secret());
            if (passwordConfirm != passwordConfirm)
            {
                AnsiConsole.MarkupLine("[bold red]Passwords do not match, please try again[/]");
            }
        } while (password != passwordConfirm);
        user.Password = password;
        db.Users.Add(user);
        db.SaveChanges();
        UserController.user = user;
        AnsiConsole.Markup("[bold green]Registration successful, press any key to go back![/]");
        Console.ReadKey();
        // Go to the main Menu
        MenuController.DefaultMenuController();
    }

    // Edit profileController
    public static void EditProfileController()
    {
        showProfile();
        AnsiConsole.MarkupLine("[bold green]Do you want to edit your profile? ([/][bold yellow]Y[/]/[bold red]N[/])");
        string confirmation = Console.ReadLine();
        if (confirmation.ToUpper() == "N")
        {
            return;
        }
        else if (confirmation.ToUpper() == "Y")
        {
            EditProfile();
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Invalid choice, press any key to go back[/]");
            Console.ReadKey();
        }
    }

    private static void showProfile()
    {
        Console.Clear();
        var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.None,
            Padding = new Padding(1,1,1,1),
            Header = new PanelHeader("[bold yellow]WELCOME TO HRM APP[/]").Centered(),
        };
        Table userInfo = new Table()
        {
            Title = new TableTitle("[bold green]User Infomation[/]"),
        };
        userInfo.AddColumn("[bold]User ID[/]");
        userInfo.AddColumn("[bold]User Name[/]");
        userInfo.AddColumn("[bold]Full Name[/]");
        userInfo.AddColumn("[bold]Email[/]");
        userInfo.AddColumn("[bold]Address[/]");
        userInfo.AddRow(user.UserId.ToString(), user.Username, user.FullName, user.Email, user.Address);
        AnsiConsole.Render(userInfo);
    }

    public static void EditProfile()
        {
            using var db = new HotelContext();
            AnsiConsole.Markup("[bold green]Enter new name: [/]");
            string newName = Console.ReadLine();
            while (string.IsNullOrEmpty(newName))
            {
                //Console.Clear();
                Console.WriteLine("Name cannot be empty. Please enter a new name.");
                AnsiConsole.Markup("[bold green]Enter new name: [/]");
                newName = Console.ReadLine();
            }
            user.FullName = newName;

            string email;
            var existingUser = new User();
            do
            {
                email = AnsiConsole.Ask<string>("[bold green]Enter new email: [/]");
                existingUser = db.Users.FirstOrDefault(u => u.Email == email); // Assign the existingUser value
                if (existingUser != null)
                {
                    AnsiConsole.MarkupLine("[bold red]Email already exists! Please enter again![/]");
                }
                else if (!IsValidEmail(email))
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid email format! Please enter a valid email![/]");
                }
                else if (string.IsNullOrEmpty(email))
                {
                    AnsiConsole.MarkupLine("[bold red]Email cannot be empty. Please enter again.[/]");
                }
            } while (existingUser != null || !IsValidEmail(email));
            user.Email = email;

            AnsiConsole.Markup("[bold green]Enter the new address: [/]");
            string newAddress = Console.ReadLine();
            while (string.IsNullOrEmpty(newAddress))
            {
                Console.Clear();
                Console.Clear();
                AnsiConsole.MarkupLine("[bold yellow]Address cannot be empty. Please enter again.\n[/]");
                AnsiConsole.Markup("[bold green]Enter the new address: [/]");
                newAddress = Console.ReadLine();
            }
            user.Address = newAddress;
            AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to update your profile? ([/][bold green]Y[/]/[bold red]N[/])");
            string confirmation = Console.ReadLine();
            if (confirmation.ToUpper() == "Y")
            {
                db.Users.Update(user);
                db.SaveChanges();
                AnsiConsole.MarkupLine("[bold green]Profile updated successfully, press any key to continue[/]");
                Console.ReadKey();
            }
            else if (confirmation.ToUpper() == "N")
            {
                AnsiConsole.MarkupLine("[bold yellow]Profile update cancelled[/]");
                Console.ReadKey();
            }
    }

    // User logout function
    public static void Logout()
    {
        AnsiConsole.MarkupLine("[bold yellow]Are you sure you want to log out? ([/][bold green]Y[/]/[bold red]N[/])");
        string confirmation = Console.ReadLine();
        if (confirmation.ToUpper() == "Y")
        {
            user = null;
            AnsiConsole.MarkupLine("[bold green]Log out successful, press any key to continue[/]");
            Console.ReadKey();
            MenuController.DefaultMenuController();
        }
        else if (confirmation.ToUpper() == "N")
        {
            AnsiConsole.MarkupLine("[bold yellow]Log out cancelled, press any key to continue[/]");
            Console.ReadKey();
        }
        else
        {
            AnsiConsole.MarkupLine("[bold red]Invalid choice, press any key to continue[/]");
            Console.ReadKey();
        }
    }

    // Validate email format
    private static bool IsValidEmail(string email)
    {
        // Use regular expression to validate email format
        string pattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        return Regex.IsMatch(email, pattern);
    }


}