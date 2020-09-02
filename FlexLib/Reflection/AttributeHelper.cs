using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace FlexLib.Reflection
{
    /// <summary>
    /// Provides helper methods to find types and parsers based on attributes on types and methods.
    /// </summary>
    public static class AttributeHelper
    {
        private static IDictionary<string, Type> Types;
        private static IDictionary<Type, IDictionary<string, Func<string, object>>> Parsers;

        /// <summary>
        /// Finds a type specified by a common name.
        /// </summary>
        /// <param name="name">The common name of the type.</param>
        /// <returns>The type if it can be found; otherwise, <c>null</c>.</returns>
        public static Type FindType(string name)
        {
            // If the types mapping has not been initialized, create it.
            if (Types == null)
            {
                // Search through all types in the current assemblies and compile a list of types.
                Types = new Dictionary<string, Type>();
                foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    foreach (Type type in assembly.GetTypes())
                    {
                        // Add type mappings.
                        foreach (TypeAttribute typeAttr in type.GetCustomAttributes<TypeAttribute>(false))
                            Types.Add(typeAttr.Name, type);
                    }
                }
            }

            // Try to find the type corresponding to the specified name.
            // If there is no corresponding type, just return 'null'.
            bool isFound = Types.TryGetValue(name, out Type foundType);
            return isFound ? foundType : null;
        }
        /// <summary>
        /// Finds a parser for a type specified by a common name.
        /// </summary>
        /// <param name="name">The common name of the parser.</param>
        /// <typeparam name="T">The type that the parser is for.</typeparam>
        /// <returns>The parser if it can be found; otherwise, <c>null</c>.</returns>
        public static Func<string, object> FindParser<T>(string name)
        {
            // Get the type called using this function.
            Type type = typeof(T);

            // Check if the parsers mapping for the associated name and type has been initialized.
            // If not, attempt to initialize it.
            if (Parsers == null)
                Parsers = new Dictionary<Type, IDictionary<string, Func<string, object>>>();
            if (!Parsers.ContainsKey(type))
            {
                // Construct a new dictionary to store the parsers for the specified type.
                IDictionary<string, Func<string, object>> typeParsers = new Dictionary<string, Func<string, object>>();
                Parsers.Add(type, typeParsers);

                // Search through all methods in the type and compile a list of parsers.
                foreach (MethodInfo method in type.GetMethods())
                {
                    // Try to convert method to correct signature.
                    // If a failure occurs, notify the user that parser is incorrect.
                    Func<string, T> parser = null;
                    IEnumerable<ParserAttribute> parserAttrs = method.GetCustomAttributes<ParserAttribute>(false);
                    if (parserAttrs.Any())
                    {
                        if (!method.IsStatic)
                            throw new MethodAccessException($"Parser named '{method.Name}' for type '{type}' is not static.");

                        try
                        {
                            parser = (Func<string, T>)Delegate.CreateDelegate(typeof(Func<string, T>), method);
                        }
                        catch (Exception)
                        {
                            throw new InvalidCastException($"Parser named '{method.Name}' for type '{type}' is not convertable to a '{typeof(Func<string, T>)}'.");
                        }
                    }

                    // Attach the parser to the parser names in the mapping.
                    foreach (ParserAttribute parserAttr in parserAttrs)
                        typeParsers.Add(parserAttr.Name, (text) => parser(text));
                }
            }

            // Try to find the parser corresponding to the specified name and type.
            // If there is no corresponding parser, just return 'null'.
            IDictionary<string, Func<string, object>> parsers = Parsers[type];
            bool isFound = parsers.TryGetValue(name, out Func<string, object> foundParser);
            return isFound ? foundParser : null;
        }
    }
}