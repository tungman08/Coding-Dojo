using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.Game
{
    class OxGame : IBoardGame
    {
        private bool _error = false;

        public OxGame() : this(BoardSize.Small)
        {
        }

        public OxGame(BoardSize size)
        {
            Size = size;
            Slots = InitialBoard();
            ThisTurn = string.Empty;
        }

        public List<Slot> Slots { get; private set; }

        public State GameState => GetGameState();

        public BoardSize Size { get; private set; }

        public string ThisTurn { get; private set; }

        public string GetWinner()
        {
            // เงือนไขการชนะ
            var winState = GetWinState();
            var length = (int)Size == 3 ? 3 : 4;

            if ((winState.Contains("XXX") && length == 3) || (winState.Contains("XXXX") && length == 4))
            {
                return "X";
            }
            else if ((winState.Contains("OOO") && length == 3) || (winState.Contains("OOOO") && length == 4))
            {
                return "O";
            }

            // ยังไม่จบเกมส่ง empty string
            return string.Empty;
        }

        public bool TakeSlot(bool isX, int row, int col)
        {
            var player = isX ? "X" : "O";
            var slot = Slots.Single(s => s.Row == row && s.Column == col);

            // ตรวจสอบเกมยังไม่จบและช่องนี้ว่าง
            if (GameState == State.Playing && slot.IsX == null)
            {
                // ตาแรก
                if (ThisTurn == string.Empty)
                {
                    ThisTurn = player;
                }

                // เป็นตาของผู้เล่นหรือไม่
                if (ThisTurn == player)
                {
                    slot.IsX = player == "X";
                    ChangeTurn();

                    return true;
                }

                return false;
            }

            return false;
        }

        public bool TakeSlot(Slot slot)
        {
            if (slot.IsX != null)
            {
                return TakeSlot((bool)slot.IsX, slot.Row, slot.Column);
            }

            return false;
        }

        public void CheckError(bool isSuccess)
        {
            if (!isSuccess)
            {
                _error = true;
            }
        }

        protected State GetGameState()
        {
            if (_error)
            {
                return State.Error;
            }
            else if (GetWinner() != string.Empty)
            {
                return State.Win;
            }
            else if (Slots.Where(s => s.IsX == null).Count() == 0)
            {
                return State.Draw;
            }

            return State.Playing;
        }

        protected void ChangeTurn()
        {
            ThisTurn = (ThisTurn == "X") ? "O" : "X";
        }

        protected List<string> GetWinState()
        {
            var winState = new List<string>();
            var length = (int)Size == 3 ? 3 : 4;
            var maxRow = (int)Size - length + 1;
            var maxCol = (int)Size - length + 1;

            // เงื่อนไขการชนะแนวนอน
            winState.AddRange(CreateWinState("Horizontal", (int)Size, maxCol, length));

            // เงื่อนไขการชนะแนวตั้ง
            winState.AddRange(CreateWinState("Vertical", maxRow, (int)Size, length));

            // เงื่อนไขการชนะแนวทแยงซ้าย
            winState.AddRange(CreateWinState("SlashLeft", maxRow, maxCol, length));

            // เงื่อนไขการชนะแนวทแยงขวา
            winState.AddRange(CreateWinState("SlashRight", maxRow, maxCol, length));

            return winState;
        }

        protected List<string> CreateWinState(string diection, int maxRow, int maxCol, int length)
        {
            var winState = new List<string>();

            for (var row = 0; row < maxRow; row++)
            {
                for (var col = 0; col < maxCol; col++)
                {
                    var state = string.Empty;

                    for (var cell = 0; cell < length; cell++)
                    {
                        switch (diection)
                        {
                            case "Horizontal":
                                state += Slots[(row * (int)Size) + col + cell].Value;
                                break;
                            case "Vertical":
                                state += Slots[((row + cell) * (int)Size) + col].Value;
                                break;
                            case "SlashLeft":
                                state += Slots[((row + cell) * (int)Size) + col + cell].Value;
                                break;
                            case "SlashRight":
                                state += Slots[((row + cell) * (int)Size) + col - cell + length - 1].Value;
                                break;
                        }
                    }

                    winState.Add(state);
                }
            }

            return winState;
        }

        protected List<Slot> InitialBoard()
        {
            var slots = new List<Slot>();

            for (var row = 0; row < (int)Size; row++)
            {
                for (var col = 0; col < (int)Size; col++)
                {
                    slots.Add(new Slot() { IsX = null, Row = row, Column = col });
                }
            }

            return slots;
        }
    }

    enum State
    {
        Win,
        Draw,
        Playing,
        Error
    }
    enum BoardSize
    {
        Small = 3,
        Medium = 5,
        Large = 7
    }
}
