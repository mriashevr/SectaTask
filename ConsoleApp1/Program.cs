using System;
using Spectre.Console;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            AnsiConsole.Markup("[underline red]Hello World![/]\n");
            
            
            var table =  new Table();
            table.AddColumn("Name");
            table.AddColumn("Status");
            
            table.AddRow("Sonya", "[green]doing physics[/]");
            table.AddRow("Mary", "[red]doing shit[/]");
            AnsiConsole.Render(table);
        }
    }
}