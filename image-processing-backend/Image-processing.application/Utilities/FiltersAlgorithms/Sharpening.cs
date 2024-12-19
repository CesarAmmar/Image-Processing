using System;
using System.Collections.Generic;
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Image_processing.application.Utilities.FiltersAlgorithms
{
    public static class Sharpening
    {
        public static float[,] GetFilterKernel()
        {
            return new float[,] {
                { 0, -1,  0 },
                {-1,  5, -1 },
                { 0, -1,  0 }
            };
        }
    }
}
