using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class GameScore
    {
        public GameScore()
        {
            Win = 0;
            Lose = 0;
            Draw = 0;
        }

        public int Win { get; private set; }

        public int Lose { get; private set; }

        public int Draw { get; private set; }

        public int Total => Win + Lose + Draw;

        public void AddWin()
        {
            Win += 1;
        }

        public void AddLose()
        {
            Lose += 1;
        }

        public void AddDraw()
        {
            Draw += 1;
        }

        public void Summary()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Summary Score:");
            Console.ResetColor();
            Console.WriteLine("Play: {0,8}", Total);
            Console.WriteLine("Win:  {0,8}", Win);
            Console.WriteLine("Lose: {0,8}", Lose);
            Console.WriteLine("Draw: {0,8}", Draw);
            Console.WriteLine();
        }
    }
}
