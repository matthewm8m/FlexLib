using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FlexLib.Reflection;

namespace FlexLib.Parsing
{
    public delegate object Parser(string text);

    /// <summary>
    /// Uses a specified tokenizer context to provide the rules for taking plain text and converting to tokens.
    /// </summary>
    public class Tokenizer
    {
        /// <summary>
        /// Specifies tokenizer rules and settings.
        /// </summary>
        private readonly TokenizerContext Context;
        /// <summary>
        /// The compiled regular expressions rule constructed from the context.
        /// </summary>
        private readonly Regex Rule;

        private readonly Dictionary<string, Type> TypeMap;
        private readonly Dictionary<Tuple<Type, string>, Parser> ParserMap;

        public Tokenizer(TokenizerContext context)
            : this(context, true) { }
        public Tokenizer(TokenizerContext context, bool ignoreWhitespace)
        {
            string whitespaceDelimiter = ignoreWhitespace ? @"\s*" : @"";

            Context = context;
            Rule = new Regex($@"{whitespaceDelimiter}(?:{
                    string.Join(@"|", Context.TokenResources.Select(tokenResource => $@"({
                        string.Join(@"|", tokenResource.RegexPatterns.Select(tokenPattern => $@"(?:{tokenPattern})"))
                        })"))
                    }|(.)){whitespaceDelimiter}", RegexOptions.Compiled);

            InitializeAttributeMaps();
        }

        /// <summary>
        /// Initializes mappings between naming attributes and properties.
        /// </summary>
        private void InitializeAttributeMaps()
        {
            // Search through all types in the current assemblies and compile a list of types and type parsers.
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
                        // Try to convert method to correct signature.
                        // If a failure occurs, notify the user that parser is incorrect.
                        IEnumerable<ParserAttribute> parserAttrs = method.GetCustomAttributes<ParserAttribute>(false);
                        foreach (ParserAttribute parserAttr in parserAttrs)
                        {
                            try
                            {
                                ParserMap.Add(new Tuple<Type, string>(type, parserAttr.Name), (Parser)Delegate.CreateDelegate(typeof(Parser), method));
                            }
                            catch (Exception)
                            {
                                throw new NotSupportedException($"Parser named '{parserAttr.Name}' is not implemented correctly.");
                            }
                        }
                    }
                }
            }
        }

        public IEnumerable<Token> Tokenize(string input)
        {
            MatchCollection matches = Rule.Matches(input);

            foreach (Match match in matches)
            {
                bool tokenized = false;
                for (int i = 0; i < Context.TokenResources.Count; i++)
                {
                    if (!string.IsNullOrEmpty(match.Groups[i + 1].Value))
                    {
                        yield return new Token(Context.TokenResources[i].Name, Context.TokenResources[i].Ligature, input, match.Index);
                        tokenized = true;
                        break;
                    }
                }

                if (!tokenized)
                    throw new Exception($"Syntax error: Unexpected symbol '{match.Value}'");
            }
        }
    }
}
