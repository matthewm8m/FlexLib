using System;

namespace FlexLib.Reflection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TypeAttribute : Attribute
    {
        public readonly string Name;

        public TypeAttribute(string name)
        {
            Name = name;
        }
    }
}