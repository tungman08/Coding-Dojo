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
            var oHuman = cmd.Option("-c | --choose", "Choose X or O", CommandOptionType.SingleValue);
            var oFirst = cmd.Option("-f | --first", "You play first turn", CommandOptionType.NoValue);
            cmd.HelpOption("-h | --help");

            cmd.OnExecute(() =>
            {
                Display.Logo();
                cmd.ShowHelp();

                var human = (!oHuman.HasValue()) ?
                    Input.Select("Choose X or O:", new Dictionary<bool, string> { { true, "X" }, { false, "O" } }) :
                    oHuman.Value().ToLower() == "x";
                var first = (!oFirst.HasValue()) ?
                    Input.Confirm("First to start?:") :
                    true;

                return Game.Play(human, first);
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
