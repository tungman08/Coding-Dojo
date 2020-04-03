using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        public static int Play(string symbol, bool first)
        {
            var game = new BoardGame(symbol);
            var play = first;

            while (game.GetState() == BoardGame.State.Playing)
            {
                if (!play)
                {
                    game.AiTurn();
                    play = true;
                }

                game.HumanTurn();
                game.AiTurn();
            }

            if (game.GetState() == BoardGame.State.Win) 
            {
                Display.Winner($"{game.GetWinnerName()} ({game.GetWinner()})");
            }
            else
            {
                Display.Draw();
            }

            return 0;
        }
    }
}
