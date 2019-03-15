using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathemediaConsole
{
    public enum ParseAssociativity
    {
        LEFT,
        RIGHT,
        NONE
    }

    public class ParseRule
    {
        public readonly Type[] Arguments;
        public readonly ParseAssociativity Associativity;
        public readonly Func<object[], object> Parse;

        public ParseRule(Type[] args, ParseAssociativity associate, Func<object[], object> parse)
        {
            Arguments = args;
            Associativity = associate;
            Parse = parse;
        }
    }

    public class Parser
    {

    }
}
