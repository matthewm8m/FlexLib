using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexLib
{
    public class PolynomialExpression
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
        public PolynomialExpression(double[] coeffs, double[] degrees)
        {

        }
        public PolynomialExpression(IEnumerable<double> coeffs, IEnumerable<double> degrees)
        {

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
