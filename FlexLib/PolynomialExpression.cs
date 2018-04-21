using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexLib
{
    class PolynomialExpression
    {
        private PolynomialTerm[] Terms;

        public PolynomialExpression(PolynomialTerm[] terms)
        {
            Terms = terms;
        }
        public PolynomialExpression(IEnumerable<PolynomialTerm> terms)
        {
            Terms = terms.ToArray();
        }

        public double Evaluate(double indeterminate)
        {
            return 0;
        }

        public PolynomialExpression Derivative()
        {
            return null;
        }

        public PolynomialExpression Antiderivative()
        {
            return null;
        }
    }
}
