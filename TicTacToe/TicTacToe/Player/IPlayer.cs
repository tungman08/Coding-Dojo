using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    interface IPlayer
    {
        Slot Play(Board board, OxGame game);
    }
}
