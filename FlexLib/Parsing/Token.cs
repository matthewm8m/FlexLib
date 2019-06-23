namespace FlexLib.Parsing
{
    public class Token
    {
        public readonly string Type;
        public readonly string Input;
        public readonly int Index;

        public Token(string type, string input, int index)
        {
            Type = type;
            Input = input;
            Index = index;
        }
    }
}
