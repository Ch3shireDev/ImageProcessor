using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class EdgeDetectionService:OpenCvService
{
    public IImageData SobelEdgeDetection(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        //Cv2.ImWrite("a-1.png", mat);
        var result =  SobelEdgeDetection(mat);
        //Cv2.ImWrite("b-1.png", result);
        return ToImageData3f(result);
    }
    
    public IImageData ToImageData3f(Mat outputMat)
    {
        var width = outputMat.Cols;
        var height = outputMat.Rows;

        var result = new ImageData(width, height);

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = outputMat.Get<Vec3s>(y, x);

                if (pixel.Item1 != 0)
                {
                }

                result.SetPixel(x, y, Color.FromArgb(
                    (byte)(128+pixel.Item0 ),
                    (byte)(128+pixel.Item1 ),
                    (byte)(128+pixel.Item2 )
                ));
            }
        }

        return result;
    }


    public Mat SobelEdgeDetection(Mat inputMat)
    {
        var outputMat = new Mat(inputMat.Rows, inputMat.Cols, MatType.CV_8UC3);

        Cv2.Sobel(inputMat, outputMat, MatType.CV_16S, 1, 1);
        return outputMat;
    }

    public IImageData PrewittEdgeDetection(IImageData imageData)
    {

        return imageData;
    }

    public IImageData CannyOperatorEdgeDetection(IImageData imageData)
    {
        return imageData;
    }
}