using System;

namespace FlexLib.Parsing
{
    /// <summary>
    /// Represents an instance of a token match emitted from a tokenizer.
    /// </summary>
    public class Token
    {
        /// <summary>
        /// The name of the type of token.
        /// </summary>
        public readonly string Name;
        /// <summary>
        /// The input that resulted in the token.
        /// </summary>
        public readonly string Input;
        /// <summary>
        /// The optional symbol that represents the token.
        /// </summary>
        public readonly string Ligature;
        /// <summary>
        /// The type of the optional token data.
        /// </summary>
        public readonly Type Type;
        /// <summary>
        /// The value of the optional token data.
        /// </summary>
        public readonly object Data;

        /// <summary>
        /// Constructs a new token using tokenizer information.
        /// </summary>
        /// <param name="name">The name of the type of token.</param>
        /// <param name="input">The matched text of the token.</param>
        /// <param name="ligature">An optional symbol to represent the token.</param>
        /// <param name="type">The type of the optional token data.</param>
        /// <param name="data">The value of the optional token data.</param>
        public Token(
            string name,
            string input,
            string ligature = null,
            Type type = null,
            object data = null)
        {
            // Set properties of the token.
            Type = type;
            Input = input;
            Ligature = ligature;
            Type = type;
            Data = data;
        }

        /// <summary>
        /// Converts the token to a human-readable string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // If a ligature exists, represent the token using it.
            // Otherwise, use whatever the user input verbatim.
            if (Ligature == null)
                return Input;
            else
                return Ligature;
        }
    }
}
