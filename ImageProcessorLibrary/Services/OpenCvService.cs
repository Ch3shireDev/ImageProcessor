using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class OpenCvService
{
    public double[,] Normalize(double[,] kernel)
    {
        var kernelArray = new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
        Cv2.Normalize(kernelArray, kernelArray, 1.0, 0, NormTypes.L1);
        kernelArray.GetRectangularArray<double>(out var result);
        return result;
    }

    public Mat ToMatrix(ImageData imageData)
    {
        var mat = new Mat(imageData.Height, imageData.Width, MatType.CV_8UC3);

        for (var x = 0; x < imageData.Width; x++)
        {
            for (var y = 0; y < imageData.Height; y++)
            {
                var pixel = imageData.GetPixelRgb(x, y);
                mat.Set(y, x, new Vec3b(pixel.R, pixel.G, pixel.B));
            }
        }

        return mat;
    }

    public Mat ToGrayMatrix(ImageData imageData)
    {
        var mat = new Mat(imageData.Height, imageData.Width, MatType.CV_8UC1);

        for (var x = 0; x < imageData.Width; x++)
        {
            for (var y = 0; y < imageData.Height; y++)
            {
                var value = imageData.GetGrayValue(x, y);
                if (value > 255) value = 255;
                if (value < 0) value = 0;
                mat.Set(y, x, value);
            }
        }

        return mat;
    }

    public Mat GetKernel(double[,] kernel)
    {
        return new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
    }

    public ImageData ToImageDataFromUC3(Mat outputMat)
    {
        var width = outputMat.Cols;
        var height = outputMat.Rows;

        var result = new ImageData(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = outputMat.Get<Vec3b>(y, x);

                result.SetPixel(x, y, Color.FromArgb(pixel.Item0, pixel.Item1, pixel.Item2));
            }
        }

        return result;
    }

    public ImageData ToImageDataFromUC1(Mat outputMat)
    {
        var width = outputMat.Cols;
        var height = outputMat.Rows;

        var result = new ImageData(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var value = outputMat.Get<byte>(y, x);
                result.SetPixel(x, y, Color.FromArgb(value, value, value));
            }
        }

        return result;
    }
}