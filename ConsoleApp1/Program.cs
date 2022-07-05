using System;
using ConsoleApp1.Entities;
using ConsoleApp1.Servise;
using Spectre.Console;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskManager taskManager = new TaskManager();
            IAction consoleInterface = new ConsoleAction(taskManager);
            consoleInterface.StartListening();
        }
    }
}