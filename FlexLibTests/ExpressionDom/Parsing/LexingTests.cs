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
        protected RealField Field;

        [SetUp]
        protected void SetUp()
        {
            // Set up the real field.
            Field = new RealField(0.0001);

            // Set up the lexer rules.
            IList<LexerRule> lexerRules = new List<LexerRule>();
            lexerRules.Add(new LexerRule
            {
                Name = "Comment",
                Pattern = @"\/{2}.*$",
                Ignore = true
            });
            lexerRules.Add(new LexerRule
            {
                Name = "Space",
                Pattern = @"\s+",
                Ignore = true
            });
            lexerRules.Add(new LexerRule<RealFieldElement>
            {
                Name = "Real",
                Pattern = @"\d*\.\d+",
                Parser = RealFieldElement.TryParse
            });
            lexerRules.Add(new LexerRule<int>
            {
                Name = "Integer",
                Pattern = @"\d+",
                Parser = int.TryParse
            });
            lexerRules.Add(new LexerRule
            {
                Name = "Multiply",
                Pattern = @"\*"
            });

            // Create lexer.
            Lexer = new Lexer(lexerRules);
        }

        [Test]
        public void TestBasic()
        {
            // Expected tokens are: [Integer=2] [Integer=3]
            string source = @"2 3 // 2";
            IList<Token> tokens = new List<Token>(Lexer.Lex(source));

            Assert.AreEqual(2, tokens.Count);
            Assert.IsInstanceOf(typeof(int), tokens[0].Value);
            Assert.IsInstanceOf(typeof(int), tokens[1].Value);
            Assert.AreEqual(2, tokens[0].Value);
            Assert.AreEqual(3, tokens[1].Value);
            Assert.AreEqual(source, tokens[0].Source.Source);
            Assert.AreEqual(source, tokens[1].Source.Source);
            Assert.AreEqual("2", tokens[0].Source.Snippet);
            Assert.AreEqual("3", tokens[1].Source.Snippet);
            Assert.AreEqual(0, tokens[0].Source.IndexStart);
            Assert.AreEqual(2, tokens[1].Source.IndexStart);
        }
        [Test]
        public void TestIgnoreWhitespace()
        {
            // Expected tokens are: [Real=1.0] [Multiply:*]
            string source = @" 1.0     * ";
            IList<Token> tokens = new List<Token>(Lexer.Lex(source));

            Assert.AreEqual(2, tokens.Count);
            Assert.IsInstanceOf(typeof(RealFieldElement), tokens[0].Value);
            Assert.IsTrue(Field.ElementsEqual(1.0, (RealFieldElement)tokens[0].Value));
            Assert.IsNull(tokens[1].Value);
            Assert.AreEqual(source, tokens[0].Source.Source);
            Assert.AreEqual(source, tokens[1].Source.Source);
            Assert.AreEqual("1.0", tokens[0].Source.Snippet);
            Assert.AreEqual("*", tokens[1].Source.Snippet);
            Assert.AreEqual(1, tokens[0].Source.IndexStart);
            Assert.AreEqual(9, tokens[1].Source.IndexStart);
        }
        [Test]
        public void TestIgnoreComments()
        {
            // Expected tokens are: None
            string source = @"// 1 + 2 + 3";
            IList<Token> tokens = new List<Token>(Lexer.Lex(source));

            Assert.AreEqual(0, tokens.Count);
        }
        [Test]
        public void TestMultipleTypes()
        {
            // Expected tokens are: [Integer=1] [Multiply:*] [Real=2.0] [Multiply:*] [Real=0.5]
            string source = @"1 * 2.0 * .5 // Multiplies one by two and a half";
            IList<Token> tokens = new List<Token>(Lexer.Lex(source));

            Assert.AreEqual(5, tokens.Count);
            Assert.IsInstanceOf(typeof(int), tokens[0].Value);
            Assert.IsInstanceOf(typeof(RealFieldElement), tokens[2].Value);
            Assert.IsInstanceOf(typeof(RealFieldElement), tokens[4].Value);
            Assert.AreEqual(1, tokens[0].Value);
            Assert.IsTrue(Field.ElementsEqual(2.0, (RealFieldElement)tokens[2].Value));
            Assert.IsTrue(Field.ElementsEqual(0.5, (RealFieldElement)tokens[4].Value));
            Assert.IsNull(tokens[1].Value);
            Assert.IsNull(tokens[3].Value);
            Assert.AreEqual(source, tokens[0].Source.Source);
            Assert.AreEqual(source, tokens[1].Source.Source);
            Assert.AreEqual(source, tokens[2].Source.Source);
            Assert.AreEqual(source, tokens[3].Source.Source);
            Assert.AreEqual(source, tokens[4].Source.Source);
            Assert.AreEqual("1", tokens[0].Source.Snippet);
            Assert.AreEqual("*", tokens[1].Source.Snippet);
            Assert.AreEqual("2.0", tokens[2].Source.Snippet);
            Assert.AreEqual("*", tokens[3].Source.Snippet);
            Assert.AreEqual(".5", tokens[4].Source.Snippet);
            Assert.AreEqual(0, tokens[0].Source.IndexStart);
            Assert.AreEqual(2, tokens[1].Source.IndexStart);
            Assert.AreEqual(4, tokens[2].Source.IndexStart);
            Assert.AreEqual(8, tokens[3].Source.IndexStart);
            Assert.AreEqual(10, tokens[4].Source.IndexStart);
        }
        [Test]
        public void TestNoRule()
        {
            // Expected lexer syntax exception.
            string source = @"a 1 b 2 c 3";
            LexerSyntaxException ex = Assert.Throws<LexerSyntaxException>(() =>
            {
                IList<Token> tokens = new List<Token>(Lexer.Lex(source));
            });

            Assert.AreEqual(source, ex.TokenSource.Source);
            Assert.AreEqual("a", ex.TokenSource.Snippet);
            Assert.AreEqual(0, ex.TokenSource.IndexStart);
        }
        [Test]
        public void TestNearMatch()
        {
            // Expected lexer syntax exception.
            string source = @"1.0 1. .1";
            LexerSyntaxException ex = Assert.Throws<LexerSyntaxException>(() =>
            {
                IList<Token> tokens = new List<Token>(Lexer.Lex(source));
            });

            Assert.AreEqual(source, ex.TokenSource.Source);
            Assert.AreEqual(".", ex.TokenSource.Snippet);
            Assert.AreEqual(5, ex.TokenSource.IndexStart);
        }
        [Test]
        public void TestNonStrict()
        {
            // Expected tokens are (non-strict): [Integer=1] [Integer=2] [Integer=3]
            string source = @"a 1 b 2 c 3";
            Assert.DoesNotThrow(() =>
            {
                IList<Token> tokens = new List<Token>(Lexer.Lex(source, strict: false));

                Assert.AreEqual(3, tokens.Count);
                Assert.IsInstanceOf(typeof(int), tokens[0].Value);
                Assert.IsInstanceOf(typeof(int), tokens[1].Value);
                Assert.IsInstanceOf(typeof(int), tokens[2].Value);
                Assert.AreEqual(1, tokens[0].Value);
                Assert.AreEqual(2, tokens[1].Value);
                Assert.AreEqual(3, tokens[2].Value);
                Assert.AreEqual(source, tokens[0].Source.Source);
                Assert.AreEqual(source, tokens[1].Source.Source);
                Assert.AreEqual(source, tokens[2].Source.Source);
                Assert.AreEqual("1", tokens[0].Source.Snippet);
                Assert.AreEqual("2", tokens[1].Source.Snippet);
                Assert.AreEqual("3", tokens[2].Source.Snippet);
                Assert.AreEqual(2, tokens[0].Source.IndexStart);
                Assert.AreEqual(6, tokens[1].Source.IndexStart);
                Assert.AreEqual(10, tokens[2].Source.IndexStart);
            });
        }
    }
}