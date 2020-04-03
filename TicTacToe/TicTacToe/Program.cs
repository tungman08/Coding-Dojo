using Microsoft.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.Linq;

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

                var options = new Dictionary<string, string>
                { 
                    { "X", "[X]" }, 
                    { "O", "[O]" } 
                };

                // เลือกข้าง
                var symbol = oSymbol.HasValue() ?
                    "XO".Contains(oSymbol.Value().ToUpper()) ? // ตรวจสอบ value ต้องเป็น x หรือ o เท่านั้น
                    oSymbol.Value().ToUpper() == "X" ? "X" : "O" :
                    Input.Select("Choose X or O:", options) :
                    Input.Select("Choose X or O:", options);

                // เลือกเล่นก่อน
                var first = !oFirst.HasValue() ?
                    Input.Confirm("First to start?:") :
                    true;

                // เริ่มเกม
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
