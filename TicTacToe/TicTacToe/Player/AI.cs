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
                AiLevel.Easy => EasyMode(board.Size, game.Slots),
                AiLevel.Normal => NormalMode(board.Size, game.Slots),
                _ => HardMode(board.Size, game.Slots, Symbol),
            };

            return slot;
        }

        protected Slot EasyMode(BoardSize size, List<Slot> slots)
        {
            var random = new Random();
            int row = random.Next(0, (int)size);
            int col = random.Next(0, (int)size);

            while (slots.Single(s => s.Row == row && s.Column == col).IsX != null)
            {
                row = random.Next(0, (int)size);
                col = random.Next(0, (int)size);
            } 

            return new Slot { IsX = Symbol == "X", Row = row, Column = col };
        }

        protected Slot NormalMode(BoardSize size, List<Slot> slots)
        {
            var stupid = ((int)size * (int)size) - ((int)size / 3);

            return (slots.Where(s => s.IsX == null).Count() >= stupid) ?
                EasyMode(size, slots) :
                HardMode(size, slots, Symbol);
        }

        // minimax algorithm
        protected Slot HardMode(BoardSize size, List<Slot> slots, string player)
        {
            // กำลังปรับปรุง

            var random = new Random();
            int row = random.Next(0, (int)size);
            int col = random.Next(0, (int)size);

            while (slots.Single(s => s.Row == row && s.Column == col).IsX != null)
            {
                row = random.Next(0, (int)size);
                col = random.Next(0, (int)size);
            }

            return new Slot { IsX = Symbol == "X", Row = row, Column = col };
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
