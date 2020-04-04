using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe.Game
{
    class OxGame : IBoardGame
    {
        private readonly List<string> _gameData;

        public OxGame()
        {
            _gameData = new List<string>
            {
                string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty
            };

            ThisTurn = string.Empty;
        }

        public string[,] GameData => GetGameData();

        public State GameState => GetEmptySlot().Count > 0 ? GetWinner() != string.Empty ?
            State.Win : State.Playing : State.Draw;

        public string ThisTurn { get; private set; }

        public string GetWinner()
        {
            // เงือนไขการชนะ
            var winState = new List<string>
            {
                $"{_gameData[0]}{_gameData[1]}{_gameData[2]}",
                $"{_gameData[3]}{_gameData[4]}{_gameData[5]}",
                $"{_gameData[6]}{_gameData[7]}{_gameData[8]}",
                $"{_gameData[0]}{_gameData[3]}{_gameData[6]}",
                $"{_gameData[1]}{_gameData[4]}{_gameData[7]}",
                $"{_gameData[2]}{_gameData[5]}{_gameData[8]}",
                $"{_gameData[0]}{_gameData[4]}{_gameData[8]}",
                $"{_gameData[2]}{_gameData[4]}{_gameData[6]}",
            };

            if (winState.Contains("XXX"))
            {
                return "X";
            }
            else if (winState.Contains("OOO"))
            {
                return "O";
            }

            // ยังไม่จบเกมส่ง empty string (ขอไม่ตามโจทย์)
            return string.Empty;
        }

        public bool TakeSlot(bool isX, int row, int col)
        {
            var player = isX ? "X" : "O";
            var index = (row * 3) + col;

            // ตรวจสอบเกมยังไม่จบและช่องนี้ว่าง
            if (GameState == State.Playing && GetEmptySlot().Contains(index))
            {
                // ตาแรก
                if (ThisTurn == string.Empty)
                {
                    ThisTurn = player;
                }

                // เป็นตาของผู้เล่นหรือไม่
                if (ThisTurn == player)
                {
                    _gameData[index] = player;
                    ChangeTurn();

                    return true;
                }

                return false;
            }

            return false;
        }

        protected List<int> GetEmptySlot()
        {
            // ส่ง index ของช่องที่ว่างทั้งหมด
            var empty = new List<int>();

            for (var i = 0; i < _gameData.Count; i++)
            {
                if (_gameData[i] == string.Empty)
                {
                    empty.Add(i);
                }
            }

            return empty;
        }

        protected void ChangeTurn()
        {
            ThisTurn = (ThisTurn == "X") ? "O" : "X";
        }

        protected string[,] GetGameData()
        {
            return new string[,]
            {
                { _gameData[0], _gameData[1], _gameData[2] },
                { _gameData[3], _gameData[4], _gameData[5] },
                { _gameData[6], _gameData[7], _gameData[8] }
            };
        }
    }
}
