using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Utilities.FiltersAlgorithms
{
    public static class EdgeDetecting
    {
        public static float[,] GetFilterKernel()
        {
            return new float[,] {
                {-1, -1, -1 },
                {-1,  8, -1 },
                {-1, -1, -1 }
            };
        }
    }
}
