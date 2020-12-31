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
        protected Parser Parser;
        protected RealField Field;

        [SetUp]
        protected void SetUp()
        {
            Field = new RealField(0.0001);

            LexerRule lexRuleReal = new LexerRule { Name = "Real" };
            LexerRule lexRuleOpAdd = new LexerRule { Name = "Op+" };
            LexerRule lexRuleOpSub = new LexerRule { Name = "Op-" };
            LexerRule lexRuleOpExp = new LexerRule { Name = "Op^" };

            ParserRule parseRuleReal = new ParserRule
            <
                RealFieldElement,
                IExpression<RealFieldElement>
            >
            {
                Name = "Real",
                Pattern = new TokenRulePattern<RealFieldElement> { Rule = lexRuleReal, Parameter = 0 },
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
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 0},
                        new TokenRulePattern { Rule = lexRuleOpAdd },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 1}
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
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 0 },
                        new TokenRulePattern { Rule = lexRuleOpSub },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 1 }
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
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 0 },
                        new TokenRulePattern { Rule = lexRuleOpExp },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Rule = lexRuleReal, Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) => new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>((a, b) => Math.Pow(a.Value, b.Value), realA, realB)
            };

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
    }
}