using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Parsing;

namespace FlexLibTests.ExpressionDom.Parsing
{
    [TestFixture]
    public class LexingTests
    {
        protected Lexer Lexer;

        [SetUp]
        protected void SetUp()
        {
            // Set up the token definitions.
            IList<TokenDefinition> tokenDefs = new List<TokenDefinition>();
            tokenDefs.Add(new TokenDefinition
            {
                Name = "Comment",
                Pattern = @"\/{2}.*$",
                Ignore = true
            });
            tokenDefs.Add(new TokenDefinition
            {
                Name = "Space",
                Pattern = @"\s+",
                Ignore = true
            });
            tokenDefs.Add(new TokenDefinition<RealFieldElement>
            {
                Name = "Real",
                Pattern = @"\d*\.\d+",
                Parser = RealFieldElement.TryParse
            });
            tokenDefs.Add(new TokenDefinition<int>
            {
                Name = "Integer",
                Pattern = @"\d+",
                Parser = int.TryParse
            });
            tokenDefs.Add(new TokenDefinition
            {
                Name = "Multiply",
                Pattern = @"\*"
            });

            // Create lexer.
            Lexer = new Lexer(tokenDefs);
        }

        [Test]
        public void TestIgnoredTokens()
        {
            IList<Token> tokens;
            string source;

            // Expected tokens are: [Integer=2] [Integer=3]
            source = @"2 3 \\ 2";
            tokens = new List<Token>(Lexer.Lex(source));
            Assert.AreEqual(2, tokens.Count);
            Assert.IsInstanceOf(typeof(int), tokens[0].Value);
            Assert.IsInstanceOf(typeof(int), tokens[1].Value);
            Assert.AreEqual(2, tokens[0].Value);
            Assert.AreEqual(3, tokens[1].Value);
            Assert.AreEqual(source, tokens[0].Source.Source);
            Assert.AreEqual(source, tokens[1].Source.Source);
            Assert.AreEqual("2", tokens[0].Source.Snippet);
            Assert.AreEqual("3", tokens[1].Source.Snippet);

            // Expected tokens are: [Real=1.0] [Multiply:*]
            source = @"1.0     *    \\ 4 4";
            tokens = new List<Token>(Lexer.Lex(source));
            Assert.AreEqual(2, tokens.Count);
            Assert.IsInstanceOf(typeof(RealFieldElement), tokens[0].Value);
            Assert.AreEqual(1.0, tokens[0].Value);
            Assert.IsNull(tokens[1].Value);
            Assert.AreEqual(source, tokens[0].Source.Source);
            Assert.AreEqual(source, tokens[1].Source.Source);
            Assert.AreEqual("1.0", tokens[0].Source.Snippet);
            Assert.AreEqual("*", tokens[1].Source.Snippet);
        }
    }
}