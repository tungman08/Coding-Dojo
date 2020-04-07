using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    abstract class BasePlayer
    {
        public BasePlayer(string symbol)
        {
            Symbol = symbol;
        }

        public string Symbol { get; private set; }

        public bool IsX => Symbol == "X";
    }
}
