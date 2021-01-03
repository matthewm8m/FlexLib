using System;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;
using FlexLib.ExpressionDom.Parsing;

namespace MathemediaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Set the indicators for prompts.
            string inputIndicator = "? ";
            string outputIndicator = "! ";
            ConsoleColor indicatorColor = ConsoleColor.Cyan;

            /* REPL */
            // Loop
            while (true)
            {
                // Read
                Console.ForegroundColor = indicatorColor;
                Console.Write(inputIndicator);
                Console.ResetColor();
                string input = Console.ReadLine();

                // Evaluate
                string output;
                try
                {
                    Token result = Pipeline.DefaultPipeline.ParseSingular(input);
                    output = ((IExpression<RealFieldElement>)result.Value).Evaluate().ToString();
                }
                catch (LexerSyntaxException lexEx)
                {
                    output = $"{lexEx.Message}: '{lexEx.TokenSource.Snippet}'";
                }
                catch (ParserIncompleteException parEx)
                {
                    output = $"{parEx.Message}: '{parEx.TokenSource.Snippet}'";
                }

                // Print
                Console.ForegroundColor = indicatorColor;
                Console.Write(outputIndicator);
                Console.ResetColor();
                Console.WriteLine(output);
            }
        }
    }
}
