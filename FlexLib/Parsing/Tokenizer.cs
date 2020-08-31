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

        /// <summary>
        /// A mapping between textual names and types.
        /// </summary>
        private readonly Dictionary<string, Type> TypeMap;
        /// <summary>
        /// A mapping between textual names and types and parsers.
        /// </summary>
        private readonly Dictionary<Tuple<Type, string>, Parser> ParserMap;

        /// <summary>
        /// Constructs a tokenizer using a specific tokenizer context.
        /// </summary>
        /// <param name="context">The tokenizer context specifying rules and patterns.</param>
        public Tokenizer(TokenizerContext context)
        {
            // Set context and compile regular expression rule.
            Context = context;
            Rule = new Regex($@"(?:{
                    string.Join(@"|", Context.TokenResources.Select(tokenResource => $@"({
                        string.Join(@"|", tokenResource.TokenPatterns.Select(tokenPattern => $@"(?:{tokenPattern.Pattern})"))
                        })"))
                    }|(.))", RegexOptions.Compiled);

            // Initialize the type and parser mappings.
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
            // Find matches to the regular expression rule.
            MatchCollection matches = Rule.Matches(input);
            foreach (Match match in matches)
            {
                // For each match, find its corresponding token and emit it.
                bool tokenized = false;
                for (int i = 0; i < Context.TokenResources.Count; i++)
                {
                    if (!string.IsNullOrEmpty(match.Groups[i + 1].Value))
                    {
                        // Get the input value and the token resource.
                        string value = match.Groups[i + 1].Value;
                        TokenResource resource = Context.TokenResources[i];

                        // Only emit the token if the ignore flag is not set.
                        if (!resource.Ignore)
                        {
                            // Get the data type and parsed data if possible.
                            Type dataType = null;
                            object dataValue = null;
                            if (resource.Type != null)
                            {
                                if (!TypeMap.TryGetValue(resource.Type, out dataType))
                                    throw new KeyNotFoundException($"Name error: Could not find type with name '{resource.Type}'.");
                            }

                            yield return new Token(resource.Name, value, resource.Ligature);
                        }

                        // A successful token has been matched; no need to raise an error.
                        tokenized = true;
                        break;
                    }
                }

                // Invalid syntax results in a syntax error.
                if (!tokenized)
                    throw new Exception($"Syntax error: Unexpected symbol '{match.Value}'.");
            }
        }
    }
}
