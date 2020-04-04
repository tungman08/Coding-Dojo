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

        protected List<int> GetEmptySlot(List<string> gameData)
        {
            // ส่ง index ของช่องที่ว่างทั้งหมด
            var empty = new List<int>();

            for (var i = 0; i < gameData.Count; i++)
            {
                if (gameData[i] == string.Empty)
                {
                    empty.Add(i);
                }
            }

            return empty;
        }

        protected List<string> ConvertGameData(string[,] gameData)
        {
            var data = new List<string>();

            for (var row = 0; row < gameData.GetLength(0); row++) // 0 = height
            {
                for (var col = 0; col < gameData.GetLength(1); col++) // 1 = width
                {
                    data.Add(gameData[row, col].Trim());
                }
            }

            return data;
        }

        protected Slot CreateSlot(int index)
        {
            var row = index / 3;
            var col = index % 3;

            return new Slot { IsX = IsX, Row = row, Column = col };
        }
    }
}
