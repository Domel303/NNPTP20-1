using System;
using System.Collections.Generic;
using System.Drawing;
using INPTPZ1.Arguments;
using INPTPZ1.Fractals;

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
            ImageArguments arguments = new ImageArguments();
            arguments.Parse(args);
            NewtonFractals fractals = new NewtonFractals();
            Bitmap image = fractals.ColorizePicture(arguments);
            image.Save(arguments.FilePath ?? "../../../out.png");


        }
    }


}
