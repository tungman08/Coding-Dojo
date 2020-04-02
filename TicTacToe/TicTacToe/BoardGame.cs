using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class BoardGame : IBoardGame
    {

        private readonly List<int> _board;
        private readonly bool _isX;
        private readonly int _top;

        public BoardGame(bool isX, int top)
        {
            _board = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _isX = isX;
            _top = top;

            Display.Table();
            Render();
        }

        protected Point CurrentCell { get; set; }

        public string GetWinner()
        {
            var winState = new List<int>[] 
            {
                new List<int> { _board[0], _board[1], _board[2] },
                new List<int> { _board[3], _board[4], _board[5] },
                new List<int> { _board[6], _board[7], _board[8] },
                new List<int> { _board[0], _board[3], _board[6] },
                new List<int> { _board[1], _board[4], _board[7] },
                new List<int> { _board[2], _board[5], _board[8] },
                new List<int> { _board[0], _board[4], _board[8] },
                new List<int> { _board[2], _board[4], _board[6] },
            };

            foreach(var win in winState)
            {
                if (win[0] != 0 && win[0] == win[1] && win[1] == win[2])
                {
                    if (_isX)
                    {
                        return win[0] == 1 ? "X" : "O";
                    }
                    else
                    {
                        return win[0] == 1 ? "O" : "X";
                    }
                }
            }

            return string.Empty;
        }

        public bool TakeSlot(bool isX, int row, int column)
        {
            var index = (row * 3) + column;

            return (index < _board.Count && index >= 0) ? _board[index] == 0 : false;
        }

        public string GetWinnerWithName()
        {
            string name;
            if (_isX)
            {
                name = (GetWinner() == "X") ? "You" : "Com";
            }
            else
            {
                name = (GetWinner() == "X") ? "Com" : "You";
            }

            return name;
        }

        public void AiTurn()
        {
            var point = InitialCell();
            _board[(point.X * 3) + point.Y] = -1;

            System.Threading.Thread.Sleep(200);
            Render();
        }

        public void HumanTurn()
        {
            ConsoleKey key;
            Console.CursorVisible = false;

            CurrentCell = InitialCell();
            DrawCell();

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
            _board[(CurrentCell.X * 3) + CurrentCell.Y] = 1;
            Render();
        }

        public int EmptyCell()
        {
            return _board.Where(c => c == 0).Count();
        }

        protected Point InitialCell()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                if (_board[i] == 0)
                {
                    var row = i / 3;
                    var col = i % 3;

                    return new Point(row, col);
                }
            }

            return new Point(0, 0);
        }

        protected void MoveRight()
        {
            var point = 1;
            while (CurrentCell.Y + point < 3)
            {
                if (TakeSlot(_isX, CurrentCell.X, CurrentCell.Y + point))
                {
                    ClearCell();
                    CurrentCell = new Point(CurrentCell.X, CurrentCell.Y + point);
                    DrawCell();

                    break;
                }

                point++;
            }
        }

        protected void MoveLeft()
        {
            var point = 1;
            while (CurrentCell.Y - point >= 0)
            {
                if (TakeSlot(_isX, CurrentCell.X, CurrentCell.Y - point))
                {
                    ClearCell();
                    CurrentCell = new Point(CurrentCell.X, CurrentCell.Y - point);
                    DrawCell();

                    break;
                }

                point++;
            }
        }

        protected void MoveUp()
        {
            var point = 1;
            while (CurrentCell.X - point >= 0)
            {
                if (TakeSlot(_isX, CurrentCell.X - point, CurrentCell.Y))
                {
                    ClearCell();
                    CurrentCell = new Point(CurrentCell.X - point, CurrentCell.Y);
                    DrawCell();

                    break;
                }

                point++;
            }
        }

        protected void MoveDown()
        {
            var point = 1;
            while (CurrentCell.X + point < 3)
            {
                if (TakeSlot(_isX, CurrentCell.X + point, CurrentCell.Y))
                {
                    ClearCell();
                    CurrentCell = new Point(CurrentCell.X + point, CurrentCell.Y);
                    DrawCell();

                    break;
                }

                point++;
            }
        }

        protected void DrawCell()
        {
            Console.SetCursorPosition((CurrentCell.Y * 4) + 2, _top + (CurrentCell.X * 2) + 2);
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.Write(_isX ? "X" : "O");
            Console.ResetColor();
        }

        protected void ClearCell()
        {
            Console.SetCursorPosition((CurrentCell.Y * 4) + 2, _top + (CurrentCell.X * 2) + 2);
            Console.Write(" ");
        }

        protected void Render()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                var row = i / 3;
                var col = i % 3;

                if (_board[i] != 0)
                {
                    Console.SetCursorPosition((col * 4) + 2, _top + (row * 2) + 2);

                    if (_isX)
                    {
                        Console.Write(_board[i] == 1 ? "X" : "O");
                    }
                    else
                    {
                        Console.Write(_board[i] == 1 ? "O" : "X");
                    }
                }
            }

            Console.SetCursorPosition(0, _top + 9);
        }
    }
}
