using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace TicTacToe
{
    static class ExtensionMethods
    {
        public static Point ToPoint(this int slot, int top)
        {
            var row = slot / 3;
            var col = slot % 3;

            return new Point(top + (row * 2) + 2, (col * 4) + 2);
        }
    }
}
