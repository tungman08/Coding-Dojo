using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using TicTacToe.ConsoleUtil;
using TicTacToe.Game;
using TicTacToe.Player;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CommandLineApplication(throwOnUnexpectedArg: false);
            var oSymbol = cmd.Option("-p | --player", "Choose X or O or Random", CommandOptionType.SingleValue);
            //var oSize = cmd.Option("-s | --size", "Choose size S or M or L", CommandOptionType.SingleValue);
            var oLevel = cmd.Option("-l | --level", "Choose level 1-3", CommandOptionType.SingleValue);
            var oFirst = cmd.Option("-f | --first", "You play first turn", CommandOptionType.NoValue);
            cmd.HelpOption("-h | --help");

            cmd.OnExecute(() =>
            {
                Logo();
                cmd.ShowHelp();

                // เลือกสัญลักษณ์ที่ใช้เล่น
                var symbol = OptionSymbol(oSymbol);

                // เลือกขนาดของกระดาน
                //var size = OptionSize(oSize);
                var size = BoardSize.Small;

                // เลือกระดับความยาก
                var level = OptionLevel(oLevel);

                // เลือกเล่นก่อนหรือไม่
                var first = OptionPlayFirst(oFirst);

                // เริ่มเล่นเกม
                return new PlayGame().Start(symbol, size, level, first);
            });

            try
            {
                cmd.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string OptionSymbol(CommandOption option)
        {
            if (option.HasValue())
            {
                switch (option.Value().ToUpper())
                {
                    case "X":
                        return "X";
                    case "O":
                        return "O";
                    case "R":
                    case "RANDOM":
                        return "R";
                }
            }

            var options = new Dictionary<string, string>
                {
                    { "X", "[X]" },
                    { "O", "[O]" },
                    { "R", "[Random]" }
                };

            return Input.Select("Choose symbol to play:", options);
        }

        static BoardSize OptionSize(CommandOption option)
        {
            if (option.HasValue())
            {
                switch (option.Value().ToUpper())
                {
                    case "S":
                        return BoardSize.Small;
                    case "M":
                        return BoardSize.Medium;
                    case "L":
                        return BoardSize.Large;
                }
            }

            var options = new Dictionary<BoardSize, string>
                {
                    { BoardSize.Small, "Small" },
                    { BoardSize.Medium, "Medium" },
                    { BoardSize.Large, "Large" }
                };

            return Input.Select("Choose board's size:", options);
        }

        static AiLevel OptionLevel(CommandOption option)
        {
            if (option.HasValue())
            {
                switch (option.Value())
                {
                    case "1":
                        return AiLevel.Easy;
                    case "2":
                        return AiLevel.Normal;
                    case "3":
                        return AiLevel.Hard;
                }
            }

            var options = new Dictionary<AiLevel, string>
                {
                    { AiLevel.Easy, "Level 1 Easy" },
                    { AiLevel.Normal, "Level 2 Normal" },
                    { AiLevel.Hard, "Level 3 Hard" },
                };

            return Input.Select("Choose level 1-3:", options);
        }

        static bool OptionPlayFirst(CommandOption option)
        {
            return !option.HasValue() ? Input.Confirm("First to start?") : true;
        }

        static void Logo()
        {
            Console.WriteLine();
            Console.WriteLine("\t    " + @"    |\__/,|   (`\");
            Console.WriteLine("\t    " + @"  _.|o o  |_   ) )");
            Console.WriteLine("\t    " + @"-(((---(((--------");
            Console.WriteLine();
            Console.WriteLine("============= Tic Tac Toe =============");
        }
    }
}
