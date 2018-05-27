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

        public PolynomialTerm Derivative(int degree=1)
        {
            PolynomialTerm derivative = new PolynomialTerm(Coefficient, Degree);
            for (int i = 0; i < degree; i++)
            {
                derivative.Coefficient *= derivative.Degree;
                derivative.Degree -= 1;
            }
            return derivative;
        }

        public PolynomialTerm Antiderivative(int degree=1)
        {
            PolynomialTerm antiderivative = new PolynomialTerm(Coefficient, Degree);
            for (int i = 0; i < degree; i++)
            {
                if (antiderivative.Degree == -1.0)
                {
                    antiderivative.Coefficient = double.NaN;
                    antiderivative.Degree = 0.0;
                }
                else
                {
                    antiderivative.Coefficient /= antiderivative.Degree + 1.0;
                    antiderivative.Degree += 1.0;
                }
            }
            return antiderivative;
        }
    }
}
