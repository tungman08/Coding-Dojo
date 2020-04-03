using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        public static int Play(string symbol, bool first)
        {
            bool again;
            do
            {
                var game = new BoardGame(symbol);
                var player = first;

                // game loop
                while (game.GetState() == BoardGame.State.Playing)
                {
                    // ai เป็นฝ่ายเล่นก่อน
                    if (!player)
                    {
                        game.AiTurn();
                        player = true;
                    }

                    game.HumanTurn();
                    game.AiTurn();
                }

                if (game.GetState() == BoardGame.State.Win)
                {
                    // แสดงผู้ชนะ
                    Display.Winner($"{game.GetWinnerName()} ({game.GetWinner()})");
                }
                else
                {
                    // เสมอ
                    Display.Draw();
                }

                // เล่นใหม่อีกครั้ง
                Console.WriteLine();
                again = Input.Confirm("Play again?:");
            } while (again);

            return 0;
        }
    }
}
