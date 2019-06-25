using System.Collections.Generic;
using System.Xml.Linq;
using FlexLib.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlexLibTests.Parsing
{
    [TestClass]
    public class TokenizerTest
    {
        private string TokenizerContextString;
        private TokenizerContext TokenizerContext;
        private Tokenizer Tokenizer;

        [TestInitialize]
        public void TestInitialize()
        {
            TokenizerContextString =
            @"
            <tokens>
                <token name=""integer"">
                    <patterns>
                        <pattern>\d+</pattern>
                    </patterns>
                </token>
                <token name=""plus"">
                    <patterns>
                        <pattern>plus</pattern>
                        <pattern>\+</pattern>
                    </patterns>
                    <ligature>+</ligature>
                </token>
                <token name=""minus"">
                    <patterns>
                        <pattern>minus</pattern>
                        <pattern>\-</pattern>
                    </patterns>
                    <ligature>-</ligature>
                </token>
            </tokens>
            ";

            TokenizerContext = TokenizerContext.FromXml(XElement.Parse(TokenizerContextString));

            Tokenizer = new Tokenizer(TokenizerContext);
        }

        [TestMethod]
        public void TestTokenizerContext()
        {
            Assert.AreEqual(3, TokenizerContext.Count);

            Assert.AreEqual("integer", TokenizerContext[0].Name);
            Assert.AreEqual(1, TokenizerContext[0].RegexPatterns.Count);
            Assert.AreEqual(@"\d+", TokenizerContext[0].RegexPatterns[0]);

            Assert.AreEqual("plus", TokenizerContext[1].Name);
            Assert.AreEqual(2, TokenizerContext[1].RegexPatterns.Count);
            Assert.AreEqual(@"plus", TokenizerContext[1].RegexPatterns[0]);
            Assert.AreEqual(@"\+", TokenizerContext[1].RegexPatterns[1]);

            Assert.AreEqual("minus", TokenizerContext[2].Name);
            Assert.AreEqual(2, TokenizerContext[2].RegexPatterns.Count);
            Assert.AreEqual(@"minus", TokenizerContext[2].RegexPatterns[0]);
            Assert.AreEqual(@"\-", TokenizerContext[2].RegexPatterns[1]);
        }

        [TestMethod]
        public void TestTokenizer()
        {
            List<Token> tokensA = new List<Token>(Tokenizer.Tokenize("1 + 2 - 3"));
            List<Token> tokensB = new List<Token>(Tokenizer.Tokenize("1 plus 2 minus 3 plus 4"));

            Assert.AreEqual(5, tokensA.Count);
            Assert.AreEqual("integer", tokensA[0].Type);
            Assert.AreEqual("1", tokensA[0].ToString());
            Assert.AreEqual("plus", tokensA[1].Type);
            Assert.AreEqual("+", tokensA[1].ToString());
            Assert.AreEqual("integer", tokensA[2].Type);
            Assert.AreEqual("2", tokensA[2].ToString());
            Assert.AreEqual("minus", tokensA[3].Type);
            Assert.AreEqual("-", tokensA[3].ToString());
            Assert.AreEqual("integer", tokensA[4].Type);
            Assert.AreEqual("3", tokensA[4].ToString());

            Assert.AreEqual(7, tokensB.Count);
            Assert.AreEqual("integer", tokensB[0].Type);
            Assert.AreEqual("1", tokensB[0].ToString());
            Assert.AreEqual("plus", tokensB[1].Type);
            Assert.AreEqual("+", tokensB[1].ToString());
            Assert.AreEqual("integer", tokensB[2].Type);
            Assert.AreEqual("2", tokensB[2].ToString());
            Assert.AreEqual("minus", tokensB[3].Type);
            Assert.AreEqual("-", tokensB[3].ToString());
            Assert.AreEqual("integer", tokensB[4].Type);
            Assert.AreEqual("3", tokensB[4].ToString());
            Assert.AreEqual("plus", tokensB[5].Type);
            Assert.AreEqual("+", tokensB[5].ToString());
            Assert.AreEqual("integer", tokensB[6].Type);
            Assert.AreEqual("4", tokensB[6].ToString());
        }
    }
}
