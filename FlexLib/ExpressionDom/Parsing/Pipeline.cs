using System;
using System.Collections.Generic;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;

namespace FlexLib.ExpressionDom.Parsing
{
    /// <summary>
    /// Represents a lexer and parser combined into a general parsing pipeline.
    /// </summary>
    public class Pipeline
    {
        /// <summary>
        /// The lexer of the pipeline that preprocesses parser inputs.
        /// </summary>
        public readonly Lexer Lexer;
        /// <summary>
        /// The parser of the pipeline that postprocesses lexer outputs.
        /// </summary>
        public readonly Parser Parser;

        /// <summary>
        /// Creates a new <see cref="Pipeline"/> object with a specified lexer and parser.
        /// </summary>
        /// <param name="lexer">The lexer.</param>
        /// <param name="parser">The parser.</param>
        public Pipeline(Lexer lexer, Parser parser)
        {
            Lexer = lexer;
            Parser = parser;
        }

        /// <summary>
        /// Parses a source stream into a token stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="strict">Whether syntax errors should be thrown.</param>
        /// <returns></returns>
        public IEnumerable<Token> Parse(string source, bool strict = true)
        {
            IEnumerable<Token> lexerOut = Lexer.Lex(source, strict);
            IEnumerable<Token> parserOut = Parser.Parse(lexerOut);
            return parserOut;
        }
        /// <summary>
        /// Parses a source stream into a single token.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="strict">Whether syntax errors should be thrown.</param>
        /// <returns></returns>
        public Token ParseSingular(string source, bool strict = true)
        {
            IEnumerable<Token> lexerOut = Lexer.Lex(source, strict);
            Token parserOut = Parser.ParseSingular(lexerOut);
            return parserOut;
        }

        /// <summary>
        /// The default pipeline for general arithmetic and algebra.
        /// </summary>
        public static readonly Pipeline DefaultPipeline;

        /// <summary>
        /// The default real field.
        /// </summary>
        public static readonly RealField DefaultRealField;

        /// <summary>
        /// Sets up all of the default lexers, parsers, and pipelines.
        /// </summary>
        static Pipeline()
        {
            DefaultRealField = new RealField(Math.Pow(2, -8));
            DefaultPipeline = CreateDefaultPipeline();
        }

