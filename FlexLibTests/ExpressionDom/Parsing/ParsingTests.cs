using System;
using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;
using FlexLib.ExpressionDom.Parsing;

namespace FlexLibTests.ExpressionDom.Parsing
{
    [TestFixture]
    public class ParsingTests
    {
        protected LexerRule LexerRuleReal;
        protected LexerRule LexerRuleOpAdd;
        protected LexerRule LexerRuleOpSub;
        protected LexerRule LexerRuleOpExp;

        protected Parser Parser;
        protected RealField Field;

        [SetUp]
        protected void SetUp()
        {
            Field = new RealField(0.0001);

            // Set up the lexer rules.
            LexerRuleReal = new LexerRule { Name = "Real" };
            LexerRuleOpAdd = new LexerRule { Name = "Op+" };
            LexerRuleOpSub = new LexerRule { Name = "Op-" };
            LexerRuleOpExp = new LexerRule { Name = "Op^" };

            // Set up the parser rules.
            ParserRule parseRuleReal = new ParserRule
            <
                RealFieldElement,
                IExpression<RealFieldElement>
            >
            {
                Name = "Real",
                Pattern = new TokenRulePattern<RealFieldElement> { Parameter = 0 },
                Transform = (real) => new ConstantExpression<RealFieldElement>(real)
            };
            ParserRule parseRuleAdd = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>
            >
            {
                Name = "Sum",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0},
                        new TokenRulePattern { Rule = LexerRuleOpAdd },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1}
                    }
                ),
                Transform = (realA, realB) => new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>(Field.Add, realA, realB)
            };
            ParserRule parseRuleSub = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>
            >
            {
                Name = "Diff",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpSub },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) => new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>((a, b) => Field.Add(a, Field.Negative(b)), realA, realB)
            };
            ParserRule parseRuleExp = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>
            >
            {
                Name = "Exp",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpExp },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) => new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>((a, b) => Math.Pow(a.Value, b.Value), realA, realB)
            };

            // Create parser.
            Parser = new Parser
            (
                new ParserLevel[]
                {
                    new ParserLevel
                    (
                        new ParserRule[]
                        {
                            parseRuleReal
                        }
                    ),
                    new ParserLevel
                    (
                        new ParserRule[]
                        {
                            parseRuleExp
                        }
                    ) { Associativity = ParserAssociativity.RightToLeft },
                    new ParserLevel
                    (
                        new ParserRule[]
                        {
                            parseRuleAdd,
                            parseRuleSub
                        }
                    ),
                }
            );
        }

        [Test]
        public void TestBasic()
        {
            // input: 3^2 + 4^2
            // tokens: [Real=3] [Op^] [Real=2] [Op+] [Real=4] [Op^] [Real=2]
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, value: new RealFieldElement(3.0)),
                new Token(LexerRuleOpExp),
                new Token(LexerRuleReal, value: new RealFieldElement(2.0)),
                new Token(LexerRuleOpAdd),
                new Token(LexerRuleReal, value: new RealFieldElement(4.0)),
                new Token(LexerRuleOpExp),
                new Token(LexerRuleReal, value: new RealFieldElement(2.0))
            };

            // Check parsing results.
            Token result = Parser.ParseSingular(tokens);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), result.Value);
            var op1 = result.Value as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), op1.Operand1);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), op1.Operand2);
            var op2 = op1.Operand1 as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            var op3 = op1.Operand2 as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand2);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op3.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op3.Operand2);
            var const1 = op2.Operand1 as ConstantExpression<RealFieldElement>;
            var const2 = op2.Operand2 as ConstantExpression<RealFieldElement>;
            var const3 = op3.Operand1 as ConstantExpression<RealFieldElement>;
            var const4 = op3.Operand2 as ConstantExpression<RealFieldElement>;

            Assert.IsTrue(Field.ElementsEqual(25.0, op1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(9.0, op2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(16.0, op3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, const1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(4.0, const3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const4.Evaluate()));
        }

        [Test]
        public void TestLeftToRightAssociativity()
        {
            // input: 4 - 3 + 2 - 1
            // tokens: [Real=4] [Op-] [Real=3] [Op+] [Real=2] [Op-] [Real=1]
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, value: new RealFieldElement(4.0)),
                new Token(LexerRuleOpSub),
                new Token(LexerRuleReal, value: new RealFieldElement(3.0)),
                new Token(LexerRuleOpAdd),
                new Token(LexerRuleReal, value: new RealFieldElement(2.0)),
                new Token(LexerRuleOpSub),
                new Token(LexerRuleReal, value: new RealFieldElement(1.0))
            };

            // Check parsing results.
            Token result = Parser.ParseSingular(tokens);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), result.Value);
            var op1 = result.Value as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), op1.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op1.Operand2);
            var op2 = op1.Operand1 as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            var const4 = op1.Operand2 as ConstantExpression<RealFieldElement>;
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), op2.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand2);
            var op3 = op2.Operand1 as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            var const3 = op2.Operand2 as ConstantExpression<RealFieldElement>;
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op3.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op3.Operand2);
            var const1 = op3.Operand1 as ConstantExpression<RealFieldElement>;
            var const2 = op3.Operand2 as ConstantExpression<RealFieldElement>;

            Assert.IsTrue(Field.ElementsEqual(2.0, op1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, op2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(1.0, op3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(4.0, const1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, const2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(1.0, const4.Evaluate()));
        }

        [Test]
        public void TestRightToLeftAssociativity()
        {
            // input: 4 ^ 3 ^ 2
            // tokens: [Real=4] [Op^] [Real=3] [Op^] [Real=2]
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, value: new RealFieldElement(4.0)),
                new Token(LexerRuleOpExp),
                new Token(LexerRuleReal, value: new RealFieldElement(3.0)),
                new Token(LexerRuleOpExp),
                new Token(LexerRuleReal, value: new RealFieldElement(2.0))
            };

            // Check parsing results.
            Token result = Parser.ParseSingular(tokens);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), result.Value);
            var op1 = result.Value as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op1.Operand1);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), op1.Operand2);
            var const1 = op1.Operand1 as ConstantExpression<RealFieldElement>;
            var op2 = op1.Operand2 as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand2);
            var const2 = op2.Operand1 as ConstantExpression<RealFieldElement>;
            var const3 = op2.Operand2 as ConstantExpression<RealFieldElement>;

            Assert.IsTrue(Field.ElementsEqual(262144.0, op1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(9.0, op2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(4.0, const1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, const2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const3.Evaluate()));
        }

        [Test]
        public void TestMultipleReduction()
        {
            // input: 10 + 3 10 - 3
            // tokens: [Real=10] [Op+] [Real=3] [Real=10] [Op-] [Real=3]
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, value: new RealFieldElement(10.0)),
                new Token(LexerRuleOpAdd),
                new Token(LexerRuleReal, value: new RealFieldElement(3.0)),
                new Token(LexerRuleReal, value: new RealFieldElement(10.0)),
                new Token(LexerRuleOpSub),
                new Token(LexerRuleReal, value: new RealFieldElement(3.0))
            };

            // Check parsing results.
            IList<Token> results = new List<Token>(Parser.Parse(tokens));
            Assert.AreEqual(2, results.Count);
            Token result1 = results[0];
            Token result2 = results[1];
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), result1.Value);
            Assert.IsInstanceOf(typeof(OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>), result2.Value);
            var op1 = result1.Value as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            var op2 = result2.Value as OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>;
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op1.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op1.Operand2);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand1);
            Assert.IsInstanceOf(typeof(ConstantExpression<RealFieldElement>), op2.Operand2);
            var const1 = op1.Operand1 as ConstantExpression<RealFieldElement>;
            var const2 = op1.Operand2 as ConstantExpression<RealFieldElement>;
            var const3 = op2.Operand1 as ConstantExpression<RealFieldElement>;
            var const4 = op2.Operand2 as ConstantExpression<RealFieldElement>;

            Assert.IsTrue(Field.ElementsEqual(13.0, op1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(7.0, op2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(10.0, const1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, const2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(10.0, const3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(3.0, const4.Evaluate()));
        }

        [Test]
        public void TestSingularReduction()
        {
            // input: 1 - 3
            // tokens: [Real=1] [Op-] [Real=3]
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, value: new RealFieldElement(1.0)),
                new Token(LexerRuleOpSub),
                new Token(LexerRuleReal, value: new RealFieldElement(3.0))
            };

            // Check ability to parse singular.
            Assert.DoesNotThrow
            (
                () => Parser.ParseSingular(tokens)
            );
        }

        [Test]
        public void TestSingularReductionException()
        {
            // input: 1 ^ 2 3 ^ 4
            // tokens: [Real=1.0] [Op^] [Real=2.0] [Real=3.0] [Op^] [Real=4.0]
            string input = "1 ^ 2 3 ^ 4";
            IEnumerable<Token> tokens = new Token[]
            {
                new Token(LexerRuleReal, new TokenSource(input, "1", 0), value: new RealFieldElement(1.0)),
                new Token(LexerRuleOpExp, new TokenSource(input, "^", 2)),
                new Token(LexerRuleReal, new TokenSource(input, "2", 4), value: new RealFieldElement(2.0)),
                new Token(LexerRuleReal, new TokenSource(input, "3", 6), value: new RealFieldElement(3.0)),
                new Token(LexerRuleOpExp, new TokenSource(input, "^", 8)),
                new Token(LexerRuleReal, new TokenSource(input, "4", 10), value: new RealFieldElement(4.0))
            };

            // Check inability to parse singular.
            ParserIncompleteException ex = Assert.Throws<ParserIncompleteException>
            (
                () => Parser.ParseSingular(tokens)
            );
            Assert.AreEqual(input, ex.TokenSource.Source);
            Assert.AreEqual("3^4", ex.TokenSource.Snippet);
            Assert.AreEqual(6, ex.TokenSource.Location);
        }
    }
}