using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class OpenCvService
{
    public IImageData Filter(IImageData imageData, double[,] kernel, BorderTypes borderType = BorderTypes.Default, int numberOfBorderPixels = 0, int valueN=0, bool fillBeforeTransform = true,
        bool fillAfterTransform = false)
    {
        var inputArray = ToInputArray(imageData);

        var width = imageData.Width;
        var height = imageData.Height;

        Mat inputArray2;

        var scalar = new Scalar(valueN, valueN, valueN);

        if (fillBeforeTransform)
        {
            width += numberOfBorderPixels * 2;
            height += numberOfBorderPixels * 2;

            inputArray2 = new Mat(height, width, MatType.CV_8UC3);
            ;

            Cv2.CopyMakeBorder(
                inputArray,
                inputArray2,
                numberOfBorderPixels,
                numberOfBorderPixels,
                numberOfBorderPixels,
                numberOfBorderPixels,
                borderType,
                scalar
            );
        }
        else
        {
            inputArray2 = inputArray;
        }

        var kernelArray = new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
        var outputArray = new Mat(height, width, MatType.CV_8UC3);
        Cv2.Filter2D(inputArray2, outputArray, MatType.CV_8UC3, kernelArray, borderType: borderType);

        Mat outputArray2;

        if (fillAfterTransform)
        {
            width += numberOfBorderPixels * 2;
            height += numberOfBorderPixels * 2;
            outputArray2 = new Mat(height, width, MatType.CV_8UC3);
            
            Cv2.CopyMakeBorder(
                outputArray,
                outputArray2,
                numberOfBorderPixels,
                numberOfBorderPixels,
                numberOfBorderPixels,
                numberOfBorderPixels,
                borderType,
                scalar
            );
        }
        else
        {
            outputArray2 = outputArray;
        }

        return ToImageData(outputArray2, width, height);
    }

    public double[,] Normalize(double[,] kernel)
    {
        var kernelArray = new Mat(kernel.GetLength(0), kernel.GetLength(1), MatType.CV_64F, kernel);
        Cv2.Normalize(kernelArray, kernelArray, 1.0, 0, NormTypes.L1);
        kernelArray.GetRectangularArray<double>(out var result);
        return result;
    }

    public IImageData MedianBlur(IImageData imageData, int medianBoxSize)
    {
        var inputArray = ToInputArray(imageData);

        var width = imageData.Width;
        var height = imageData.Height;
        var outputArray = new Mat(height, width, MatType.CV_8UC3);
        Cv2.MedianBlur(inputArray, outputArray, medianBoxSize);
        return ToImageData(outputArray, width, height);
    }

    private static Mat ToInputArray(IImageData imageData)
    {
        var mat = new Mat(imageData.Width, imageData.Height, MatType.CV_8UC3);

        for (var x = 0; x < imageData.Width; x++)
        {
            for (var y = 0; y < imageData.Height; y++)
            {
                var pixel = imageData.GetPixelRgb(x, y);
                mat.Set(x, y, new Vec3b(pixel.R, pixel.G, pixel.B));
            }
        }

        return mat;
    }

    private static ImageData ToImageData(OutputArray outputArray, int width, int height)
    {
        var outputMat = outputArray.GetMat();

        var result = new ImageData(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = outputMat.Get<Vec3b>(x, y);

                if (pixel.Item1 != 0)
                {
                }

                result.SetPixel(x, y, Color.FromArgb(pixel.Item0, pixel.Item1, pixel.Item2));
            }
        }

        return result;
    }
}