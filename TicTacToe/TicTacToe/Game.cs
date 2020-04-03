using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        public static int Play(string symbol, bool first)
        {
            int win = 0;
            int lose = 0;
            int draw = 0;

            bool again;
            do
            {
                var game = new BoardGame(symbol);
                var playerStart = first;

                // game loop
                while (game.GetState() == BoardGame.State.Playing)
                {
                    // ai เป็นฝ่ายเล่นก่อน
                    if (!playerStart)
                    {
                        game.AiTurn();
                        playerStart = true;
                    }

                    game.PlayerTurn();
                    game.AiTurn();
                }

                if (game.GetState() == BoardGame.State.Win)
                {
                    // นับแต้ม
                    if (game.GetWinnerName() == "You")
                    {
                        win++;
                    }
                    else
                    {
                        lose++;
                    }

                    // แสดงผู้ชนะ
                    Display.Winner($"{game.GetWinnerName()} ({game.GetWinner()})");
                }
                else
                {
                    // เสมอ
                    draw++;
                    Display.Draw();
                }

                // เล่นใหม่อีกครั้ง
                Console.WriteLine();
                again = Input.Confirm("Play again?:");
            } while (again);

            Display.Summary(win, lose, draw);

            return 0;
        }
    }
}
