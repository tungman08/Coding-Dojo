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
            Console.WriteLine(" +---+---+---+");
            Console.WriteLine(" |   |   |   |");
            Console.WriteLine(" +---+---+---+");
            Console.WriteLine(" |   |   |   |");
            Console.WriteLine(" +---+---+---+");
            Console.WriteLine(" |   |   |   |");
            Console.WriteLine(" +---+---+---+");
            Console.WriteLine();
        }

        public static void Draw()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"***  Draw  ***");
            Console.ResetColor();
        }

        public static void Winner(string name)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"*** {name} Win ***");
            Console.ResetColor();
        }

        public static void Summary(int win, int lose, int draw)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Summary Score:");
            Console.ResetColor();
            Console.WriteLine("Play: {0,8}", win + lose + draw);
            Console.WriteLine("Win:  {0,8}", win);
            Console.WriteLine("Lose: {0,8}", lose);
            Console.WriteLine("Draw: {0,8}", draw);
            Console.WriteLine();
        }
    }
}
