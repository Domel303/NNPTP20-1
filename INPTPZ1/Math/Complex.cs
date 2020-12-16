using System;

namespace INPTPZ1
{
    public class Complex
    {
        public static readonly int Power = 2;
        private static readonly double RootLimit = 0.01;
        public double Real { get; set; }
        public double Imaginary { get; set; }
        public readonly static Complex Zero = new Complex()
        {
            Real = 0,
            Imaginary = 0
        };

        public Complex Multiply(Complex multiple)
        {
            return new Complex()
            {
                Real = Real * multiple.Real - Imaginary * multiple.Imaginary,
                Imaginary = Real * multiple.Imaginary + Imaginary * multiple.Real
            };
        }

        public Complex Add(Complex addition)
        {         
            return new Complex()
            {
                Real = Real + addition.Real,
                Imaginary = Imaginary + addition.Imaginary
            };
        }

        public Complex Subtract(Complex subtractor)
        { 
            return new Complex()
            {
                Real = Real - subtractor.Real,
                Imaginary = Imaginary - subtractor.Imaginary
            };
        }

        internal Complex Divide(Complex divided)
        {
            Complex dividend = Multiply(new Complex() { Real = divided.Real, Imaginary = -divided.Imaginary });
            double divisor = divided.Real * divided.Real + divided.Imaginary * divided.Imaginary;

            return new Complex()
            {
                Real = dividend.Real / divisor,
                Imaginary = dividend.Imaginary / divisor
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginary}i)";
        }

        public override bool Equals(object obj)
        {
            if (obj is Complex complex)
            {
               
                return Math.Pow(Real - complex.Real, 2) + Math.Pow(Imaginary - complex.Imaginary, Power) <= RootLimit ;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            int hashCode = -568953957;
            hashCode = hashCode * -1521134295 + Real.GetHashCode();
            hashCode = hashCode * -1521134295 + Imaginary.GetHashCode();
            return hashCode;
        }
    }
}
