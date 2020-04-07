using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    class Human : BasePlayer, IPlayer
    {
        public Human(string symbol) : base(symbol)
        {
        }

        protected int CurrentCell { get; set; }

        public Slot Play(OxGame game, Board board)
        {
            ConsoleKey key;
            Console.CursorVisible = false;

            // หาช่องที่ว่างแรกที่เจอเพื่อวาง cursor
            var first = game.Slots.First(s => s.IsX == null);
            CurrentCell = game.Slots.IndexOf(first);
            board.Cursor(new Slot { IsX = Symbol == "X", Row = first.Row, Column = first.Column });

            do
            {
                // กดปุ่มลูกศรเพื่อเลื่อน cursor ไปยังช่องที่ว่างอยู่
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp(board, game.Slots);
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown(board, game.Slots);
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveLeft(board, game.Slots);
                        break;
                    case ConsoleKey.RightArrow:
                        MoveRight(board, game.Slots);
                        break;
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            // ผู้เล่นใส่ค่าในช่องที่เลือก
            return new Slot { IsX = Symbol == "X", 
                Row = game.Slots[CurrentCell].Row, Column = game.Slots[CurrentCell].Column };
        }

        protected void MoveUp(Board board, List<Slot> slots)
        {
            var step = GetVerticalStep(board.Size);
            var current = step.IndexOf(CurrentCell);
            var cell = current - 1;

            while (cell >= 0)
            {
                if (FindEmptyCell(board, slots, step[cell]))
                {
                    break;
                }

                cell++;
            }
        }

        protected void MoveDown(Board board, List<Slot> slots)
        {
            var step = GetVerticalStep(board.Size);
            var current = step.IndexOf(CurrentCell);
            var cell = current + 1;

            while (cell < slots.Count)
            {
                if (FindEmptyCell(board, slots, step[cell]))
                {
                    break;
                }

                cell++;
            }
        }

        protected void MoveLeft(Board board, List<Slot> slots)
        {
            var cell = CurrentCell - 1;

            while (cell >= 0)
            {
                if (FindEmptyCell(board, slots, cell))
                {
                    break;
                }

                cell--;
            }
        }

        protected void MoveRight(Board board, List<Slot> slots)
        {
            var cell = CurrentCell + 1;

            while (cell < slots.Count)
            {
                if (FindEmptyCell(board, slots, cell))
                {
                    break;
                }

                cell++;
            }
        }

        protected bool FindEmptyCell(Board board, List<Slot> slots, int cell)
        {
            if (slots[cell].IsX == null)
            {
                var current = slots[CurrentCell];
                var newSlot = slots[cell];

                board.Clear(new Slot { IsX = Symbol == "X", Row = current.Row, Column = current.Column });
                board.Cursor(new Slot { IsX = Symbol == "X", Row = newSlot.Row, Column = newSlot.Column });
                CurrentCell = cell;

                return true;
            }

            return false;
        }

        protected List<int> GetVerticalStep(BoardSize size)
        {
            var step = new List<int>();

            for (int row = 0; row < (int)size; row++)
            {
                for (int col = 0; col < (int)size; col++)
                {
                    step.Add(row + (col * (int)size));
                }
            }

            return step;
        }
    }
}
