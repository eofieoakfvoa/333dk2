// See https://aka.ms/new-console-template for more information
using System;
using System.Runtime.InteropServices;
//använd c för att använda en knapp pilarna för up, ner eller höger vänster, debug console visar vilken som är selected
//1 högst up, går neråt
class Program1
{
    static void Main(string[] args)
    {
        ConsoleManager.InitConsole();
        ConsoleManager.Update();
        Console.ReadLine();

    }
}