using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathemediaConsole
{
    public enum ParseAssociativity
    {
        RIGHT,
        LEFT,
        NONE
    }

    public struct ParsePattern<T>
    {
        public readonly Func<object[], int, int, bool> Pattern;
        public readonly uint Precedence;
        public readonly ParseAssociativity Associativity;
        public readonly Func<object[], T> Transform;
    }
}
