using System.Collections.Generic;
using System.Text;

namespace INPTPZ1
{
    class Polynomial
    {
        public List<Cplx> ComplexNumbers { get; } = new List<Cplx>();
        public Polynomial Derive()
        {
            Polynomial derivated = new Polynomial();

            for (int i = 1; i < ComplexNumbers.Count; i++)
            {
                Cplx multiplier = new Cplx() { Real = i }; 
                derivated.ComplexNumbers.Add(ComplexNumbers[i].Multiply(multiplier));
            }

            return derivated;
        }

        public Cplx Evaluate(Cplx evaluation)
        {
            Cplx evaluated = Cplx.Zero;
            for (int i = 0; i < ComplexNumbers.Count; i++)
                evaluated = evaluated.Add(MultiplicateCoefficient(i,evaluation));
            
            return evaluated;
        }

        private Cplx MultiplicateCoefficient(int power, Cplx evaluation)
        {
            Cplx coefficient = ComplexNumbers[power];
            Cplx evaluationTotal = evaluation;
           
            if (power > 0)
            {
                for (int i = 0; i < power - 1; i++)
                    evaluationTotal = evaluationTotal.Multiply(evaluation);

                return coefficient.Multiply(evaluationTotal);
            }

            return coefficient;
        }

        public override string ToString()
        {

            StringBuilder stringBuilder = new StringBuilder();

            for (int power = ComplexNumbers.Count-1; power >= 0; power--)
            {
                stringBuilder.Append(ComplexNumbers[power]);
                if (power > 0)
                    stringBuilder.Append($" X^{power} +");
            }

            return stringBuilder.ToString();
        }
    }
}
