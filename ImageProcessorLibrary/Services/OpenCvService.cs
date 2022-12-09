using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class OpenCvService
{
    public Mat Filter(Mat inputArray, Mat kernelArray, BorderTypes borderType)
    {
        var outputArray = new Mat(inputArray.Rows, inputArray.Cols, MatType.CV_8UC3);
        Cv2.Filter2D(inputArray, outputArray, MatType.CV_8UC3, kernelArray, borderType: borderType);
        return outputArray;
    }

    public Mat AddBorder(Mat inputArray, BorderTypes borderType, int numberOfBorderPixels, Scalar scalar)
    {
        if (numberOfBorderPixels == 0) return inputArray;

        var rows = inputArray.Rows + numberOfBorderPixels * 2;
        var cols = inputArray.Cols + numberOfBorderPixels * 2;

        var outputArray = new Mat(rows, cols, MatType.CV_8UC3);

        Cv2.CopyMakeBorder(
            inputArray,
            outputArray,
            numberOfBorderPixels,
            numberOfBorderPixels,
            numberOfBorderPixels,
            numberOfBorderPixels,
            borderType,
            scalar
        );

        return outputArray;
    }

    public Mat GetKernel(double[,] kernel)
    {
        return new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
    }

    public double[,] Normalize(double[,] kernel)
    {
        var kernelArray = new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
        Cv2.Normalize(kernelArray, kernelArray, 1.0, 0, NormTypes.L1);
        kernelArray.GetRectangularArray<double>(out var result);
        return result;
    }

    public Mat MedianBlur(Mat inputArray, int medianBoxSize)
    { 
        var height = inputArray.Rows;
        var width = inputArray.Cols;
        var outputArray = new Mat(height, width, MatType.CV_8UC3);
        Cv2.MedianBlur(inputArray, outputArray, medianBoxSize); 
        return outputArray;
    }

    public Mat ToMatrix(IImageData imageData)
    {
        var mat = new Mat(imageData.Height, imageData.Width, MatType.CV_8UC3);

        for (var x = 0; x < imageData.Width; x++)
        {
            for (var y = 0; y < imageData.Height; y++)
            {
                var pixel = imageData.GetPixelRgb(x, y);
                mat.Set(y,x, new Vec3b(pixel.R, pixel.G, pixel.B));
            }
        }

        return mat;
    }

    public IImageData ToImageData(Mat outputMat)
    {
        var width = outputMat.Cols;
        var height = outputMat.Rows;

        var result = new ImageData(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = outputMat.Get<Vec3b>(y,x);
                
                result.SetPixel(x, y, Color.FromArgb(pixel.Item0, pixel.Item1, pixel.Item2));
            }
        }

        return result;
    }
}