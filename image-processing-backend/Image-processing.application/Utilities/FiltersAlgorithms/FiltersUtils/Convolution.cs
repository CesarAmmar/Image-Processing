using ILGPU.Runtime;
using ILGPU;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image_processing.application.Utilities.FiltersAlgorithms.FiltersUtils
{
    public static class Convolution
    {
        public static void ConvolutionKernel(
           Index2D index,
           ArrayView1D<byte, Stride1D.Dense> input,
           ArrayView1D<byte, Stride1D.Dense> output,
           ArrayView2D<float, Stride2D.DenseY> kernel,
           int kernelSize,
           int width,
           int height)
        {
            int halfKernel = kernelSize / 2;

            for (int channel = 0; channel < 3; channel++)
            {
                float sum = 0;
                for (int kx = -halfKernel; kx <= halfKernel; ++kx)
                {
                    for (int ky = -halfKernel; ky <= halfKernel; ++ky)
                    {
                        int x = Clamp(index.X + kx, 0, width - 1);
                        int y = Clamp(index.Y + ky, 0, height - 1);
                        int inputIndex = channel * width * height + y * width + x;

                        sum += input[inputIndex] * kernel[kx + halfKernel, ky + halfKernel];
                    }
                }

                int outputIndex = channel * width * height + index.Y * width + index.X;
                output[outputIndex] = (byte)Clamp(sum, 0, 255);
            }
        }
        public static byte[] Flatten3DArray(byte[,,] array, int depth, int width, int height)
        {
            byte[] flatArray = new byte[depth * width * height];
            for (int c = 0; c < depth; c++)
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                        flatArray[c * width * height + y * width + x] = array[c, x, y];
            return flatArray;
        }

        public static byte[,,] Reconstruct3DArray(byte[] flatArray, int depth, int width, int height)
        {
            byte[,,] array = new byte[depth, width, height];
            for (int c = 0; c < depth; c++)
                for (int x = 0; x < width; x++)
                    for (int y = 0; y < height; y++)
                        array[c, x, y] = flatArray[c * width * height + y * width + x];
            return array;
        }

        private static int Clamp(int value, int min, int max) => Math.Max(min, Math.Min(max, value));
        private static float Clamp(float value, float min, float max) => Math.Max(min, Math.Min(max, value));
    }
}
