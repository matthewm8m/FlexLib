using System;
using System.Collections.Generic;
using System.Linq;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a parser rule to create tokens from combinations of other tokens based on a pattern.
    /// </summary>
    public abstract class ParserRule : TokenRule<IEnumerable<Token>>
    {
        /// <summary>
        /// The token pattern used to find matching sequences of tokens in the source stream.
        /// </summary>
        public ITokenPattern Pattern;
    }

    /// <summary>
    /// Represents a parser rule to create tokens from a given function from combinations of other tokens based on a pattern.
    /// </summary>
    /// <typeparam name="TResult">The type of token generated by the rule.</typeparam>
    public class ParserRule<TResult> : ParserRule
    {
        /// <summary>
        /// The function used to create a new token value.
        /// </summary>
        public Func<TResult> Transform;

        /// <summary>
        /// Uses a combination of tokens to generate a new set of tokens to replace them.
        /// </summary>
        /// <param name="tokens">The source tokens.</param>
        /// <returns>The replacement tokens.</returns>
        public override IEnumerable<Token> Tokenize(IEnumerable<Token> tokens)
        {
            TResult result = Transform();

            TokenSource source = TokenSource.Join(tokens.Select(token => token.Source));
            yield return new Token(this, source, result);
        }
    }
    /// <summary>
    /// Represents a parser rule to create tokens from a given function from combinations of other tokens based on a pattern.
    /// </summary>
    /// <typeparam name="T1">The type of token associated with parameter 1.</typeparam>
    /// <typeparam name="TResult">The type of token generated by the rule.</typeparam>
    public class ParserRule<T1, TResult> : ParserRule
    {
        /// <summary>
        /// The function used to create a new token value.
        /// </summary>
        public Func<T1, TResult> Transform;

        /// <summary>
        /// Uses a combination of tokens to generate a new set of tokens to replace them.
        /// </summary>
        /// <param name="tokens">The source tokens.</param>
        /// <returns>The replacement tokens.</returns>
        public override IEnumerable<Token> Tokenize(IEnumerable<Token> tokens)
        {
            IList<object> parameters = Pattern.FindParameters(tokens, 1);
            if (!(parameters[0] is T1 value1)) throw new InvalidCastException($"Parameter {0} which is {parameters[0]} is not of type {typeof(T1)}");
            TResult result = Transform(value1);

            TokenSource source = TokenSource.Join(tokens.Select(token => token.Source));
            yield return new Token(this, source, result);
        }
    }
    /// <summary>
    /// Represents a parser rule to create tokens from a given function from combinations of other tokens based on a pattern.
    /// </summary>
    /// <typeparam name="T1">The type of token associated with parameter 1.</typeparam>
    /// <typeparam name="T2">The type of token associated with parameter 2.</typeparam>
    /// <typeparam name="TResult">The type of token generated by the rule.</typeparam>
    public class ParserRule<T1, T2, TResult> : ParserRule
    {
        /// <summary>
        /// The function used to create a new token value.
        /// </summary>
        public Func<T1, T2, TResult> Transform;

        /// <summary>
        /// Uses a combination of tokens to generate a new set of tokens to replace them.
        /// </summary>
        /// <param name="tokens">The source tokens.</param>
        /// <returns>The replacement tokens.</returns>
        public override IEnumerable<Token> Tokenize(IEnumerable<Token> tokens)
        {
            IList<object> parameters = Pattern.FindParameters(tokens, 2);
            if (!(parameters[0] is T1 value1)) throw new InvalidCastException($"Parameter {0} which is {parameters[0]} is not of type {typeof(T1)}");
            if (!(parameters[1] is T2 value2)) throw new InvalidCastException($"Parameter {1} which is {parameters[1]} is not of type {typeof(T2)}");
            TResult result = Transform(value1, value2);

            TokenSource source = TokenSource.Join(tokens.Select(token => token.Source));
            yield return new Token(this, source, result);
        }
    }
    /// <summary>
    /// Represents a parser rule to create tokens from a given function from combinations of other tokens based on a pattern.
    /// </summary>
    /// <typeparam name="T1">The type of token associated with parameter 1.</typeparam>
    /// <typeparam name="T2">The type of token associated with parameter 2.</typeparam>
    /// <typeparam name="T3">The type of token associated with parameter 3.</typeparam>
    /// <typeparam name="TResult">The type of token generated by the rule.</typeparam>
    public class ParserRule<T1, T2, T3, TResult> : ParserRule
    {
        /// <summary>
        /// The function used to create a new token value.
        /// </summary>
        public Func<T1, T2, T3, TResult> Transform;

        /// <summary>
        /// Uses a combination of tokens to generate a new set of tokens to replace them.
        /// </summary>
        /// <param name="tokens">The source tokens.</param>
        /// <returns>The replacement tokens.</returns>
        public override IEnumerable<Token> Tokenize(IEnumerable<Token> tokens)
        {
            IList<object> parameters = Pattern.FindParameters(tokens, 3);
            if (!(parameters[0] is T1 value1)) throw new InvalidCastException($"Parameter {0} which is {parameters[0]} is not of type {typeof(T1)}");
            if (!(parameters[1] is T2 value2)) throw new InvalidCastException($"Parameter {1} which is {parameters[1]} is not of type {typeof(T2)}");
            if (!(parameters[2] is T3 value3)) throw new InvalidCastException($"Parameter {2} which is {parameters[2]} is not of type {typeof(T3)}");
            TResult result = Transform(value1, value2, value3);

            TokenSource source = TokenSource.Join(tokens.Select(token => token.Source));
            yield return new Token(this, source, result);
        }
    }
    /// <summary>
    /// Represents a parser rule to create tokens from a given function from combinations of other tokens based on a pattern.
    /// </summary>
    /// <typeparam name="T1">The type of token associated with parameter 1.</typeparam>
    /// <typeparam name="T2">The type of token associated with parameter 2.</typeparam>
    /// <typeparam name="T3">The type of token associated with parameter 3.</typeparam>
    /// <typeparam name="T4">The type of token associated with parameter 4.</typeparam>
    /// <typeparam name="TResult">The type of token generated by the rule.</typeparam>
    public class ParserRule<T1, T2, T3, T4, TResult> : ParserRule
    {
        /// <summary>
        /// The function used to create a new token value.
        /// </summary>
        public Func<T1, T2, T3, T4, TResult> Transform;

        /// <summary>
        /// Uses a combination of tokens to generate a new set of tokens to replace them.
        /// </summary>
        /// <param name="tokens">The source tokens.</param>
        /// <returns>The replacement tokens.</returns>
        public override IEnumerable<Token> Tokenize(IEnumerable<Token> tokens)
        {
            IList<object> parameters = Pattern.FindParameters(tokens, 4);
            if (!(parameters[0] is T1 value1)) throw new InvalidCastException($"Parameter {0} which is {parameters[0]} is not of type {typeof(T1)}");
            if (!(parameters[1] is T2 value2)) throw new InvalidCastException($"Parameter {1} which is {parameters[1]} is not of type {typeof(T2)}");
            if (!(parameters[2] is T3 value3)) throw new InvalidCastException($"Parameter {2} which is {parameters[2]} is not of type {typeof(T3)}");
            if (!(parameters[3] is T4 value4)) throw new InvalidCastException($"Parameter {3} which is {parameters[3]} is not of type {typeof(T4)}");
            TResult result = Transform(value1, value2, value3, value4);

            TokenSource source = TokenSource.Join(tokens.Select(token => token.Source));
            yield return new Token(this, source, result);
        }
    }
}