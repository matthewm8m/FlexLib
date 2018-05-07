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
        public PolynomialExpression(IEnumerable<PolynomialTerm> terms) :
            this(terms.ToArray()) {}
        public PolynomialExpression(double[] coeffs, double[] degrees)
        {
            // Throw error if arrays are not the same size
            if (coeffs.Length != degrees.Length)
            {
                throw new ArgumentException("Coefficient and Degree collections are not the same size.");
            }

            // Create terms array
            Terms = new PolynomialTerm[coeffs.Length];
            for (int i = 0; i < coeffs.Length; i++)
            {
                Terms[i] = new PolynomialTerm(coeffs[i], degrees[i]);
            }
        }
        public PolynomialExpression(IEnumerable<double> coeffs, IEnumerable<double> degrees) :
            this(coeffs.ToArray(), degrees.ToArray()) {}

        public double Evaluate(double indeterminate)
        {
            double total = 0;
            foreach (PolynomialTerm term in Terms)
            {
                total += term.Evaluate(indeterminate);
            }
            return total;
        }

        public PolynomialExpression Derivative()
        {
            PolynomialTerm[] TermsPrime = new PolynomialTerm[Terms.Length];
            for (int i = 0; i < Terms.Length; i++)
            {
                TermsPrime[i] = Terms[i].Derivative();
            }
            return new PolynomialExpression(TermsPrime);
        }

        public PolynomialExpression Antiderivative()
        {
            return null;
        }
    }
}
