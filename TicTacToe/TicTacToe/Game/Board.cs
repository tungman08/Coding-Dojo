using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    class Board
    {
        private readonly int _position;

        public Board() : this(BoardSize.Small)
        {
        }

        public Board(BoardSize size)
        {
            _position = Console.CursorTop; // cursor เริ่มต้นของตาราง
            Size = size;

            CreateTable();
        }

        public BoardSize Size { get; private set; }

        public void Render(List<Slot> slots)
        {
            foreach (var slot in slots)
            {
                if (slot.Value != string.Empty)
                {
                    var top = RowToTop(slot.Row);
                    var left = ColumnToLeft(slot.Column);

                    Console.SetCursorPosition(left, top);
                    Console.ForegroundColor = (slot.Value == "X") ? ConsoleColor.Red : ConsoleColor.Blue;
                    Console.Write($" {slot.Value} ");
                    Console.ResetColor();
                }
            }

            Console.SetCursorPosition(0, MoveToEnd());
        }

        public void Cursor(Slot slot)
        {
            if (slot.IsX != null)
            {
                var symbol = slot.Value;
                var top = RowToTop(slot.Row);
                var left = ColumnToLeft(slot.Column);

                Console.SetCursorPosition(left, top);
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.Write($" { symbol } ");
                Console.ResetColor();
                Console.SetCursorPosition(0, MoveToEnd());
            }
        }

        public void Clear(Slot slot)
        {
            var top = RowToTop(slot.Row);
            var left = ColumnToLeft(slot.Column);

            Console.SetCursorPosition(left, top);
            Console.Write("   ");
            Console.SetCursorPosition(0, MoveToEnd());
        }

        protected int RowToTop(int row)
        {
            return _position + (row * 2) + 2;
        }

        protected int ColumnToLeft(int col)
        {
            return (col * 4) + 2;
        }

        protected int MoveToEnd()
        {
            var end = (Size != BoardSize.Small) ? (Size != BoardSize.Medium) ? 17 : 13 : 9;

            return _position + end;
        }

        protected void CreateTable()
        {
            var table = new string[] { " +", " |", "---+", "   |" };

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Play:");
            Console.ResetColor();

            for (var row = 0; row <= (int)Size * 2; row++)
            {
                var line = string.Empty;

                for (var col = 0; col <= (int)Size; col++)
                {
                    if (row % 2 == 0)
                    {
                        line += (col == 0) ? table[0] : table[2];
                    }
                    else
                    {
                        line += (col == 0) ? table[1] : table[3];
                    }
                }

                Console.WriteLine(line);
            }

            Console.WriteLine();
            Console.SetCursorPosition(0, MoveToEnd());
        }
    }
}