        /// <summary>
        /// Creates the default pipeline.
        /// </summary>
        /// <returns>The default pipeline.</returns>
        private static Pipeline CreateDefaultPipeline()
        {
            // Create all the necessary lexer rules.
            LexerRule LexerRuleSpace = new LexerRule
            {
                Name = "Space",
                Pattern = @"\s+",
                Ignore = true
            };
            LexerRule LexerRuleReal = new LexerRule<RealFieldElement>
            {
                Name = "Real",
                Pattern = @"\d*\.?\d+",
                Parser = RealFieldElement.TryParse
            };
            LexerRule LexerRuleOpAdd = new LexerRule
            {
                Name = "Operator+",
                Pattern = @"\+"
            };
            LexerRule LexerRuleOpSub = new LexerRule
            {
                Name = "Operator-",
                Pattern = @"\-"
            };
            LexerRule LexerRuleOpMul = new LexerRule
            {
                Name = "Operator*",
                Pattern = @"\*"
            };
            LexerRule LexerRuleOpDiv = new LexerRule
            {
                Name = "Operator/",
                Pattern = @"\/"
            };
            LexerRule LexerRuleOpenParen = new LexerRule
            {
                Name = "Open Paren",
                Pattern = @"\("
            };
            LexerRule LexerRuleCloseParen = new LexerRule
            {
                Name = "Close Paren",
                Pattern = @"\)"
            };

            // Create all the necessary parser rules.
            ParserRule ParserRuleReal = new ParserRule
            <
                RealFieldElement,
                ConstantExpression<RealFieldElement>
            >
            {
                Name = "Real",
                Pattern = new TokenRulePattern<RealFieldElement> { Parameter = 0 },
                Transform = (real) => new ConstantExpression<RealFieldElement>(real)
            };
            ParserRule ParserRuleFactor = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
            >
            {
                Name = "Factor",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpenParen },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 },
                        new TokenRulePattern { Rule = LexerRuleCloseParen }
                    }
                ),
                Transform = (realA, realB) =>
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
                    (DefaultRealField.Multiply, realA, realB)
            };
            ParserRule ParserRuleParen = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>
            >
            {
                Name = "Parentheses",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern { Rule = LexerRuleOpenParen },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleCloseParen }
                    }
                ),
                Transform = (real) => real
            };
            ParserRule ParserRuleNeg = new ParserRule
            <
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement>
            >
            {
                Name = "Negative",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern { Rule = LexerRuleOpSub },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 }
                    }
                ),
                Transform = (real) =>
                    new OperationExpression<RealFieldElement, RealFieldElement>
                    (DefaultRealField.Negative, real)
            };
            ParserRule ParserRuleMul = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
            >
            {
                Name = "Product",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpMul },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) =>
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
                    (DefaultRealField.Multiply, realA, realB)
            };
            ParserRule ParserRuleDiv = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
            >
            {
                Name = "Quotient",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpDiv },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) =>
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
                    (DefaultRealField.Divide, realA, realB)
            };
            ParserRule ParserRuleAdd = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
            >
            {
                Name = "Sum",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpAdd },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) =>
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
                    (DefaultRealField.Add, realA, realB)
            };
            ParserRule ParserRuleSub = new ParserRule
            <
                IExpression<RealFieldElement>,
                IExpression<RealFieldElement>,
                OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
            >
            {
                Name = "Difference",
                Pattern = new TokenGroupPattern
                (
                    new ITokenPattern[]
                    {
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 0 },
                        new TokenRulePattern { Rule = LexerRuleOpSub },
                        new TokenRulePattern<IExpression<RealFieldElement>> { Parameter = 1 }
                    }
                ),
                Transform = (realA, realB) =>
                    new OperationExpression<RealFieldElement, RealFieldElement, RealFieldElement>
                    (DefaultRealField.Subtract, realA, realB)
            };

            // Create all the necessary parser levels.
            ParserLevel ParserLevelTypes = new ParserLevel
            (
                new ParserRule[]
                {
                    ParserRuleReal
                }
            );
            ParserLevel ParserLevelParens = new ParserLevel
            (
                new ParserRule[]
                {
                    ParserRuleFactor,
                    ParserRuleParen
                }
            );
            ParserLevel ParserLevelMulDiv = new ParserLevel
            (
                new ParserRule[]
                {
                    ParserRuleMul,
                    ParserRuleDiv
                }
            );
            ParserLevel ParserLevelAddSub = new ParserLevel
            (
                new ParserRule[]
                {
                    ParserRuleAdd,
                    ParserRuleSub
                }
            );
            ParserLevel ParserLevelNeg = new ParserLevel
            (
                new ParserRule[]
                {
                    ParserRuleNeg
                }
            );

            // Create lexer and parser.
            Lexer Lexer = new Lexer
            (
                new LexerRule[]
                {
                    LexerRuleSpace,
                    LexerRuleReal,
                    LexerRuleOpAdd,
                    LexerRuleOpSub,
                    LexerRuleOpMul,
                    LexerRuleOpDiv,
                    LexerRuleOpenParen,
                    LexerRuleCloseParen
                }
            );
            Parser Parser = new Parser
            (
                new ParserLevel[]
                {
                    ParserLevelTypes,
                    ParserLevelParens,
                    ParserLevelMulDiv,
                    ParserLevelAddSub,
                    ParserLevelNeg
                }
            );

            // Create and return the pipeline.
            return new Pipeline(Lexer, Parser);
        }
    }
}