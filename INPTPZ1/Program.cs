using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.Arguments;

namespace INPTPZ1
{
    /// <summary>
    /// This program should produce Newton fractals.
    /// See more at: https://en.wikipedia.org/wiki/Newton_fractal
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {

           CommandLineArguments arguments = new CommandLineArguments();
            arguments.Parse(args);

            Bitmap image = new Bitmap(arguments.Width, arguments.Height);
           

            List<Complex> koreny = new List<Complex>();
            // TODO: poly should be parameterised?
            Polynomial p = new Polynomial();
            p.ComplexNumbers.Add(new Complex() { Real = 1 });
            p.ComplexNumbers.Add(Complex.Zero);
            p.ComplexNumbers.Add(Complex.Zero);
            p.ComplexNumbers.Add(new Complex() { Real = 1 });
            Polynomial pd = p.Derive();

            Console.WriteLine(p);
            Console.WriteLine(pd);

            var clrs = new Color[]
            {
                Color.Red, Color.Blue, Color.Green, Color.Yellow, Color.Orange, Color.Fuchsia, Color.Gold, Color.Cyan, Color.Magenta
            };

            var maxid = 0;

            // TODO: cleanup!!!
            // for every pixel in image...
            for (int i = 0; i < arguments.Width; i++)
            {
                for (int j = 0; j < arguments.Height; j++)
                {
                    // find "world" coordinates of pixel
                    double y = arguments.Ymin + i * arguments.Ystep;
                    double x = arguments.Xmin + j * arguments.Xstep;

                    Complex ox = new Complex()
                    {
                        Real = x,
                        Imaginary = (float)(y)
                    };

                    if (ox.Real == 0)
                        ox.Real = 0.0001;
                    if (ox.Imaginary == 0)
                        ox.Imaginary = 0.0001f;

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q< 30; q++)
                    {
                        var diff = p.Evaluate(ox).Divide(pd.Evaluate(ox));
                        ox = ox.Subtract(diff);

                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginary, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }
                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <koreny.Count;w++)
                    {
                        if (Math.Pow(ox.Real- koreny[w].Real, 2) + Math.Pow(ox.Imaginary - koreny[w].Imaginary, 2) <= 0.01)
                        {
                            known = true;
                            id = w;
                        }
                    }
                    if (!known)
                    {
                        koreny.Add(ox);
                        id = koreny.Count;
                        maxid = id + 1; 
                    }
                    // colorize pixel according to root number
                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
                 
                    image.SetPixel(j, i, vv);
                  
                }
            }
                    image.Save(arguments.FilePath ?? "../../../out.png");
        }
    }


}
