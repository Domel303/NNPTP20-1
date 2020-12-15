using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace INPTPZ1.Arguments
{
    public class CommandLineArguments
    {
        private static readonly int IndexOfWidth = 0;
        private static readonly int IdefOfHeight = 1;
        private static readonly int IndexOfXMin = 2;
        private static readonly int IndexOfYMin = 3;
        private static readonly int IndexOfXMax = 4;
        private static readonly int IndexOfYMax = 5;
        private static readonly int IndexOfFilePath = 6;

        public int Width { get; set; }
        public int Height { get; set; }
        public double Xmin { get; set; }
        public double Ymin { get; set; }
        public double Xmax { get; set; }
        public double Ymax { get; set; }
        public string FilePath { get; set; }
        public double Xstep { get; set; }
        public double Ystep { get; set; }

        public void Parse(string[] args)
        {
            Width = Int32.Parse(args[IndexOfWidth]);
            Height = Int32.Parse(args[IdefOfHeight]);
            Xmin = Double.Parse(args[IndexOfXMin]);
            Ymin = Double.Parse(args[IndexOfYMin]);
            Xmax = Double.Parse(args[IndexOfXMax]);
            Ymax = Double.Parse(args[IndexOfYMax]);
            FilePath = args[IndexOfFilePath];

            CalculateSteps();
        }
        private void CalculateSteps()
        { 
            Xstep = (Xmax - Xmin) / Width;
            Ystep = (Ymax - Ymin) / Height;
        }
    }


}
