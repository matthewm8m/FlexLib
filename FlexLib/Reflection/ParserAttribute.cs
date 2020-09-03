using System;

namespace FlexLib.Reflection
{
    /// <summary>
    /// Represents the existance of a common name for a parser than can be referenced outside of the code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParserAttribute : Attribute
    {
        /// <summary>
        /// The common name of the parser.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParserAttribute"/> class with no name.
        /// </summary>
        public ParserAttribute()
            : this(null) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParserAttribute"/> class with a specified name.
        /// </summary>
        /// <param name="name">The common name.</param>
        public ParserAttribute(string name)
        {
            Name = name;
        }
    }
}