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

        public SineTerm Derivative(int degree=1)
        {
            int phase = (Type == SinusoidalType.SINE ? 0 : 1) + degree;
            return new SineTerm(
                phase % 2 == 0 ? SinusoidalType.SINE : SinusoidalType.COSINE,
                ((phase % 4) / 2 == 0 ? 1 : -1) * Amplitude * Math.Pow(Frequency, degree),
                Frequency, Shift);
        }

        public SineTerm Antiderivative(int degree=1)
        {
            int phase = (Type == SinusoidalType.SINE ? 1 : 0) + degree;
            return new SineTerm(
                phase % 2 == 0 ? SinusoidalType.COSINE : SinusoidalType.SINE,
                ((phase % 4) / 2 == 0 ? 1 : -1) * Amplitude / Math.Pow(Frequency, degree),
                Frequency, Shift);
        }
    }
}
