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
                    ),
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
                new Token(null, LexerRuleReal, new RealFieldElement(3.0)),
                new Token(null, LexerRuleOpExp),
                new Token(null, LexerRuleReal, new RealFieldElement(2.0)),
                new Token(null, LexerRuleOpAdd),
                new Token(null, LexerRuleReal, new RealFieldElement(4.0)),
                new Token(null, LexerRuleOpExp),
                new Token(null, LexerRuleReal, new RealFieldElement(2.0))
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
            Assert.IsTrue(Field.ElementsEqual(3.0, const1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(4.0, const3.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(2.0, const4.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(25.0, op1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(9.0, op2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(16.0, op3.Evaluate()));
        }
    }
}