using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class Program
    {
        static void Main(string[] args)
        {
            var cmd = new CommandLineApplication(throwOnUnexpectedArg: false);
            var oSymbol = cmd.Option("-c | --choose", "Choose X or O", CommandOptionType.SingleValue);
            var oFirst = cmd.Option("-f | --first", "You play first turn", CommandOptionType.NoValue);
            cmd.HelpOption("-h | --help");

            cmd.OnExecute(() =>
            {
                Display.Logo();
                cmd.ShowHelp();

                var symbol = (!oSymbol.HasValue()) ?
                    Input.Select("Choose X or O:", new Dictionary<string, string> { { "X", "X" }, { "O", "O" } }) :
                    "X";
                var first = (!oFirst.HasValue()) ?
                    Input.Confirm("First to start?:") :
                    true;

                return Game.Play(symbol, first);
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
    }
}
