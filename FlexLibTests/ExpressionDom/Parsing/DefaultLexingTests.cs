using System.Collections.Generic;

using NUnit.Framework;

using FlexLib.Algebra;
using FlexLib.ExpressionDom.Parsing;

namespace FlexLibTests.ExpressionDom.Parsing
{
    [TestFixture]
    public class DefaultLexingTests
    {
        protected RealField Field;

        [SetUp]
        protected void SetUp()
        {
            // Set up the real field.
            Field = new RealField(0.0001);
        }
    }
}