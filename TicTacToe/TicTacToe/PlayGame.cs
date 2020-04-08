using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.ConsoleUtil;
using TicTacToe.Game;
using TicTacToe.Player;

namespace TicTacToe
{
    class PlayGame
    {
        public int Start(string selected, BoardSize size, AiLevel level, bool first)
        {
            var score = new GameScore();

            do
            {
                var symbol = GetSymbolForPlayer(selected);
                var player = first ? symbol.Item1 : symbol.Item2;

                var game = new OxGame(size);
                var board = new Board(size);
                var human = new Human(symbol.Item1);
                var ai = new AI(symbol.Item2, level);

                // game loop
                while (game.GameState == State.Playing)
                {
                    if (player == human.Symbol)
                    {
                        // ตาผู้เล่น
                        game.CheckError(PlayerTurn(game, board, human));
                        player = ChangeTurn(player);
                    }
                    else
                    {
                        // ตา ai
                        game.CheckError(PlayerTurn(game, board, ai));
                        player = ChangeTurn(player);
                    }
                }

                if (game.GameState == State.Win || game.GameState == State.Draw)
                {
                    // นับสถิติการแข่งขัน
                    AddSore(game, score, human, ai);
                }
                else if (game.GameState == State.Error)
                {
                    // แสดงข้อผิดพลาด
                    DisplayError();
                }

            } while (PlayAgain());

            score.Summary();

            return 0;
        }

        protected bool PlayerTurn(OxGame game, Board board, IPlayer player)
        {
            var slot = player.Play(game, board);
            var result = game.TakeSlot(slot);
            board.Render(game.Slots);

            return result;
        }

        protected bool PlayAgain()
        {
            Console.WriteLine();
            return Input.Confirm("Play again?");
        }

        protected string ChangeTurn(string player)
        {
            return player == "X" ? "O" : "X";
        }

        protected void AddSore(OxGame game, GameScore score, Human human, AI ai)
        {
            switch (game.GameState)
            {
                case State.Win:
                    if (game.GetWinner() == human.Symbol)
                    {
                        score.AddWin();
                        DisplayResult($"You({human.Symbol}) Win");
                    }
                    else
                    {
                        score.AddLose();
                        DisplayResult($"Com({ai.Symbol}) Win");
                    }
                    break;
                case State.Draw:
                    score.AddDraw();
                    DisplayResult("Draw");
                    break;
            }
        }

        protected Tuple<string, string> GetSymbolForPlayer(string symbol)
        {
            var symbols = new List<string> { "X", "O" };

            if (symbol == "R")
            {
                var random = new Random();
                var index = random.Next(0, 2);
                return Tuple.Create(symbols.Find(a => a == symbols[index]),
                    symbols.Find(a => a != symbols[index]));
            }

            return Tuple.Create(symbols.Find(a => a == symbol),
                symbols.Find(a => a != symbol));
        }

        protected void DisplayResult(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"*** {message} ***");
            Console.ResetColor();
        }

        protected void DisplayError()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"*** Error ***");
            Console.ResetColor();
        }
    }
}
