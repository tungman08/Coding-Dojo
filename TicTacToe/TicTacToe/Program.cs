using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using TicTacToe.ConsoleUtil;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CommandLineApplication(throwOnUnexpectedArg: false);
            var oSymbol = cmd.Option("-p | --player", "Choose X or O or Random", CommandOptionType.SingleValue);
            var oLevel = cmd.Option("-l | --level", "Choose level 1-3", CommandOptionType.SingleValue);
            var oFirst = cmd.Option("-f | --first", "You play first turn", CommandOptionType.NoValue);
            cmd.HelpOption("-h | --help");

            cmd.OnExecute(() =>
            {
                Logo();
                cmd.ShowHelp();

                // เลือกสัญลักษณ์ที่ใช้เล่น
                var symbol = OptionSymbol(oSymbol);

                // เลือกระดับความยาก
                var level = OptionLevel(oLevel);

                // เลือกเล่นก่อนหรือไม่
                var first = OptionPlayFirst(oFirst);

                // เริ่มเล่นเกม
                return new PlayGame().Start(symbol, level, first);
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
                    case "RANDAM":
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

        static int OptionLevel(CommandOption option)
        {
            if (option.HasValue())
            {
                if (int.Parse(option.Value()) >= 0 && int.Parse(option.Value()) <= 3) {
                    return int.Parse(option.Value());
                }
            }

            var options = new Dictionary<string, string>
                {
                    { "1", "Level 1 Easy" },
                    { "2", "Level 2 Normal" },
                    { "3", "Level 3 Hard" },
                };

            return int.Parse(Input.Select("Choose level 1-3:", options));
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
