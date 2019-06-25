namespace FlexLib.Parsing
{
    public class Token
    {
        public readonly string Type;
        public readonly string Ligature;
        public readonly string Input;
        public readonly int Index;

        public Token(string type, string input, int index)
            : this(type, null, input, index) { }

        public Token(string type, string ligature, string input, int index)
        {
            Type = type;
            Ligature = ligature;
            Input = input;
            Index = index;
        }
    }
}
