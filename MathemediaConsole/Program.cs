using System;
using System.Collections.Generic;

namespace MathemediaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            TokenRule tokenRuleInt = new TokenRule(@"\d+", s => int.Parse(s));
            Tokenizer tokenizer = new Tokenizer(new List<TokenRule> { tokenRuleInt });

            // Get user input from user until interrupt.
            string userInput;
            while (true)
            {
                // Write out input indicator.
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("? ");

                // Get user input until ENTER is pressed.
                Console.ForegroundColor = ConsoleColor.White;
                userInput = Console.ReadLine();

                // Exit from program if user inputs 'exit'.
                if (userInput.ToLower() == "exit")
                    break;

                // Write out output indicator.
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("! ");

                foreach (Token token in tokenizer.Tokenize(userInput))
                {
                    Console.WriteLine(token.Value);
                }
            }
            Console.ResetColor();
        }
    }
}
