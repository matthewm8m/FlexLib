using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexLib
{
    public class SineTerm
    {
        public enum SinusoidalType { SINE, COSINE }

        public SinusoidalType Type { get; private set; }
        public double Amplitude { get; set; }
        public double Frequency { get; set; }
        public double Shift { get; set; }

        public SineTerm(SinusoidalType type, double amp, double freq, double shift)
        {

        }

        public double Evaluate(double indeterminate)
        {
            return 0;
        }

        public SineTerm Derivative()
        {
            return null;
        }

        public SineTerm Antiderivative()
        {
            return null;
        }
    }
}
