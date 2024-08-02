using System;
using Spectre.Console;
using UI;

class Menu
{
    // Display the Welcome screen panel
    public static void Panel()
    {
        var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.Square,
            Padding = new Padding(1, 1, 1, 1),
        };

        AnsiConsole.Write(panel);
    }


    //Display the default menu screen
    public static void DefaultMenu()
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Main Menu");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Room[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Register[/]"));
        table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Logout[/]"));
        table3.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);

        AnsiConsole.Write(mainTable);
    }

    public static void MainMenu()
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Main Menu");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Customer[/]"));
        table1.Expand();
        // var table2 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Check room[/]"));
        // table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Booking[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Bill[/]"));
        table4.Expand();
        // var table5 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table5.AddColumn(new TableColumn("[bold yellow]5.[/][bold] Edit profile[/]"));
        // table5.Expand();
        var table6 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table6.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Log out[/]"));
        table6.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        // mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        // mainTable.AddRow(table5);
        mainTable.AddRow(table6);

        AnsiConsole.Write(mainTable);
    }

    public static void CustomerMenu()
    {
        AnsiConsole.Clear();
        ConsoleUI uI= new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Customer Menu");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Add customer[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update customer[/]"));
        table2.Expand();

        // var table3 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete customer[/]"));
        // table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Search customer by name[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table5.Expand();
        var mainTable = new Table();
        //mainTable.AddColumn(new TableColumn(panel));
        mainTable.AddColumn(new TableColumn(table1)).RoundedBorder().Centered();
        mainTable.AddRow(table2);
        // mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void UpdateCustomerMenu()
    {
        AnsiConsole.Clear();
        // Tạo tiêu đề lớn
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Update Customer");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Update Name[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update Email[/]"));
        table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Update Phone number[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Update Number of participants[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table5.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void BookingMenu()
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        uI.Title("Booking");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Add booking[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update booking[/]"));
        table2.Expand();
        // var table3 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete booking[/]"));
        // table3.Expand();

        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Search booking by customer ID[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table5.Expand();

        var mainTable = new Table();
        // mainTable.AddColumn(new TableColumn(table1)).RoundedBorder().Centered();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        // mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void BillMenu()
    {
        AnsiConsole.Clear();
        // Tạo tiêu đề lớn
        ConsoleUI uI= new ConsoleUI();
        uI.Title("Bill");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Add bill[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update bill[/]"));
        table2.Expand();

        // var table3 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete bill[/]"));
        // table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Search bill by customer ID[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Pay bill[/]"));
        table5.Expand();
        var table6 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table6.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table6.Expand();
        var mainTable = new Table();
        //mainTable.AddColumn(new TableColumn(panel));
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        // mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);
        mainTable.AddRow(table6);

        AnsiConsole.Write(mainTable);
    }

    public static void UpdateBillMenu()
    {
        AnsiConsole.Clear();
        // Tạo tiêu đề lớn
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        Console.WriteLine();
        uI.Title("Update Bill");
        Console.WriteLine();
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Update Description[/]"));
        table1.Expand();
        // var table2 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update Number of nights[/]"));
        // table2.Expand();

        // var table3 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Update Number of rooms[/]"));
        // table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table4.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        // mainTable.AddRow(table2);
        // mainTable.AddRow(table3);
        mainTable.AddRow(table4);

        AnsiConsole.Write(mainTable);
    }
    
    public static void UpdateBookingMenu()
    {
        AnsiConsole.Clear();
        // Tạo tiêu đề lớn
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        Console.WriteLine();
        uI.Title("Update Booking");
        Console.WriteLine();
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Update Customer ID[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update Date[/]"));
        table2.Expand();

        // var table3 = new Table()
        // {
        //     Border = TableBorder.Rounded,
        // };
        // table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Update Check Out Date[/]"));
        // table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table4.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        // mainTable.AddRow(table3);
        mainTable.AddRow(table4);

        AnsiConsole.Write(mainTable);
    }

    public static void RoomMenu()
    {
        AnsiConsole.Clear();    
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Room Menu");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Add Room[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update Room[/]"));
        table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Search Room By Room ID[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table4.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);

        AnsiConsole.Write(mainTable);
    }

    public static void UpdateRoomMenu()
    {
        AnsiConsole.Clear();    
        ConsoleUI uI = new ConsoleUI();
        uI.ApplicationLogoBeforeLogin();
        uI.Title("Update Room");
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Update Room Number[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Update Price[/]"));
        table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Update Description[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Update Room Type[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn("")).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }
}
