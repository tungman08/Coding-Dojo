using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    class AI : BasePlayer, IPlayer
    {
        public AI(string symbol) : this(symbol, AiLevel.Normal)
        {
        }

        public AI(string symbol, AiLevel level) : base(symbol)
        {
            Level = level;
        }

        public AiLevel Level { get; private set; }

        public Slot Play(OxGame game, Board board)
        {
            var cells = (int)board.Size * (int)board.Size;

            if (game.Slots.Where(s => s.IsX == null).Count() == cells)
            {
                // ai เริ่มเล่นก่อน
                var random = new Random();
                return new Slot { IsX = Symbol == "X",
                    Row = random.Next(0, (int)board.Size), Column = random.Next(0, (int)board.Size) };
            }

            // difficult level
            var slot = Level switch
            {
                AiLevel.Easy => EasyMode(game),
                AiLevel.Normal => NormalMode(game),
                _ => HardMode(game, Symbol),
            };

            return slot;
        }

        protected Slot EasyMode(OxGame game)
        {
            var random = new Random();
            int row = random.Next(0, (int)game.Size);
            int col = random.Next(0, (int)game.Size);

            while (game.Slots.Find(s => s.Row == row && s.Column == col).IsX != null)
            {
                row = random.Next(0, (int)game.Size);
                col = random.Next(0, (int)game.Size);
            } 

            return new Slot { IsX = Symbol == "X", Row = row, Column = col };
        }

        protected Slot NormalMode(OxGame game)
        {
            var stupid = ((int)game.Size * (int)game.Size) - ((int)game.Size != 3 ? (int)game.Size != 5 ? 5 : 3 : 1);

            return (game.Slots.Where(s => s.IsX == null).Count() >= stupid) ?
                EasyMode(game) :
                HardMode(game, Symbol);
        }

        // minimax algorithm
        protected Slot HardMode(OxGame game, string player)
        {
            return Minimax(game, player);
        }

        protected Slot Minimax(OxGame game, string player)
        {
            Slot best = null;
            var isX = player == "X";

            foreach (var slot in game.Slots.Where(s => s.IsX == null))
            {
                var clone = game.Clone();
                clone.TakeSlot(isX, slot.Row, slot.Column);

                if (clone.GameState == State.Playing)
                {
                    var minimax = Minimax(clone, TogglePlayer(player));
                    slot.Score = minimax.Score;
                }
                else
                {
                    switch (clone.GameState)
                    {
                        case State.Win:
                            slot.Score = (clone.GetWinner() == Symbol) ? 1 : -1;
                            break;
                        case State.Draw:
                            slot.Score = 0;
                            break;
                    }
                }

                if (best == null)
                {
                    best = new Slot { IsX = isX, Row = slot.Row, Column = slot.Column, Score = slot.Score };
                }

                if ((player == Symbol && slot.Score > best.Score) || 
                    (player != Symbol && slot.Score < best.Score))
                {
                    best = new Slot { IsX = isX, Row = slot.Row, Column = slot.Column, Score = slot.Score };
                }
            }

            return best;
        }

        protected string TogglePlayer(string player)
        {
            return player == "X" ? "O" : "X";
        }
    }

    enum AiLevel
    {
        Easy,
        Normal,
        Hard
    }
}
