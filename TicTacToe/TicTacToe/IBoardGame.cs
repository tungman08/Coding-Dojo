using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    interface IBoardGame
    {
        string GetWinner();

        bool TakeSlot(bool isX, int row, int column);
    }
}
