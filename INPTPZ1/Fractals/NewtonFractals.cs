using INPTPZ1.Arguments;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace INPTPZ1.Fractals
{
    public class NewtonFractals
    {
        private static readonly int IterationLimit = 30;
        private static readonly double NewtonsFractalLimit = 0.5;
        private static readonly int RGBColorValue = 255;
        private static readonly Color[] FractalsColors =
        {
            Color.Red, Color.Blue, Color.Green,
            Color.Yellow, Color.Orange, Color.Fuchsia,
            Color.Gold, Color.Cyan, Color.Magenta
        };
        private static Polynomial CreateStartFunction()
        {
            Polynomial polynom = new Polynomial();
            polynom.ComplexNumbers.AddRange(new List<Complex> { new Complex() { Real = 1 },
                Complex.Zero,
                Complex.Zero,
                new Complex() { Real = 1 } });
            return polynom;
        }
        private bool IsInNewtonsLimit(Complex difference)
        {
            return (Math.Pow(difference.Real, Complex.Power) + Math.Pow(difference.Imaginary,Complex.Power) >= NewtonsFractalLimit);
        }
        private Complex CalculateSolutionOfEquation(Complex coordinates, Polynomial derivedPolynom)
        {
            int iteractionNumber = 0;
            for (int i = 0; i < IterationLimit; i++)
            {
                Complex difference = CreateStartFunction().Evaluate(coordinates).Divide(derivedPolynom.Evaluate(coordinates));
                coordinates = coordinates.Subtract(difference);

                if (IsInNewtonsLimit(difference))
                    i--;
                
                iteractionNumber++;
            }
            return coordinates;
        }
        private int FindRoot(List<Complex> roots, Complex coordinates)
        {
            for (int i = 0; i < roots.Count; i++)
                if (coordinates.Equals(roots[i]))
                    return i;
            roots.Add(coordinates);
            return roots.Count;
        }
        private Color ColorizePixel(int id)
        {
            Color resultColor = FractalsColors[id % FractalsColors.Length];
            resultColor = Color.FromArgb(Math.Min(Math.Max(0, resultColor.R - IterationLimit * Complex.Power), RGBColorValue),
                Math.Min(Math.Max(0, resultColor.G - IterationLimit * Complex.Power), RGBColorValue),
                Math.Min(Math.Max(0, resultColor.B - IterationLimit * Complex.Power), RGBColorValue));
            return resultColor;
        }
        public Bitmap ColorizePicture(ImageArguments arguments)
        {
           Bitmap image = new Bitmap(arguments.Width, arguments.Height);

            List<Complex> roots = new List<Complex>();
            Polynomial derivedPolynom = CreateStartFunction().Derive();

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

                    coordinates = CalculateSolutionOfEquation(coordinates, derivedPolynom);

                    int id = FindRoot( roots, coordinates);
                    Color color = ColorizePixel(id);
                    image.SetPixel(y, x, color);

                }
            }
            return image;
        }
    }
}
