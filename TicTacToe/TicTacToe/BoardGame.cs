using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe
{
    class BoardGame : IBoardGame
    {
        private readonly List<int> _board;
        private readonly string _player;
        private readonly string _ai;
        private readonly int _row;

        public BoardGame(string symbol)
        {
            _board = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            _player = symbol;
            _ai = symbol == "X" ? "O" : "X";
            _row = Console.CursorTop; // cursor เริ่มต้นของตาราง

            Display.Table();
            Render();
        }
        
        public string GetWinner()
        {
            // เงือนไขการชนะ
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
                return _player;
            }
            else if (winState.Contains("-1-1-1"))
            {
                return _ai;
            }

            // ยังไม่รู้ผลให้ส่งค่าว่าง
            return string.Empty;
        }

        public bool TakeSlot(bool isX, int row, int col)
        {
            // ไม่ได้ใช้
            throw new NotImplementedException();
        }

        public string GetWinnerName()
        {
            return GetWinner() == _player ? "You" : "Com";
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

        public void PlayerTurn()
        {
            ConsoleKey key;
            Console.CursorVisible = false;

            // หาช่องที่ว่างแรกที่เจอเพื่อวาง cursor
            CurrentSlot = GetEmptySlot(_board).First();
            DrawSymbol();

            do
            {
                // กดปุ่มลูกศรเพื่อเลื่อน cursor ไปยังช่องที่ว่างอยู่
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

            // ผู้เล่นใส่ค่าในช่องที่เลือก
            Console.CursorVisible = true;
            _board[CurrentSlot] = 1;
            Render();
        }

        public void AiTurn()
        {
            // ตรวจสอบว่าจบเกมแล้วหรือยัง
            if (GetState() == State.Playing)
            {
                Console.CursorVisible = false;
                System.Threading.Thread.Sleep(300);

                if (GetEmptySlot(_board).Count == 9)
                {
                    // ai เริ่มเล่นก่อน
                    var random = new Random();
                    _board[random.Next(0, 8)] = -1;
                }
                else {
                    // เลือกช่องที่มี score มากที่สุด
                    var slot = MiniMax(_board, GetEmptySlot(_board).Count, -1).Slot;
                    _board[slot] = -1;
                }

                Render();
                System.Threading.Thread.Sleep(100);
                Console.CursorVisible = true;
            }
        }

        protected MiniMaxScore MiniMax(List<int> board, int depth, int player)
        {
            // คำนวณค่าช่องที่มีโอกาสชนะมากที่สุด
            var best = player == -1 ? 
                new MiniMaxScore { Slot = -1, Score = -1000 } : 
                new MiniMaxScore { Slot = -1, Score = 1000 };

            if (depth == 0 || GetState() != State.Playing)
            {
                    var score = Evaluate();
                    return new MiniMaxScore { Slot = -1, Score = score };
            }

            foreach (var slot in GetEmptySlot(board))
            {
                board[slot] = player;
                var score = MiniMax(board, depth - 1, -player); // recursive function
                board[slot] = 0;
                score.Slot = slot;

                if (player == -1)
                {
                    // max value
                    if (score.Score > best.Score)
                        best = score;
                }
                else
                {
                    // min value
                    if (score.Score < best.Score)
                        best = score;
                }
            }

            return best;
        }

        protected int Evaluate()
        {
            // เสมอ
            var score = 0;

            if (GetWinner() == _player)
            {
                // ผู้เล่นชนะ
                score = -1;
            }
            else if (GetWinner() == _ai)
            {
                // ai ชนะ
                score = 1;
            }

            return score;
        }

        protected bool TakeSlot(int slot)
        {
            // ตรวจสอบว่าช่องนี้ว่างหรือไม่
            return GetEmptySlot(_board).Contains(slot);
        }

        protected List<int> GetEmptySlot(List<int> board)
        {
            // ส่ง index ของช่องที่ว่างทั้งหมด
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

        // cursor สำหรับเลือกช่องลง
        protected int CurrentSlot { get; set; }

        protected void MoveRight()
        {
            var point = 1;

            while (CurrentSlot + point < _board.Count)
            {
                if (TakeSlot(CurrentSlot + point))
                {
                    ClearSymbol();
                    CurrentSlot += point;
                    DrawSymbol();

                    break;
                }

                point++;
            }
        }

        protected void MoveLeft()
        {
            var point = 1;

            while (CurrentSlot - point >= 0)
            {
                if (TakeSlot(CurrentSlot - point))
                {
                    ClearSymbol();
                    CurrentSlot -= point;
                    DrawSymbol();

                    break;
                }

                point++;
            }
        }

        protected void MoveUp()
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentSlot);
            var point = 1;

            while (current - point >= 0)
            {
                if (TakeSlot(step[current - point]))
                {
                    ClearSymbol();
                    CurrentSlot = step[current - point];
                    DrawSymbol();

                    break;
                }

                point++;
            }
        }

        protected void MoveDown()
        {
            var step = new List<int> { 0, 3, 6, 1, 4, 7, 2, 5, 8 };
            var current = step.IndexOf(CurrentSlot);
            var point = 1;

            while (current + point < _board.Count)
            {
                if (TakeSlot(step[current + point]))
                {
                    ClearSymbol();
                    CurrentSlot = step[current + point];
                    DrawSymbol();

                    break;
                }

                point++;
            }
        }

        protected void DrawSymbol()
        {
            var point = CurrentSlot.ToPoint(_row);

            Console.SetCursorPosition(point.Y, point.X);
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write($" { _player } ");
            Console.ResetColor();
        }

        protected void ClearSymbol()
        {
            var point = CurrentSlot.ToPoint(_row);

            Console.SetCursorPosition(point.Y, point.X);
            Console.Write("   ");
        }

        protected void Render()
        {
            for (var i = 0; i < _board.Count; i++)
            {
                var point = i.ToPoint(_row);

                if (_board[i] != 0)
                {
                    var symbol = _board[i] == 1 ? $" {_player } " : $" {_ai } ";
                    Console.SetCursorPosition(point.Y, point.X);

                    if (symbol.Trim() == "X")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }

                    Console.Write(symbol);
                    Console.ResetColor();
                }
            }

            Console.SetCursorPosition(0, _row + 9);
        }

        protected class MiniMaxScore
        {
            public int Slot { get; set; }

            public int Score { get; set; }
        }

        public enum State 
        { 
            Playing,
            Win,
            Draw
        }
    }
}
