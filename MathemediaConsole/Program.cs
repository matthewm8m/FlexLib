using System;
using System.Text;
using System.Collections.Generic;

namespace MathemediaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputIndicator = "? ";
            string outputIndicator = "! ";

            TokenRule tokenRuleInt = new TokenRule(@"\d+", s => int.Parse(s));
            TokenRule tokenRuleOperator = new TokenRule(@"[=\+\-\*\\]", s => s);
            TokenRule tokenRuleUnknown = new TokenRule(@".", s => null);
            Tokenizer tokenizer = new Tokenizer(new List<TokenRule> { tokenRuleInt, tokenRuleOperator, tokenRuleUnknown });

            // Get user input from user until interrupt.
            StringBuilder userInput = new StringBuilder();
            int userIndex = 0;
            while (true)
            {
                // Write out input indicator.
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(inputIndicator);

                // Get user input until ENTER is pressed.
                do
                {
                    // Get user input character by character.
                    ConsoleKeyInfo userKey = Console.ReadKey();

                    // Break input loop when ENTER is pressed.
                    if (userKey.Key == ConsoleKey.Enter)
                        break;

                    // Check key for special key codes.
                    switch (userKey.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            userIndex = (userIndex == 0) ? userIndex : userIndex - 1;
                            break;
                        case ConsoleKey.RightArrow:
                            userIndex = (userIndex == userInput.Length) ? userIndex : userIndex + 1;
                            break;
                        case ConsoleKey.Backspace:
                            if (userIndex != 0)
                            {
                                userIndex = userIndex - 1;
                                userInput.Remove(userIndex, 1);
                            }
                            break;
                        case ConsoleKey.Delete:
                            if (userIndex != userInput.Length)
                            {
                                userInput.Remove(userIndex, 1);
                            }
                            break;
                        default:
                            userInput.Insert(userIndex, userKey.KeyChar);
                            userIndex++;
                            break;
                    }

                    // Get tokens and write them with color.
                    Console.CursorLeft = inputIndicator.Length;
                    for (int i = 0; i < userInput.Length + 1; i++)
                    {
                        Console.Write(" ");
                    }
                    Console.CursorLeft = inputIndicator.Length;
                    foreach (Token token in tokenizer.Tokenize(userInput.ToString()))
                    {
                        if (token.Value is int)
                            Console.ForegroundColor = ConsoleColor.Yellow;
                        else if (token.Value is string)
                            Console.ForegroundColor = ConsoleColor.Green;
                        else if (token.Value is null)
                            Console.ForegroundColor = ConsoleColor.Red;

                        Console.Write(token.Text);
                    }
                    Console.CursorLeft = inputIndicator.Length + userIndex;
                } while (true);

                // Write out output indicator.
                userInput.Clear();
                userIndex = 0;
                Console.CursorLeft = 0;
                Console.CursorTop = Console.CursorTop + 1;
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(outputIndicator);
            }
            Console.ResetColor();
        }
    }
}
