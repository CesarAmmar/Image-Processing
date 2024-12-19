using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Utilities.FiltersAlgorithms.FiltersUtils
{
    public static class Helpers
    {
        public static (int Width, int Height) GetImageDimensions(byte[] imageBytes)
        {
            using var ms = new MemoryStream(imageBytes);
            using var image = Image.FromStream(ms);
            return (image.Width, image.Height);
        }
    }
}