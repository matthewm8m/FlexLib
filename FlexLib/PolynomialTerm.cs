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
            if (Coefficient == 0.0)
                return 0.0;
            return Coefficient * Math.Pow(indeterminate, Degree);
        }

        public PolynomialTerm Derivative()
        {
            return new PolynomialTerm(Coefficient * Degree, Degree - 1.0);
        }

        public PolynomialTerm Antiderivative()
        {
            if (Degree == -1.0)
                return new PolynomialTerm(double.NaN, 0.0);
            else
                return new PolynomialTerm(Coefficient / (Degree + 1.0), Degree + 1.0);
        }
    }
}
