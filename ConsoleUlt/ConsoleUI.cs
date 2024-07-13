using Spectre.Console;
using System.Globalization;
using System;
using Controller;
using Model;

namespace UI
{
    public class ConsoleUI
    {
        public void Line()
        {
            var rule = new Rule();
            AnsiConsole.Write(rule.DoubleBorder());
        }

        //PressAnyKeyToContinue
        public void PressAnyKeyToContinue()
        {
            AnsiConsole.Markup("[Yellow]Press any key to continue[/]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }

        //Logo
        public void ApplicationLogoAfterLogin(User user)
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn(@"
            ██╗  ██╗██████╗ ███╗   ███╗     █████╗ ██████╗ ██████╗ 
            ██║  ██║██╔══██╗████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗
            ███████║██████╔╝██╔████╔██║    ███████║██████╔╝██████╔╝
            ██╔══██║██╔══██╗██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝ 
            ██║  ██║██║  ██║██║ ╚═╝ ██║    ██║  ██║██║     ██║     
            ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝             
            ----------------------------+--------------------------            ").Centered()).RoundedBorder().Centered();
            table.AddRow($"[Green]{user.FullName + " - ID: " }[/]");

            AnsiConsole.Write(table);
        }

                public void ApplicationLogoBeforeLogin()
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn(@"
            ██╗  ██╗██████╗ ███╗   ███╗     █████╗ ██████╗ ██████╗ 
            ██║  ██║██╔══██╗████╗ ████║    ██╔══██╗██╔══██╗██╔══██╗
            ███████║██████╔╝██╔████╔██║    ███████║██████╔╝██████╔╝
            ██╔══██║██╔══██╗██║╚██╔╝██║    ██╔══██║██╔═══╝ ██╔═══╝ 
            ██║  ██║██║  ██║██║ ╚═╝ ██║    ██║  ██║██║     ██║     
            ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝    ╚═╝  ╚═╝╚═╝     ╚═╝            
            ----------------------------+--------------------------            ").Centered()).RoundedBorder().Centered();
            AnsiConsole.Write(table);
        }

        public void Introduction()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            TitleNoBorder(@"
                ██████╗ ██████╗  ██████╗ ██████╗ ██╗   ██╗ ██████╗████████╗    ██████╗ ██╗   ██╗                
                ██╔══██╗██╔══██╗██╔═══██╗██╔══██╗██║   ██║██╔════╝╚══██╔══╝    ██╔══██╗╚██╗ ██╔╝                
                ██████╔╝██████╔╝██║   ██║██║  ██║██║   ██║██║        ██║       ██████╔╝ ╚████╔╝                 
                ██╔═══╝ ██╔══██╗██║   ██║██║  ██║██║   ██║██║        ██║       ██╔══██╗  ╚██╔╝                  
                ██║     ██║  ██║╚██████╔╝██████╔╝╚██████╔╝╚██████╗   ██║       ██████╔╝   ██║                   
                ╚═╝     ╚═╝  ╚═╝ ╚═════╝ ╚═════╝  ╚═════╝  ╚═════╝   ╚═╝       ╚═════╝    ╚═╝                   
");
            Thread.Sleep(1000);
            Console.Clear();
            TitleNoBorder(@"
            ██╗   ██╗████████╗ ██████╗     █████╗  ██████╗ █████╗ ██████╗ ███████╗███╗   ███╗██╗   ██╗            
            ██║   ██║╚══██╔══╝██╔════╝    ██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝████╗ ████║╚██╗ ██╔╝            
            ██║   ██║   ██║   ██║         ███████║██║     ███████║██║  ██║█████╗  ██╔████╔██║ ╚████╔╝             
            ╚██╗ ██╔╝   ██║   ██║         ██╔══██║██║     ██╔══██║██║  ██║██╔══╝  ██║╚██╔╝██║  ╚██╔╝              
             ╚████╔╝    ██║   ╚██████╗    ██║  ██║╚██████╗██║  ██║██████╔╝███████╗██║ ╚═╝ ██║   ██║               
              ╚═══╝     ╚═╝    ╚═════╝    ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝     ╚═╝   ╚═╝               

");
            Thread.Sleep(1000);
            Console.Clear();
            TitleNoBorder(@"
                ██╗  ██╗██████╗ ███╗   ███╗       
                ██║  ██║██╔══██╗████╗ ████║       
                ███████║██████╔╝██╔████╔██║       
                ██╔══██║██╔══██╗██║╚██╔╝██║       
                ██║  ██║██║  ██║██║ ╚═╝ ██║       
                ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝     ╚═╝       
                     
                                                                                                
            ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗     █████╗ ██████╗ ██████╗ 
            ██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝    ██╔══██╗██╔══██╗██╔══██╗
            ██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗      ███████║██████╔╝██████╔╝
            ██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝      ██╔══██║██╔═══╝ ██╔═══╝ 
            ╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗    ██║  ██║██║     ██║     
            ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝    ╚═╝  ╚═╝╚═╝     ╚═╝     
");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Title
        public void Title(string title)
        {
            // Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn($"{title}").Centered()).RoundedBorder().Centered();
            AnsiConsole.Write(table);
        }

        public void TitleNoBorder(string title)
        {
            Console.Clear();
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn($"{title}").Centered()).NoBorder().Centered();
            AnsiConsole.Write(table);
        }

        // Message Color
        public void RedMessage(string message)
        {
            AnsiConsole.Markup($"[underline red]{message}[/]");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            PressAnyKeyToContinue();
            Console.Clear();
        }

        public void GreenMessage(string message)
        {
            Console.WriteLine();
            AnsiConsole.Markup($"[underline green]{message}[/] ");
            //Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        //Staff
        public void WelcomeStaff(User user)
        {
            Console.Clear();
            TitleNoBorder($"[green]Welcome {user.FullName}[/]");
            Thread.Sleep(1000);
        }

        public void About(User currentUser)
        {
            Console.Clear();
            ApplicationLogoAfterLogin(currentUser);
            Title("ABOUT");
            var table = new Spectre.Console.Table();
            table.AddColumn(new TableColumn("ABOUT").Centered());
            table.AddRow("Hotel Reception Management Application");
            table.AddRow("Version: V1.0.0");
            table.AddRow("Made By : Nguyen Dinh Kha, Vu Trong Giang, Nguyen Ngoc Linh");
            table.AddRow("Instructor: Nguyen Dinh Cuong");
            AnsiConsole.Write(table.Centered());
            PressAnyKeyToContinue();
        }

        public string SellectFunction(string[] item)
        {
            var choice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
               .Title("Move [green]UP/DOWN[/] button and [Green] ENTER[/] to select function")
               .PageSize(10)
               .AddChoices(item));
            return choice;
        }


    }
}