using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using FlexLib.Reflection;

namespace FlexLib.Parsing
{
    public delegate object Parser(string text);

    public class TokenizerContext
    {
        public readonly List<TokenResource> TokenResources;

        private readonly Dictionary<string, Type> TypeMap;
        private readonly Dictionary<Tuple<Type, string>, Parser> ParserMap;

        public TokenizerContext()
            : this(Enumerable.Empty<TokenResource>()) { }
        public TokenizerContext(IEnumerable<TokenResource> tokenResources)
        {
            TokenResources = new List<TokenResource>(tokenResources);

            InitializeAttributeMaps();
        }

        private void InitializeAttributeMaps()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    // Add type mappings.
                    IEnumerable<TypeAttribute> typeAttrs = type.GetCustomAttributes<TypeAttribute>(false);
                    foreach (TypeAttribute typeAttr in typeAttrs)
                        TypeMap.Add(typeAttr.Name, type);

                    // Add parser mappings.
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        IEnumerable<ParserAttribute> parserAttrs = method.GetCustomAttributes<ParserAttribute>(false);
                        foreach (ParserAttribute parserAttr in parserAttrs)
                        {
                            try
                            {
                                ParserMap.Add(new Tuple<Type, string>(type, parserAttr.Name), (Parser)Delegate.CreateDelegate(typeof(Parser), method));
                            }
                            catch (Exception ex)
                            {
                                throw new NotSupportedException($"Parser named '{parserAttr.Name}' is not implemented correctly.");
                            }
                        }
                    }
                }
            }
        }

        public static TokenizerContext FromXml(XElement xml)
        {
            return new TokenizerContext(xml.Elements("token")
                .Select(tokenElement => TokenResource.FromXml(tokenElement)));
        }
    }
}