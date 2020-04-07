using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    class Slot
    {
        public Slot()
        {
            Score = 0;
        }

        public bool? IsX { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public string Value => (IsX != null) ? (bool)IsX ? "X" : "O" : string.Empty;

        public int Score { get; set; }
    }
}
