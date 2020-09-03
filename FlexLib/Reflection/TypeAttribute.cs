using System;

namespace FlexLib.Reflection
{
    /// <summary>
    /// Represents the existance of a common name for a type that can be referenced outside of the code.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public class TypeAttribute : Attribute
    {
        /// <summary>
        /// The common name of the type.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAttribute"/> class with a specified name.
        /// </summary>
        /// <param name="name">The common name.</param>
        public TypeAttribute(string name)
        {
            Name = name;
        }
    }
}