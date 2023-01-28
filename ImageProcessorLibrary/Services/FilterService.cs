using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class FilterService : OpenCvService
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


    public Mat MedianBlur(Mat inputArray, int medianBoxSize)
    {
        var height = inputArray.Rows;
        var width = inputArray.Cols;
        var outputArray = new Mat(height, width, MatType.CV_8UC3);
        Cv2.MedianBlur(inputArray, outputArray, medianBoxSize);
        return outputArray;
    }
}