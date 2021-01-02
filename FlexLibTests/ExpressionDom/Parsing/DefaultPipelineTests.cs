using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Expressions;
using FlexLib.ExpressionDom.Parsing;

namespace FlexLibTests.ExpressionDom.Parsing
{
    [TestFixture]
    public class DefaultPipelineTests
    {
        protected Pipeline Pipeline;
        protected RealField Field;

        [SetUp]
        protected void SetUp()
        {
            Pipeline = Pipeline.DefaultPipeline;
            Field = Pipeline.DefaultRealField;
        }

        [Test]
        public void TestAddition()
        {
            Token result = Pipeline.ParseSingular("1 + .5");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(1.5, expr.Evaluate()));
        }
        [Test]
        public void TestSubtraction()
        {
            Token result = Pipeline.ParseSingular("3.0 - 1.5");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(1.5, expr.Evaluate()));
        }
        [Test]
        public void TestMultiplication()
        {
            Token result = Pipeline.ParseSingular("2 * .5");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(1.0, expr.Evaluate()));
        }
        [Test]
        public void TestDivision()
        {
            Token result = Pipeline.ParseSingular("7/8");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(0.875, expr.Evaluate()));
        }
        [Test]
        public void TestNegative()
        {
            Token result = Pipeline.ParseSingular("-1 + -2");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(-3.0, expr.Evaluate()));
        }
        [Test]
        public void TestParentheses()
        {
            Token result = Pipeline.ParseSingular("3 / (4 - 1)");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(1.0, expr.Evaluate()));
        }
        [Test]
        public void TestFactor()
        {
            Token result = Pipeline.ParseSingular("2 (3 - 1)");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(4.0, expr.Evaluate()));
        }
        [Test]
        public void TestDoubleFactor()
        {
            Token result = Pipeline.ParseSingular("(2 + 3) (4 - 2)");

            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), result.Value);
            IExpression<RealFieldElement> expr = result.Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(10.0, expr.Evaluate()));
        }
        [Test]
        public void TestSyntaxException()
        {
            LexerSyntaxException ex = Assert.Throws<LexerSyntaxException>
            (
                () => Pipeline.ParseSingular("2 + x")
            );
            Assert.AreEqual("x", ex.TokenSource.Snippet);
        }
        [Test]
        public void TestNearSyntaxException()
        {
            LexerSyntaxException ex = Assert.Throws<LexerSyntaxException>
            (
                () => Pipeline.ParseSingular("2. + 1.")
            );
            Assert.AreEqual(".", ex.TokenSource.Snippet);
        }
        [Test]
        public void TestMultiple()
        {
            IList<Token> results = new List<Token>(Pipeline.Parse("3 - .1 2.1 * -1.0 3/2/1"));

            Assert.AreEqual(3, results.Count);
            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), results[0].Value);
            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), results[1].Value);
            Assert.IsInstanceOf(typeof(IExpression<RealFieldElement>), results[2].Value);
            IExpression<RealFieldElement> expr1 = results[0].Value as IExpression<RealFieldElement>;
            IExpression<RealFieldElement> expr2 = results[1].Value as IExpression<RealFieldElement>;
            IExpression<RealFieldElement> expr3 = results[2].Value as IExpression<RealFieldElement>;
            Assert.IsTrue(Field.ElementsEqual(2.9, expr1.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(-2.1, expr2.Evaluate()));
            Assert.IsTrue(Field.ElementsEqual(1.5, expr3.Evaluate()));
        }
        [Test]
        public void TestIncompleteException()
        {
            ParserIncompleteException ex = Assert.Throws<ParserIncompleteException>
            (
                () => Pipeline.ParseSingular("1 - 2 3 - 4 5 - 6")
            );
            Assert.AreEqual("3 - 4", ex.TokenSource.Snippet);
        }
    }
}