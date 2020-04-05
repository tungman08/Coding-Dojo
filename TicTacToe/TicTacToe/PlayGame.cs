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
        public int Start(string selected, int level, bool first)
        {
            var score = new GameScore();

            do
            {
                var symbol = GetSymbolForPlayer(selected);
                var turn = first ? symbol.Item1 : symbol.Item2;

                var game = new OxGame();
                var board = new Board();
                var human = new Human(symbol.Item1);
                var ai = new AI(symbol.Item2, level);

                // game loop
                while (game.GameState == State.Playing)
                {
                    if (turn == human.Symbol)
                    {
                        game.CheckError(HumanTurn(game, board, human));
                        turn = ChangeTurn(turn);
                    }
                    else
                    {
                        game.CheckError(AiTurn(game, board, ai));
                        turn = ChangeTurn(turn);
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

        protected bool HumanTurn(OxGame game, Board board, Human player)
        {
            var result = false;

            // ตรวจสอบว่าจบเกมแล้วหรือยัง
            if (game.GameState == State.Playing)
            {
                var slot = player.Play(board, game);
                result = game.TakeSlot(slot.IsX, slot.Row, slot.Column);
                board.Render(game.GameData);
            }

            return result;
        }

        protected bool AiTurn(OxGame game, Board board, AI player)
        {
            var result = false;

            // ตรวจสอบว่าจบเกมแล้วหรือยัง
            if (game.GameState == State.Playing)
            {
                var slot = player.Play(board, game);
                result = game.TakeSlot(slot.IsX, slot.Row, slot.Column);
                board.Render(game.GameData);
            }

            return result;
        }

        protected bool PlayAgain()
        {
            Console.WriteLine();
            return Input.Confirm("Play again?");
        }

        protected string ChangeTurn(string turn)
        {
            return turn == "X" ? "O" : "X";
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
                return Tuple.Create(symbols.Single(a => a == symbols[index]),
                    symbols.Single(a => a != symbols[index]));
            }

            return Tuple.Create(symbols.Single(a => a == symbol),
                symbols.Single(a => a != symbol));
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
