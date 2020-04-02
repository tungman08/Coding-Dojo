using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Display
    {
        public static void Logo()
        {
            Console.WriteLine();
            Console.WriteLine("\t    " + @"    |\__/,|   (`\");
            Console.WriteLine("\t    " + @"  _.|o o  |_   ) )");
            Console.WriteLine("\t    " + @"-(((---(((--------");
            Console.WriteLine();
            Console.WriteLine("============= Tic Tac Toe =============");
        }

        public static void Table()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Play:");
            Console.ResetColor();
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("|   |   |   |");
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("|   |   |   |");
            Console.WriteLine("+---+---+---+");
            Console.WriteLine("|   |   |   |");
            Console.WriteLine("+---+---+---+");
            Console.WriteLine();
        }

        public static void Winner(string name)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"*** {name} Win ***");
            Console.ResetColor();
        }
    }
}
