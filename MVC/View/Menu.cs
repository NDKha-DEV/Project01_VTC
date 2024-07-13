using System;
using Spectre.Console;
using UI;

class Menu
{
    // Display the Welcome screen panel
    public static void Panel()
    {
         // Tạo tiêu đề lớn
        var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.Square,
            Padding = new Padding(1, 1, 1, 1),
        };

        // Hiển thị tiêu đề
        AnsiConsole.Write(panel);
    }


    //Display the default menu screen
    public static void DefaultMenu()
    {
        AnsiConsole.Clear();
        ConsoleUI uI = new ConsoleUI();
        // Display the large title
         var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.None,
            Padding = new Padding(1, 1),
            Header = new PanelHeader("[yellow]Welcome to[/]").Centered(),
        };
//        uI.ApplicationLogoBeforeLogin();
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Login[/]"));
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
        table3.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Exit[/]"));
        table3.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn(panel));
        // mainTable.AddColumn(new TableColumn(@"
        //     ██╗  ██╗██████╗ ███╗   ███╗     █████╗ ██████╗ ██████╗ 
        //     ██║  ██║██╔══██╗████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗
        //     ███████║██████╔╝██╔████╔██║    ███████║██████╔╝██████╔╝
        //     ██╔══██║██╔══██╗██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝ 
        //     ██║  ██║██║  ██║██║ ╚═╝ ██║    ██║  ██║██║     ██║     
        //     ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝             
        //     ----------------------------+--------------------------            ").Centered()).RoundedBorder().Centered();
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);

        AnsiConsole.Write(mainTable);
    }

    public static void MainMenu()
    {
        AnsiConsole.Clear();
        // Tạo tiêu đề lớn
        var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.None,
            Padding = new Padding(1, 1, 1, 1),
        };
        var table1 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table1.AddColumn(new TableColumn("[bold yellow]1.[/][bold] Customer[/]"));
        table1.Expand();
        var table2 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table2.AddColumn(new TableColumn("[bold yellow]2.[/][bold] Check room[/]"));
        table2.Expand();

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Booking[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Bill[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]5.[/][bold] Edit profile[/]"));
        table5.Expand();
        var table6 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table6.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Log out[/]"));
        table6.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn(panel));
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);
        mainTable.AddRow(table6);

        AnsiConsole.Write(mainTable);
    }

    public static void CustomerMenu()
    {
        Console.Clear();
        // Tạo tiêu đề lớn
        ConsoleUI uI= new ConsoleUI();
        uI.Title("Customer");
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

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete customer[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Search customer by name[/]"));
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
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void UpdateCustomerMenu()
    {
        Console.Clear();
        // Tạo tiêu đề lớn
        var panel = new Panel(new FigletText("HRM APP").Centered().Color(Color.Aqua))
        {
            Border = BoxBorder.None,
            Padding = new Padding(1, 1, 1, 1),
            Header = new PanelHeader("[bold yellow]WELLCOME TO HRM APP[/]").Centered(),
        };
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
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Update Number of customers[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table5.Expand();
        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn(panel));
        mainTable.AddRow(table1);
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void BookingMenu()
    {
        Console.Clear();
        // Tạo tiêu đề lớn
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
        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete booking[/]"));
        table3.Expand();

        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Search booking by customer ID[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table5.Expand();

        var mainTable = new Table();
        mainTable.AddColumn(new TableColumn(table1)).RoundedBorder().Centered();
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);

        AnsiConsole.Write(mainTable);
    }

    public static void BillMenu()
    {
        Console.Clear();
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

        var table3 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table3.AddColumn(new TableColumn("[bold yellow]3.[/][bold] Delete bill[/]"));
        table3.Expand();
        var table4 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table4.AddColumn(new TableColumn("[bold yellow]4.[/][bold] Search bill by customer ID[/]"));
        table4.Expand();
        var table5 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table5.AddColumn(new TableColumn("[bold yellow]5.[/][bold] Pay bill[/]"));
        table5.Expand();
        var table6 = new Table()
        {
            Border = TableBorder.Rounded,
        };
        table6.AddColumn(new TableColumn("[bold yellow]0.[/][bold] Return[/]"));
        table6.Expand();
        var mainTable = new Table();
        //mainTable.AddColumn(new TableColumn(panel));
        mainTable.AddColumn(new TableColumn(table1)).RoundedBorder().Centered();
        mainTable.AddRow(table2);
        mainTable.AddRow(table3);
        mainTable.AddRow(table4);
        mainTable.AddRow(table5);
        mainTable.AddRow(table6);

        AnsiConsole.Write(mainTable);
    }
}