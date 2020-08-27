using System;

namespace FlexLib.Reflection
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ParserAttribute : Attribute
    {
        public readonly string Name;

        public ParserAttribute()
            : this(null) { }
        public ParserAttribute(string name)
        {
            Name = name;
        }
    }
}