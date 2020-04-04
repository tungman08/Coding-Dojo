using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    interface IBoardGame
    {
        string GetWinner();

        bool TakeSlot(bool isX, int row, int col);
    }
}
