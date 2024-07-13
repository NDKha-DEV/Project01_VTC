using System;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Spectre.Console;
using UI;

class ApplicationController
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        AnsiConsole.Background = ConsoleColor.Blue;
        ConsoleUI uI = new ConsoleUI();
        uI.Introduction();
        // MenuController.MainMenuController();
        MenuController.DefaultMenuController();
    }
}