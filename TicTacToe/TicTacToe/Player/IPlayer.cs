using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Game;

namespace TicTacToe.Player
{
    interface IPlayer
    {
        Slot Play(OxGame game, Board board);
    }
}
