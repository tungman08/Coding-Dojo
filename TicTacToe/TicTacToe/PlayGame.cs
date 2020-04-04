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
        public int Start(string selectedOption, bool humanFirst)
        {
            var score = new GameScore();

            do
            {
                var game = new OxGame();
                var board = new Board();
                var symbol = GetSymbol(selectedOption);
                var human = new Human(symbol.Item1);
                var ai = new AI(symbol.Item2);

                // game loop
                while (game.GameState == State.Playing)
                {
                    board.Render(game.GameData);

                    // ai เป็นฝ่ายเล่นก่อน
                    if (!humanFirst)
                    {
                        humanFirst = true;
                        AiTurn(game, board, ai);
                    }

                    HumanTurn(game, board, human);
                    AiTurn(game, board, ai);
                }

                if (game.GameState != State.Playing)
                {
                    // นับสถิติการแข่งขัน
                    AddSore(game, score, human, ai);
                }

            } while (PlayAgain());

            return score.Summary();
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

        protected Tuple<string, string> GetSymbol(string symbol)
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
    }
}
