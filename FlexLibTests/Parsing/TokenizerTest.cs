﻿using System.Collections.Generic;
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
                </token>
                <token name=""minus"">
                    <patterns>
                        <pattern>minus</pattern>
                        <pattern>\-</pattern>
                    </patterns>
                </token>
            </tokens>
            ";

            TokenizerContext = TokenizerContext.FromXml(XDocument.Parse(TokenizerContextString));

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
            List<Token> tokensB = new List<Token>(Tokenizer.Tokenize("1 plus 2 minus 3"));

            Assert.AreEqual(5, tokensA.Count);
            Assert.AreEqual("integer", tokensA[0].Type);
            Assert.AreEqual("plus", tokensA[1].Type);
            Assert.AreEqual("integer", tokensA[2].Type);
            Assert.AreEqual("minus", tokensA[3].Type);
            Assert.AreEqual("integer", tokensA[4].Type);

            Assert.AreEqual(5, tokensB.Count);
            Assert.AreEqual("integer", tokensB[0].Type);
            Assert.AreEqual("plus", tokensB[1].Type);
            Assert.AreEqual("integer", tokensB[2].Type);
            Assert.AreEqual("minus", tokensB[3].Type);
            Assert.AreEqual("integer", tokensB[4].Type);
        }
    }
}
