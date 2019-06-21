using System;

namespace MathemediaConsole
{
    public struct TokenPattern<T>
    {
        public readonly string Pattern;
        public readonly Func<string, T> Transform;

        public TokenPattern(string pattern, Func<string, T> transform)
        {
            Pattern = pattern;
            Transform = transform;
        }
    }
}
