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

        public Slot Play(Board board, OxGame game)
        {
            ConsoleKey key;
            Console.CursorVisible = false;

            // หาช่องที่ว่างแรกที่เจอเพื่อวาง cursor
            var data = ConvertGameData(game.GameData);
            CurrentCell = GetEmptySlot(data).First();
            board.PutSymbol(Symbol, CurrentCell);

            do
            {
                // กดปุ่มลูกศรเพื่อเลื่อน cursor ไปยังช่องที่ว่างอยู่
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp(board, data);
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown(board, data);
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveLeft(board, data);
                        break;
                    case ConsoleKey.RightArrow:
                        MoveRight(board, data);
                        break;
                }
            } while (key != ConsoleKey.Enter);


            Console.CursorVisible = true;

            // ผู้เล่นใส่ค่าในช่องที่เลือก
            return CreateSlot(CurrentCell);
        }

        protected void MoveRight(Board board, List<string> gameData)
        {
            var point = 1;

            while (CurrentCell + point < gameData.Count)
            {
                if (GetEmptySlot(gameData).Contains(CurrentCell + point))
                {
                    board.ClearSymbol(CurrentCell);
                    CurrentCell += point;
                    board.PutSymbol(Symbol, CurrentCell);

                    break;
                }

                point++;
            }
        }

        protected void MoveLeft(Board board, List<string> gameData)
        {
            var point = 1;

            while (CurrentCell - point >= 0)
            {
                if (GetEmptySlot(gameData).Contains(CurrentCell - point))
                {
                    board.ClearSymbol(CurrentCell);
                    CurrentCell -= point;
                    board.PutSymbol(Symbol, CurrentCell);

                    break;
                }

                point++;
            }
        }

        protected void MoveDown(Board board, List<string> gameData)
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentCell);
            var point = 1;

            while (current + point < gameData.Count)
            {
                if (GetEmptySlot(gameData).Contains(step[current + point]))
                {
                    board.ClearSymbol(CurrentCell);
                    CurrentCell = step[current + point];
                    board.PutSymbol(Symbol, CurrentCell);

                    break;
                }

                point++;
            }
        }

        protected void MoveUp(Board board, List<string> gameData)
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentCell);
            var point = 1;

            while (current - point >= 0)
            {
                if (GetEmptySlot(gameData).Contains(step[current - point]))
                {
                    board.ClearSymbol(CurrentCell);
                    CurrentCell = step[current - point];
                    board.PutSymbol(Symbol, CurrentCell);

                    break;
                }

                point++;
            }
        }
    }
}
