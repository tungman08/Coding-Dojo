using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TicTacToe.ConsoleUtil
{
    class Input
    {
        public static string Text(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(label);
            Console.ResetColor();
            Console.Write("> ");
            string text = Console.ReadLine();
            Console.WriteLine();

            return text;
        }

        public static string Password(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(label);
            Console.ResetColor();
            Console.Write("> ");
            string password = ReadPassword();
            Console.WriteLine();

            return password;
        }

        public static T Select<T>(string label, Dictionary<T, string> options)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(label);
            Console.ResetColor();
            var selected = SelectOptions(options);
            Console.WriteLine();

            return selected;
        }

        public static bool Confirm(string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{label} [y/n]");
            Console.ResetColor();
            Console.Write("> ");
            bool selected = SelectChoice();
            Console.WriteLine();

            return selected;
        }

        protected static string ReadPassword()
        {
            var pwd = new Stack<char>();
            var filtered = new ConsoleKey[] { ConsoleKey.Tab, ConsoleKey.Spacebar, ConsoleKey.Enter, ConsoleKey.Escape };

            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // ctrl+backsp
                if (key.Key == ConsoleKey.Backspace && key.Modifiers.HasFlag(ConsoleModifiers.Control))
                {
                    while (pwd.Count > 0)
                    {
                        Console.Write("\b \b");
                        pwd.Pop();
                    }
                }
                // backsp
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Count > 0)
                    {
                        Console.Write("\b \b");
                        pwd.Pop();
                    }
                }
                // '\u0000' if the key pressed does not correspond to a printable character, e.g. F1, Pause-Break, etc
                else if (key.KeyChar != '\u0000' && !filtered.Any(k => k == key.Key))
                {
                    pwd.Push(key.KeyChar);
                    Console.Write("*");
                }

            } while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();

            return new string(pwd.Reverse().ToArray());
        }

        protected static bool SelectChoice()
        {
            ConsoleKey key;

            do
            {
                // true is intercept key (dont show), false is show
                key = Console.ReadKey(true).Key;
            } while (key != ConsoleKey.Y && key != ConsoleKey.N);

            Console.WriteLine(key == ConsoleKey.Y ? "Yes" : "No");

            return key == ConsoleKey.Y;
        }

        protected static T SelectOptions<T>(Dictionary<T, string> options)
        {
            var startY = Console.CursorTop;
            var current = 0;

            ConsoleKey key;
            Console.CursorVisible = false;

            do
            {
                foreach (var option in options)
                {
                    var index = options.Values.ToList().IndexOf(option.Value);

                    Console.SetCursorPosition(0, startY + index);
                    Console.WriteLine((index == current) ?
                        $"> {option.Value}" :
                        $"  {option.Value}");
                }

                key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (current > 0)
                            current--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (current < options.Count - 1)
                            current++;
                        break;
                }
            } while (key != ConsoleKey.Enter);

            Console.CursorVisible = true;

            return options.ElementAt(current).Key;
        }
    }
}
