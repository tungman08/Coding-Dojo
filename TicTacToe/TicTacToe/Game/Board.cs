using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    class Board
    {
        private readonly int _position;

        public Board()
        {
            // cursor เริ่มต้นของตาราง
            _position = Console.CursorTop;

            CreateTable();
        }

        public void Render(string[,] gameData)
        {
            for (var row = 0; row < gameData.GetLength(0); row++) // 0 = height
            {
                for (var col = 0; col < gameData.GetLength(1); col++) // 1 = width
                {
                    var symbol = gameData[row, col].Trim();

                    if (symbol != string.Empty)
                    {
                        var top = RowToTop(row);
                        var left = ColToLeft(col);

                        Console.SetCursorPosition(left, top);
                        Console.ForegroundColor = (symbol == "X") ? ConsoleColor.Red : ConsoleColor.Blue;
                        Console.Write($" {symbol} ");
                        Console.ResetColor();
                    }
                }
            }

            Console.SetCursorPosition(0, _position + 9);
        }

        public void PutSymbol(string symbol, int index)
        {
            var point = IndexToPoint(index);
            var top = RowToTop(point.Item1);
            var left = ColToLeft(point.Item2);

            Console.SetCursorPosition(left, top);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write($" { symbol } ");
            Console.ResetColor();
        }

        public void ClearSymbol(int index)
        {
            var point = IndexToPoint(index);
            var top = RowToTop(point.Item1);
            var left = ColToLeft(point.Item2);

            Console.SetCursorPosition(left, top);
            Console.Write("   ");
        }

        protected Tuple<int, int> IndexToPoint(int index)
        {
            var row = index / 3;
            var col = index % 3;

            return Tuple.Create(row, col);
        }

        protected int RowToTop(int row)
        {
            return _position + (row * 2) + 2;
        }

        protected int ColToLeft(int col)
        {
            return (col * 4) + 2;
        }

        protected void CreateTable()
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
    }
}
