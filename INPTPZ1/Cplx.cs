using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1
{
    public class Cplx
    {
        public double Real { get; set; }
        public float Imaginari { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Cplx)
            {
                Cplx x = obj as Cplx;
                return x.Real == Real && x.Imaginari == Imaginari;
            }
            return base.Equals(obj);
        }

        public readonly static Cplx Zero = new Cplx()
        {
            Real = 0,
            Imaginari = 0
        };

        public Cplx Multiply(Cplx b)
        {
            Cplx a = this;
            // aRe*bRe + aRe*bIm*i + aIm*bRe*i + aIm*bIm*i*i
            return new Cplx()
            {
                Real = a.Real * b.Real - a.Imaginari * b.Imaginari,
                Imaginari = (float)(a.Real * b.Imaginari + a.Imaginari * b.Real)
            };
        }
        public double GetAbS()
        {
            return Math.Sqrt(Real * Real + Imaginari * Imaginari);
        }

        public Cplx Add(Cplx b)
        {
            Cplx a = this;
            return new Cplx()
            {
                Real = a.Real + b.Real,
                Imaginari = a.Imaginari + b.Imaginari
            };
        }
        public double GetAngleInDegrees()
        {
            return Math.Atan(Imaginari / Real);
        }
        public Cplx Subtract(Cplx b)
        {
            Cplx a = this;
            return new Cplx()
            {
                Real = a.Real - b.Real,
                Imaginari = a.Imaginari - b.Imaginari
            };
        }

        public override string ToString()
        {
            return $"({Real} + {Imaginari}i)";
        }

        internal Cplx Divide(Cplx b)
        {
            var tmp = this.Multiply(new Cplx() { Real = b.Real, Imaginari = -b.Imaginari });
            var tmp2 = b.Real * b.Real + b.Imaginari * b.Imaginari;

            return new Cplx()
            {
                Real = tmp.Real / tmp2,
                Imaginari = (float)(tmp.Imaginari / tmp2)
            };
        }
    }
}
