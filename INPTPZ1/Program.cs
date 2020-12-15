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
           

            List<Cplx> koreny = new List<Cplx>();
            // TODO: poly should be parameterised?
            Poly p = new Poly();
            p.Coe.Add(new Cplx() { Real = 1 });
            p.Coe.Add(Cplx.Zero);
            p.Coe.Add(Cplx.Zero);
            //p.Coe.Add(Cplx.Zero);
            p.Coe.Add(new Cplx() { Real = 1 });
            Poly pd = p.Derive();

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

                    Cplx ox = new Cplx()
                    {
                        Real = x,
                        Imaginari = (float)(y)
                    };

                    if (ox.Real == 0)
                        ox.Real = 0.0001;
                    if (ox.Imaginari == 0)
                        ox.Imaginari = 0.0001f;

                    //Console.WriteLine(ox);

                    // find solution of equation using newton's iteration
                    float it = 0;
                    for (int q = 0; q< 30; q++)
                    {
                        var diff = p.Eval(ox).Divide(pd.Eval(ox));
                        ox = ox.Subtract(diff);

                        //Console.WriteLine($"{q} {ox} -({diff})");
                        if (Math.Pow(diff.Real, 2) + Math.Pow(diff.Imaginari, 2) >= 0.5)
                        {
                            q--;
                        }
                        it++;
                    }

                    //Console.ReadKey();

                    // find solution root number
                    var known = false;
                    var id = 0;
                    for (int w = 0; w <koreny.Count;w++)
                    {
                        if (Math.Pow(ox.Real- koreny[w].Real, 2) + Math.Pow(ox.Imaginari - koreny[w].Imaginari, 2) <= 0.01)
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
                    //int vv = id;
                    //int vv = id * 50 + (int)it*5;
                    var vv = clrs[id % clrs.Length];
                    vv = Color.FromArgb(vv.R, vv.G, vv.B);
                    vv = Color.FromArgb(Math.Min(Math.Max(0, vv.R-(int)it*2), 255), Math.Min(Math.Max(0, vv.G - (int)it*2), 255), Math.Min(Math.Max(0, vv.B - (int)it*2), 255));
                    //vv = Math.Min(Math.Max(0, vv), 255);
                    image.SetPixel(j, i, vv);
                    //bmp.SetPixel(j, i, Color.FromArgb(vv, vv, vv));
                }
            }

       

                    image.Save(arguments.FilePath ?? "../../../out.png");
            //Console.ReadKey();
        }
    }


}
