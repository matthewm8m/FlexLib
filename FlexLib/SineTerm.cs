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
            Type = type;
            Amplitude = amp;
            Frequency = freq;
            Shift = shift;
        }

        public double Evaluate(double indeterminate)
        {
            switch (Type)
            {
                case SinusoidalType.SINE:
                    return Amplitude * Math.Sin(Frequency * indeterminate + Shift);
                case SinusoidalType.COSINE:
                    return Amplitude * Math.Cos(Frequency * indeterminate + Shift);
                default:
                    return 0.0;
            }
        }

        public SineTerm Derivative()
        {
            switch (Type)
            {
                case SinusoidalType.SINE:
                    return new SineTerm(SinusoidalType.COSINE, Amplitude * Frequency, Frequency, Shift);
                case SinusoidalType.COSINE:
                    return new SineTerm(SinusoidalType.SINE, -Amplitude * Frequency, Frequency, Shift);
                default:
                    return null;
            }
        }

        public SineTerm Antiderivative()
        {
            switch (Type)
            {
                case SinusoidalType.SINE:
                    return new SineTerm(SinusoidalType.COSINE, -Amplitude / Frequency, Frequency, Shift);
                case SinusoidalType.COSINE:
                    return new SineTerm(SinusoidalType.SINE, Amplitude / Frequency, Frequency, Shift);
                default:
                    return null;
            }
        }
    }
}
