using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    class MinimaxScore
    {
        public int Index { get; set; }

        public int Score { get; set; }
    }

    class Slot
    {
        public bool IsX { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }
    }

    enum State
    {
        Win,
        Draw,
        Playing
    }
}
