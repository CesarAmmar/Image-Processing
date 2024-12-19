using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Utilities.FiltersAlgorithms
{
    public static class Blurring
    {
        public static float[,] GetFilterKernel()
        {
            return new float[,]
                {
                { 1 / 16f, 2 / 16f, 1 / 16f },
                { 2 / 16f, 4 / 16f, 2 / 16f },
                { 1 / 16f, 2 / 16f, 1 / 16f }
                };
        }
    }
}
