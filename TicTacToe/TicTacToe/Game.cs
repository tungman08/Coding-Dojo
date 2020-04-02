using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class Game
    {
        public static int Play(bool isX, bool first)
        {
            var top = Console.CursorTop;
            var game = new BoardGame(isX, top);
            var humanFirst = first;

            while (game.EmptyCell() > 0 && game.GetWinner() == string.Empty)
            {
                if (!humanFirst)
                {
                    game.AiTurn();
                    humanFirst = true;
                }

                game.HumanTurn();
                game.AiTurn();
            }

            Display.Winner(game.GetWinnerWithName());

            return 0;
        }
    }
}
