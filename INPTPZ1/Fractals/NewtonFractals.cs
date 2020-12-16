using INPTPZ1.Arguments;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1.Fractals
{
   public class NewtonFractals
    {
        private static readonly int IterationLimit = 30;
        private static readonly double NewtonsFractalLimit = 0.5;
        private static readonly int RGBColorValue = 255;
        private static readonly Color[] colors =
        {
            Color.Red, Color.Blue, Color.Green,
            Color.Yellow, Color.Orange, Color.Fuchsia,
            Color.Gold, Color.Cyan, Color.Magenta
        };
        private static Polynomial StartFunction()
        {
            Polynomial polynom = new Polynomial();
            polynom.ComplexNumbers.AddRange(new List<Complex> { new Complex() { Real = 1 },
                Complex.Zero,
                Complex.Zero,
                new Complex() { Real = 1 } });
            return polynom;
        }
        private bool InNewtonsLimit(Complex difference)
        {
            return (Math.Pow(difference.Real, Complex.Power) + Math.Pow(difference.Imaginary,Complex.Power) >= NewtonsFractalLimit);
        }
        private Complex SolutionOfEquation(Complex coordinates, Polynomial derivedPolynom)
        {
            int iteractionNumber = 0;
            for (int i = 0; i < IterationLimit; i++)
            {
                Complex difference = StartFunction().Evaluate(coordinates).Divide(derivedPolynom.Evaluate(coordinates));
                coordinates = coordinates.Subtract(difference);

                if (InNewtonsLimit(difference))
                    i--;
                
                iteractionNumber++;
            }
            return coordinates;
        }
        private int findRoot(List<Complex> roots, Complex coordinates)
        {
            for (int i = 0; i < roots.Count; i++)
                if (coordinates.Equals(roots[i]))
                    return i;
            roots.Add(coordinates);
            return roots.Count;
        }
        private Color ColorizePixel(int id)
        {
            Color resultColor = colors[id % colors.Length];
            resultColor = Color.FromArgb(Math.Min(Math.Max(0, resultColor.R - IterationLimit * Complex.Power), RGBColorValue),
                Math.Min(Math.Max(0, resultColor.G - IterationLimit * Complex.Power), RGBColorValue),
                Math.Min(Math.Max(0, resultColor.B - IterationLimit * Complex.Power), RGBColorValue));
            return resultColor;
        }
        public Bitmap ColorizePicture(ImageArguments arguments)
        {
           Bitmap image = new Bitmap(arguments.Width, arguments.Height);

            List<Complex> roots = new List<Complex>();
            Polynomial derivedPolynom = StartFunction().Derive();

            for (int x = 0; x < arguments.Width; x++)
            {
                for (int y = 0; y < arguments.Height; y++)
                {
                    double yPosition = arguments.Ymin + x * arguments.Ystep;
                    double xPosition = arguments.Xmin + y * arguments.Xstep;

                    Complex coordinates = new Complex()
                    {
                        Real = xPosition,
                        Imaginary = yPosition
                    };

                    coordinates = SolutionOfEquation(coordinates, derivedPolynom);

                    int id = findRoot( roots, coordinates);
                    Color color = ColorizePixel(id);
                    image.SetPixel(y, x, color);

                }
            }
            return image;
        }
    }
}
