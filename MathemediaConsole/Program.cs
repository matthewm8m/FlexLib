using System;

namespace MathemediaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
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
            }
            Console.ResetColor();
        }
    }
}
