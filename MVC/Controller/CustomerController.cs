using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using Spectre.Console;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Model;
using UI;

public static class CustomerController
{
    //public static Customer customer;
    public static void ManagementCustomer()
    {
        while (true)
        {    
            AnsiConsole.Clear();
            Menu.CustomerMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    UpdateCustomer();
                    break;
                // case "3":
                //     DeleteCustomer();
                //     break;
                case "3":
                    SearchingCustomer();
                    break;
                case "0":
                    return; 
                default:
                    AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter valid choice![/]");
                    Console.ReadKey();
                    break;
            }
        }
    }
    // Controller add customer
    public static void AddCustomer()
    {
        try
        {
            ConsoleUI uI = new ConsoleUI();
            AnsiConsole.Clear();
            uI.Title("Add Customer");
            using var db = new HotelContext();
            Customer customer = new Customer();
            
            // Get customer name
            AnsiConsole.Markup("[bold green]Enter customer name: [/]");
            string name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer name.[/]");
                AnsiConsole.Markup("[bold green]Enter customer name: [/]");
                name = Console.ReadLine();
            }
            customer.Name = name;

            // Get customer email
            string email;
            var existingCustomer = new Customer();
            do
            {
                email = AnsiConsole.Ask<string>("[bold green]Enter email: [/]");
                existingCustomer = db.Customers.FirstOrDefault(u => u.Email == email); // Assign the existingUser value
                if (existingCustomer != null)
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
            } while (existingCustomer != null || !IsValidEmail(email));
            customer.Email = email;

            // Get customer phone number
            string phonenumber;
            do
            {
                phonenumber = AnsiConsole.Ask<string>("[bold green]Enter phone number: [/]");
                existingCustomer = db.Customers.FirstOrDefault(u => u.PhoneNumber == phonenumber); // Assign the existingUser value
                if (existingCustomer != null)
                {
                    AnsiConsole.MarkupLine("[bold red]Phone number already exists! Please enter again![/]");
                }
                else if (!IsValidPhoneNumber(phonenumber))
                {
                    AnsiConsole.MarkupLine("[bold red]Invalid phonenumber format! Please enter a valid input![/]");
                }
                else if (string.IsNullOrEmpty(phonenumber))
                {
                    AnsiConsole.MarkupLine("[bold red]Phone number cannot be empty. Please enter again.[/]");
                }
            } while (existingCustomer != null || !IsValidPhoneNumber(phonenumber));
            
            customer.PhoneNumber = phonenumber;

            // Get NumberOfParticipants
            AnsiConsole.Markup("[bold green]Enter number of participants: [/]");
            int numberofcustomers;
            while (!int.TryParse(Console.ReadLine(), out numberofcustomers) || numberofcustomers <= 0)
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid price.[/]");
                AnsiConsole.MarkupLine("[bold green]Enter number of participants: [/]");
            }
            customer.NumberOfCustomers = numberofcustomers;
            

            // Confirm adding customer
            AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
            string confirm = Console.ReadLine();
            if (confirm.ToUpper() == "Y")
            {
                AnsiConsole.MarkupLine("[bold green]Customer added successfully![/]");
                AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                db.Customers.Add(customer);
                db.SaveChanges();
                
                BillController.customerId = customer.CustomerId;
                Console.ReadKey();
            }
            else
            {
                AnsiConsole.MarkupLine("[bold yellow]Customer not added![/]");
                Console.WriteLine();
                AnsiConsole.Markup("[bold yellow]Do you want to add more customers? (Y/N): [/]");
                confirm = Console.ReadLine();
                if (confirm.ToUpper() == "Y")
                {
                    AddCustomer();
                }
                else if (confirm.ToUpper() == "N")
                {
                    AnsiConsole.MarkupLine("[bold yellow]Press any key to go back![/]");
                    Console.ReadKey();
                }
                // Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
            Console.ReadKey();
        }
    }

    // Controller update customer
    public static void UpdateCustomer()
    {
        try
        {
            
            ConsoleUI uI = new ConsoleUI();
            Console.Clear();
            uI.ApplicationLogoBeforeLogin();
            uI.Title("Update Customer");
            using var db = new HotelContext();
            //Console.Clear();
            // var Panel = new Panel("[bold green]Update Customer:[/]");
            int customerId;
            AnsiConsole.Markup("[bold green]Enter customer ID to update(Enter 0 to Exit): [/]");
            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer ID.[/]");
                AnsiConsole.Markup("[bold green]Enter customer ID to update(Enter 0 to Exit):[/]");
            }            
            if (customerId == 0)
            {
                return;
            }

            // Find the customer ID
            var customer = db.Customers.FirstOrDefault(u => u.CustomerId == customerId);
            if (customer == null)
            {
                AnsiConsole.MarkupLine($"[bold red]Customer with ID {customerId} not found.[/]");
                AnsiConsole.MarkupLine("[yellow]Press any key to go back![/]");
                Console.ReadKey();
                return;
            }
            //Menu.CustomerUpdateMenu(customerId);
            Menu.UpdateCustomerMenu();
            AnsiConsole.Markup("[bold green]Enter your choice: [/]");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    UpdateCustomerName(customer);
                    break;
                case "2":
                    UpdateCustomerEmail(customer);
                    break;
                case "3":
                    UpdateCustomerNumberPhone(customer);
                    break;
                case "4":
                    UpdateNumberOfCustomers(customer);
                    break;
                case "0":
                    return;
                    break;
            }
            db.SaveChanges();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
        }
    }

    private static void UpdateCustomerName(Customer customer)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("Update Customer Name");
        Console.WriteLine();
        AnsiConsole.Markup("[bold green]Enter new cutomer name: [/]");
        string name = Console.ReadLine();
        while (string.IsNullOrEmpty(name))
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer name.[/]");
            AnsiConsole.Markup("[bold green]Enter new customer name:[/]");
            name = Console.ReadLine();
        }        
        //Confirm updated name
        AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
        string confirm = Console.ReadLine();
        if (confirm.ToUpper() == "Y")
        {
            customer.Name = name;
            AnsiConsole.MarkupLine("[bold green]Customer name updated successfully!, Press any key to go back![/]");
            Console.ReadKey();
        }
        else if (confirm.ToUpper() == "N")
        {
            AnsiConsole.MarkupLine("[bold yellow]Customer name not updated!, Press any key to go back![/]");
            Console.ReadKey();
        }
    }

    private static void UpdateCustomerEmail(Customer customer)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("Update Customer Email");
        Console.WriteLine();
        string email;
        var existingCustomer = new Customer();
        using var db = new HotelContext();
        do
        {
            email = AnsiConsole.Ask<string>("[bold green]Enter new email: [/]");
            existingCustomer = db.Customers.FirstOrDefault(u => u.Email == email); // Assign the existingUser value
            if (existingCustomer != null)
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
        } while (existingCustomer != null || !IsValidEmail(email));
        
        // Confirm email
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): ");
        string confirmation = Console.ReadLine();
        if (confirmation.ToUpper() == "Y")
        {
            customer.Email = email;
            AnsiConsole.MarkupLine("[bold green]Customer email updated successfully, press any key to continue[/]");
            Console.ReadKey();
        }
        else if (confirmation.ToUpper() == "N")
        {
            AnsiConsole.MarkupLine("[bold yellow]Customer email update cancelled[/]");
            Console.ReadKey();
        }
    }

    private static void UpdateCustomerNumberPhone(Customer customer)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("Update Customer Phone Number");
        Console.WriteLine();
        string phonenumber;
        using var db = new HotelContext();
        var existingCustomer = new Customer();
        do
        {
            phonenumber = AnsiConsole.Ask<string>("[bold green]Enter new phone number: [/]");
            existingCustomer = db.Customers.FirstOrDefault(u => u.PhoneNumber == phonenumber); // Assign the existingUser value
            if (existingCustomer != null)
            {
                AnsiConsole.MarkupLine("[bold red]Phone number already exists! Please enter again![/]");
            }
            else if (!IsValidPhoneNumber(phonenumber))
            {
                AnsiConsole.MarkupLine("[bold red]Invalid phonenumber format! Please enter a valid input![/]");
            }
            else if (string.IsNullOrEmpty(phonenumber))
            {
                AnsiConsole.MarkupLine("[bold red]Phone number cannot be empty. Please enter again.[/]");
            }
        } while (existingCustomer != null || !IsValidPhoneNumber(phonenumber));    
        //Confirm updated number phone
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N): [/]");
        string confirm = Console.ReadLine();
        if (confirm.ToUpper() == "Y")
        {
            customer.PhoneNumber = phonenumber;
            AnsiConsole.MarkupLine("[bold green]Customer number phone updated successfully!, Press any key to go back![/]");
            Console.ReadKey();
        }
        else if (confirm.ToUpper() == "N")
        {
            AnsiConsole.MarkupLine("[bold yellow]Customer number phone not updated!, Press any key to go back![/]");
            Console.ReadKey();
        }
    }

    private static void UpdateNumberOfCustomers(Customer customer)
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("Update Number Of Participants");
        Console.WriteLine();
        AnsiConsole.Markup("[bold green]Enter new number of participants: [/]");
        int numberofcustomers;

        while (!int.TryParse(Console.ReadLine(), out numberofcustomers) || numberofcustomers <= 0)
        {
            AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid input.[/]");
            AnsiConsole.Markup("[bold green]Enter new number of participants:[/]");
        }
        //Confirm updated number of customers
        AnsiConsole.Markup("[bold yellow]Do you want to save? (Y/N):[/]");
        string confirm = Console.ReadLine();
        if (confirm.ToUpper() == "Y")
        {
            customer.NumberOfCustomers = numberofcustomers;
            AnsiConsole.MarkupLine("[bold green]Number of participants updated successfully!, press any key to go back![/]");
            Console.ReadKey();
        }
        else if (confirm.ToUpper() == "N")
        {
            AnsiConsole.MarkupLine("[bold yellow]Number of participants not updated!, press any key to go back![/]");
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

    // Validate phone number format
    private static bool IsValidPhoneNumber(string phoneNumber)
    {
        // Use regular expression to validate phone number format
        string pattern = @"^\(?(\d{3})\)?[-.]? ?(\d{3})[-.]? ?(\d{4})$";
        return Regex.IsMatch(phoneNumber, pattern);
    }
    
    // Controller delete customer
    // public static void DeleteCustomer()
    // {
    //     try
    //     {
    //         using (var db = new HotelContext())
    //         {
    //             Console.Clear();
    //             var Panel = new Panel("[bold green]Delete Customer[/]");
    //             int customerId;
    //             AnsiConsole.MarkupLine("[bold green]Enter customer ID to delete:[/]");
    //             while (!int.TryParse(Console.ReadLine(), out customerId))
    //             {
    //                 AnsiConsole.MarkupLine("[bold red]Invalid input! Please enter a valid customer ID.[/]");
    //                 AnsiConsole.MarkupLine("[bold green]Enter customer ID to delete:[/]");
    //             }
    //             // Find the room by ID
    //             var customer = db.Customers.FirstOrDefault(r => r.CustomerId == customerId);
    //             if (customer == null)
    //             {
    //                 AnsiConsole.MarkupLine("[bold red]Customer not found![/]");
    //                 Console.ReadKey();
    //                 return;
    //             }
    //             AnsiConsole.MarkupLine("[bold yellow]Do you want to save? (Y/N):[/]");
    //             string confirm = Console.ReadLine();
    //             if (confirm.ToUpper() == "Y")
    //             {
    //                 db.Customers.Remove(customer);
    //                 db.SaveChanges();
    //                 AnsiConsole.MarkupLine("[bold green]Customer deleted successfully!, press any key to go back![/]");
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

    // Searching customer by name
    public static void SearchingCustomer()
    {
        try
        {
            ConsoleUI uI = new ConsoleUI();
            Console.Clear();
            uI.GreenMessage("Enter customer name to search: ");
            string name = Console.ReadLine();
            using (var db = new HotelContext())
            {
                var customers = db.Customers
                .Where(c => c.Name.Contains(name))
                .ToList();
                Console.Clear();
                if (customers.Count == 0)
                {
                    AnsiConsole.MarkupLine("[bold yellow]No customer found! Press any key to go back![/]");
                    customers = db.Customers.ToList();
                }

                int pageSize = 5;
                int currentPage = 1;
                int totalPages = (int)Math.Ceiling((double)customers.Count / pageSize);

                while (true)
                {
                    Console.Clear();
                    AnsiConsole.MarkupLine($"[bold green]Found {customers.Count} customers[/]");
                    var table = new Table()
                    {
                        Title = new TableTitle($"[bold yellow]Page {currentPage}/{totalPages}[/]"),
                    };
                    table.AddColumn("[bold]ID[/]");
                    table.AddColumn("[bold]Customer Name[/]");
                    table.AddColumn("[bold]Email[/]");
                    table.AddColumn("[bold]Phone Number[/]");
                    table.AddColumn("[bold]Number of Customers[/]");
                    Console.WriteLine();
                    Console.WriteLine();

                    for (int i = (currentPage - 1) *pageSize; i < currentPage * pageSize && i < customers.Count; i++)
                    {
                        var customer = customers[i];
                        table.AddRow(
                            customer.CustomerId.ToString(),
                            customer.Name,
                            customer.Email,
                            customer.PhoneNumber,
                            customer.NumberOfCustomers.ToString()
                        );
                    }
                    table.Expand();
                    AnsiConsole.Write(table);

                    Console.WriteLine();
                    AnsiConsole.MarkupLine("[bold]Press '[/][bold red]CTRL + P[/][bold]' for previous page, '[/][bold red]CTRL +N[/][bold]' for next page[/]");
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
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[bold red]Error: [/]" + ex.Message);
        }
    }
}
