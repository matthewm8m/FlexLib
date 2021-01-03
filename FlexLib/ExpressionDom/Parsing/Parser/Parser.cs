using System.Collections.Generic;
using System.Linq;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a parser which performs a conversion from a token stream to a reduced token stream based on a set of specified <see cref="ParserRule"/> in order of precedence in <see cref="ParserLevel"/>.
    /// </summary>
    public class Parser
    {
        private readonly IList<ParserLevel> Levels;

        /// <summary>
        /// Creates a new <see cref="Parser"/> object with the specified parser rules organized in levels.
        /// </summary>
        /// <param name="levels">The ordered parser levels including all parser rules.</param>
        public Parser(IEnumerable<ParserLevel> levels)
        {
            Levels = new List<ParserLevel>(levels);
        }

        /// <summary>
        /// Performs parsing to reduce a token stream.
        /// </summary>
        /// <param name="tokens">The input token stream.</param>
        /// <returns>The output reduced token stream.</returns>
        public IEnumerable<Token> Parse(IEnumerable<Token> tokens)
        {
            // We use this function to step forward from a particular node in the token stream.
            IEnumerable<Token> EnumerateFromNode(LinkedListNode<Token> node)
            {
                while (node != null)
                {
                    yield return node.Value;
                    node = node.Next;
                }
            }

            // We use a linked list because it has efficient insertion and deletion.
            LinkedList<Token> tokenStream = new LinkedList<Token>(tokens);

            // Perform as much reduction as possible before returning.
            bool reducible = true;
            while (reducible)
            {
                // We assume that the expression is not reducible every interation until a reduction takes place.
                // This allows us to continue reducing only if a parser rule was triggered in the previous iteration.
                reducible = false;
                foreach (ParserLevel level in Levels)
                {
                    // We determine the starting location of the token stream depending on the level associativity.
                    LinkedListNode<Token> tokenNode = null;
                    switch (level.Associativity)
                    {
                        case ParserAssociativity.LeftToRight:
                            tokenNode = tokenStream.First;
                            break;
                        case ParserAssociativity.RightToLeft:
                            tokenNode = tokenStream.Last;
                            break;
                    }

                    // We make a full pass across the token stream exactly once per parser level.
                    while (tokenNode != null)
                    {
                        // We apply each parser rule in the level per location in the token stream.
                        bool reducibleLevel = false;
                        foreach (ParserRule rule in level.Rules)
                        {
                            // We determine how many tokens are matched by the current rule starting from our head node.
                            IEnumerable<Token> streamHead = EnumerateFromNode(tokenNode);
                            int matchedTokens = rule.Pattern.FindMatch(streamHead);

                            /*
                                If there are matched tokens, we need to replace the matched tokens with the new tokens
                                provided by the rule. We make sure to set the flag to indicate a reduction has taken
                                place. We also need consistent behavior for where the new head node is located. When
                                there are new tokens, we move to their beginning (based on associativity). Otherwise, we
                                move further in the token stream (based on associativity).
                            */
                            if (matchedTokens > 0)
                            {
                                // Insert new tokens, if any.
                                LinkedListNode<Token> tokenNodeReplacement = null;
                                foreach (Token token in rule.Tokenize(streamHead.Take(matchedTokens)))
                                {
                                    LinkedListNode<Token> tokenNodeNew = tokenStream.AddBefore(tokenNode, token);
                                    switch (level.Associativity)
                                    {
                                        case ParserAssociativity.LeftToRight:
                                            tokenNodeReplacement = tokenNodeReplacement ?? tokenNodeNew;
                                            break;
                                        case ParserAssociativity.RightToLeft:
                                            tokenNodeReplacement = tokenNodeNew;
                                            break;
                                    }
                                }

                                // Delete old tokens and move token head.
                                for (int k = 0; k < matchedTokens - 1; k++)
                                    tokenStream.Remove(tokenNode.Next);
                                if (tokenNodeReplacement == null)
                                {
                                    switch (level.Associativity)
                                    {
                                        case ParserAssociativity.LeftToRight:
                                            tokenNodeReplacement = tokenNode.Next;
                                            break;
                                        case ParserAssociativity.RightToLeft:
                                            tokenNodeReplacement = tokenNode.Previous;
                                            break;
                                    }
                                }
                                tokenStream.Remove(tokenNode);
                                tokenNode = tokenNodeReplacement;

                                // Set reduction flag.
                                reducible = reducibleLevel = true;
                                break;
                            }
                        }

                        // If we reduced in this level, we need to retry all rules in order without moving the head.
                        if (reducibleLevel)
                            continue;

                        // We need to move our token stream head in the correct direction based on the level associativity.
                        switch (level.Associativity)
                        {
                            case ParserAssociativity.LeftToRight:
                                tokenNode = tokenNode.Next;
                                break;
                            case ParserAssociativity.RightToLeft:
                                tokenNode = tokenNode.Previous;
                                break;
                        }
                    }

                    // If a reduction happened in this level, we don't want to move forward to levels with lower
                    // precedence until we are certain that we are done with higher precedence levels.
                    if (reducible)
                        break;
                }
            }

            return tokenStream;
        }
        /// <summary>
        /// Performs parsing to reduce a token stream into a single token. If the parsing cannot reduce the token stream to a single token, a <see cref="ParserIncompleteException"/> is thrown.
        /// </summary>
        /// <param name="tokens">The input token stream.</param>
        /// <returns>The output single token.</returns>
        public Token ParseSingular(IEnumerable<Token> tokens)
        {
            // Return a single token if one exists.
            // If more than one token exists, throw an error.
            IEnumerable<Token> results = Parse(tokens);
            if (results.Count() > 1)
            {
                // Throw an exception for every result besides the first.
                int resultIndex = 0;
                foreach (Token result in results)
                {
                    if (resultIndex++ > 0)
                        throw new ParserIncompleteException(result.Source, "Unexpected expression encountered");
                }
            }
            return results.FirstOrDefault();
        }
    }
}