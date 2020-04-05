using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    class AI : BasePlayer, IPlayer
    {
        public AI(string symbol) : this(symbol, 2)
        {
        }

        public AI(string symbol, int level) : base(symbol)
        {
            Level = level;
        }

        public int Level { get; private set; }

        public Slot Play(Board board, OxGame game)
        {
            var data = ConvertGameData(game.GameData);

            if (GetEmptySlot(data).Count == 9)
            {
                // ai เริ่มเล่นก่อน
                return CreateSlot(new Random().Next(0, 9));
            }

            // difficult level
            var index = Level switch
            {
                1 => EasyMode(data),
                2 => NormalMode(data),
                _ => HardMode(GetEmptySlot(data).Count, data, Symbol).Index,
            };

            return CreateSlot(index);
        }

        protected int EasyMode(List<string> gameData)
        {
            var data = GetEmptySlot(gameData);
            int index;
            do
            {
                index = new Random().Next(0, 9);

            } while (!data.Contains(index));

            return index;
        }

        protected int NormalMode(List<string> gameData)
        {
            var data = GetEmptySlot(gameData);

            return (data.Count >= 8) ?
                EasyMode(gameData) :
                HardMode(data.Count, gameData, Symbol).Index;
        }

        // minimax algorithm
        protected MinimaxScore HardMode(int depth, List<string> gameData, string player)
        {
            // คำนวณค่าช่องที่มีโอกาสชนะมากที่สุด
            var best = (player == Symbol) ?
                new MinimaxScore { Index = -1, Score = -100 } : // ai
                new MinimaxScore { Index = -1, Score = 100 }; // playper

            if (IsGameOver(depth, gameData))
            {
                return new MinimaxScore { Index = -1, Score = Evaluate(gameData) };
            }

            foreach (var index in GetEmptySlot(gameData))
            {
                gameData[index] = player;
                var score = HardMode(depth - 1, gameData, TogglePlayer(player)); // recursive function
                gameData[index] = string.Empty;
                score.Index = index;

                if (player == Symbol)
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

        protected int Evaluate(List<string> gameData)
        {
            // เสมอ
            var score = 0;

            // ai ชนะ
            if (GetWinner(gameData, Symbol))
            {
                score = 10;
            }
            // ผู้เล่นชนะ
            else if (GetWinner(gameData, TogglePlayer(Symbol)))
            {
                score = -10;
            }

            return score;
        }

        protected bool GetWinner(List<string> gameData, string player)
        {
            var winState = WinState(gameData);

            return winState.Contains($"{player}{player}{player}");
        }

        protected bool IsGameOver(int depth, List<string> gameData)
        {
            var winState = WinState(gameData);

            return depth == 0 || winState.Contains("XXX") || winState.Contains("OOO");
        }

        protected List<string> WinState(List<string> gameData)
        {
            // เงือนไขการชนะ
            return new List<string>
            {
                $"{gameData[0]}{gameData[1]}{gameData[2]}",
                $"{gameData[3]}{gameData[4]}{gameData[5]}",
                $"{gameData[6]}{gameData[7]}{gameData[8]}",
                $"{gameData[0]}{gameData[3]}{gameData[6]}",
                $"{gameData[1]}{gameData[4]}{gameData[7]}",
                $"{gameData[2]}{gameData[5]}{gameData[8]}",
                $"{gameData[0]}{gameData[4]}{gameData[8]}",
                $"{gameData[2]}{gameData[4]}{gameData[6]}",
            };
        }

        protected string TogglePlayer(string player)
        {
            return player == "X" ? "O" : "X";
        }
    }
}
