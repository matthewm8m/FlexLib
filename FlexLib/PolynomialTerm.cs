using System;

namespace FlexLib
{
    public class PolynomialTerm
    {
        public double Coefficient { get; set; }
        public double Degree { get; set; }

        public PolynomialTerm(double coeff, double degree)
        {
            Coefficient = coeff;
            Degree = degree;
        }

        public double Evaluate(double indeterminate)
        {
            return Coefficient * Math.Pow(indeterminate, Degree);
        }

        public PolynomialTerm Derivative()
        {
            return new PolynomialTerm(Coefficient * Degree, Degree - 1);
        }

        public PolynomialTerm AntiDerivative()
        {
            if (Degree == -1)
                return null;
            else
                return new PolynomialTerm(Coefficient / (Degree + 1.0), Degree + 1.0);
        }
    }
}
