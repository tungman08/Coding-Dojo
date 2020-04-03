using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class BoardGame
    {
        private readonly List<int> _board;
        private readonly string _human;
        private readonly string _ai;
        private readonly int _top;

        public BoardGame(string symbol)
        {
            _board = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _human = symbol;
            _ai = symbol == "X" ? "O" : "X";
            _top = Console.CursorTop;

            Display.Table();
            Render();
        }

        protected int CurrentSlot { get; set; }

        public string GetWinner()
        {
            var winState = new List<string>
            {
                $"{_board[0]}{_board[1]}{_board[2]}",
                $"{_board[3]}{_board[4]}{_board[5]}",
                $"{_board[6]}{_board[7]}{_board[8]}",
                $"{_board[0]}{_board[3]}{_board[6]}",
                $"{_board[1]}{_board[4]}{_board[7]}",
                $"{_board[2]}{_board[5]}{_board[8]}",
                $"{_board[0]}{_board[4]}{_board[8]}",
                $"{_board[2]}{_board[4]}{_board[6]}",
            };

            if (winState.Contains("111"))
            {
                return _human;
            }
            else if (winState.Contains("-1-1-1"))
            {
                return _ai;
            }

            return string.Empty;
        }

        public string GetWinnerName()
        {
            return GetWinner() == _human ? "You" : "Com";
        }

        public State GetState()
        {
            if (GetWinner() != string.Empty)
            {
                return State.Win;
            }
            else if (GetEmptySlot(_board).Count() == 0)
            {
                return State.Draw;
            }

            return State.Playing;
        }

        public void HumanTurn()
        {
            ConsoleKey key;
            Console.CursorVisible = false;

            CurrentSlot = GetEmptySlot(_board).First();
            DrawSymbol();

            do
            {
                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        MoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        MoveDown();
                        break;
                    case ConsoleKey.LeftArrow:
                        MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        MoveRight();
                        break;
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;
            _board[CurrentSlot] = 1;
            Render();
        }

        public void AiTurn()
        {
            if (GetState() == State.Playing)
            {
                Console.CursorVisible = false;

                if (GetEmptySlot(_board).Count == 9)
                {
                    var random = new Random();
                    _board[random.Next(0, 8)] = -1;
                }
                else {
                    var slot = MiniMax(_board, GetEmptySlot(_board).Count, -1);
                    _board[slot[0]] = -1;
                }

                System.Threading.Thread.Sleep(100);
                Console.CursorVisible = true;
                Render();
            }
        }

        protected List<int> MiniMax(List<int> board, int depth, int player)
        {
            var best = player == -1 ? new List<int> { -1, -1000 } : new List<int> { -1, 1000 };

            if (depth == 0 || GetState() != State.Playing)
            {
                    var score = Evalute();
                    return new List<int> { -1, score };
            }

            foreach (var slot in GetEmptySlot(board))
            {
                board[slot] = player;
                var score = MiniMax(board, depth - 1, -player);
                board[slot] = 0;
                score[0] = slot;

                if (player == -1)
                {
                    if (score[1] > best[1])
                        best = score;
                }
                else
                {
                    if (score[1] < best[1])
                        best = score;
                }
            }

            return best;
        }

        protected int Evalute()
        {
            var score = 0;

            if (GetWinner() == _human)
            {
                score = -1;
            }
            else if (GetWinner() == _ai)
            {
                score = 1;
            }

            return score;
        }

        protected bool TakeSlot(int slot)
        {
            return GetEmptySlot(_board).Contains(slot);
        }

        protected List<int> GetEmptySlot(List<int> board)
        {
            var empty = new List<int>();

            for (var i = 0; i < board.Count; i++)
            {
                if (_board[i] == 0)
                {
                    empty.Add(i);
                }    
            }

            return empty;
        }

        protected void MoveRight()
        {
            var point = 1;
            var found = false;

            while (!found && CurrentSlot + point < _board.Count)
            {
                if (TakeSlot(CurrentSlot + point))
                {
                    ClearSymbol();
                    CurrentSlot += point;
                    DrawSymbol();

                    found = true;
                }

                point++;
            }
        }

        protected void MoveLeft()
        {
            var point = 1;
            var found = false;

            while (!found && CurrentSlot - point >= 0)
            {
                if (TakeSlot(CurrentSlot - point))
                {
                    ClearSymbol();
                    CurrentSlot -= point;
                    DrawSymbol();

                    found = true;
                }

                point++;
            }
        }

        protected void MoveUp()
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentSlot);
            var point = 1;
            var found = false;

            while (!found && current - point >= 0)
            {
                if (TakeSlot(step[current - point]))
                {
                    ClearSymbol();
                    CurrentSlot = step[current - point];
                    DrawSymbol();

                    found = true;
                }

                point++;
            }
        }

        protected void MoveDown()
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentSlot);
            var point = 1;
            var found = false;

            while (!found && current + point < _board.Count)
            {
                if (TakeSlot(step[current + point]))
                {
                    ClearSymbol();
                    CurrentSlot = step[current + point];
                    DrawSymbol();

                    found = true;
                }

                point++;
            }
        }
        protected Point ToPoint(int slot)
        {
            var row = slot / 3;
            var col = slot % 3;

            return new Point(_top + (row * 2) + 2, (col * 4) + 2);
        }

        protected void DrawSymbol()
        {
            var point = ToPoint(CurrentSlot);

            Console.SetCursorPosition(point.Y, point.X);
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write(_human);
            Console.ResetColor();
        }

        protected void ClearSymbol()
        {
            var point = ToPoint(CurrentSlot);

            Console.SetCursorPosition(point.Y, point.X);
            Console.Write(" ");
        }

        protected void Render()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                var point = ToPoint(i);

                if (_board[i] != 0)
                {
                    Console.SetCursorPosition(point.Y, point.X);
                    Console.Write(_board[i] == 1 ? _human : _ai);
                }
            }

            Console.SetCursorPosition(0, _top + 9);
        }

        public enum State 
        { 
            Playing,
            Win,
            Draw
        }
    }
}
