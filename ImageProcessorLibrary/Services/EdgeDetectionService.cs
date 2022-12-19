using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public enum PrewittType
{
    PREWITT_X,
    PREWITT_Y,
    PREWITT_XY
}
public class EdgeDetectionService : OpenCvService
{
    public IImageData SobelEdgeDetection(IImageData imageData, SobelEdgeType edgeType = SobelEdgeType.EAST)
    {
        if (edgeType == SobelEdgeType.ALL)
        {
            var i1 = SobelEdgeDetection(imageData);
            var i2 = SobelEdgeDetection(imageData, SobelEdgeType.SOUTH);
            var i3 = SobelEdgeDetection(imageData, SobelEdgeType.SOUTH_EAST);

            return ImageData.Combine(i1, i2, i3);
        }

        var mat = ToMatrix(imageData);
        var result = SobelEdgeDetection(mat, edgeType);
        var mat2 = new Mat(result.Rows, result.Cols, MatType.CV_16SC3);
        Cv2.ConvertScaleAbs(result, mat2);
        return ToImageDataFromUC3(mat2);
    }
    
    public Mat SobelEdgeDetection(Mat matrix, SobelEdgeType edgeType = SobelEdgeType.EAST)
    {
        var x = SobelGetX(edgeType);
        var y = SobelGetY(edgeType);

        var mat2 = new Mat(matrix.Rows, matrix.Cols, MatType.CV_8UC3);
        Cv2.Sobel(matrix, mat2, MatType.CV_16S, x, y);
        return mat2;
    }
    
    public IImageData PrewittEdgeDetection(IImageData imageData, PrewittType prewittType = PrewittType.PREWITT_XY)
    {
        var mat = ToMatrix(imageData);


        var kernelNums = new[,]
        {
            { 1, 1, 1 }, 
            { 0, 0, 0 }, 
            { -1, -1, -1 }
        };
        var kernel = new Mat(3, 3, MatType.CV_32S, kernelNums);

        Cv2.Filter2D(mat, mat,MatType.CV_8UC3, kernel);

        
        var mat2 = new Mat(mat.Rows, mat.Cols, MatType.CV_8UC3);
        Cv2.ConvertScaleAbs(mat, mat2);

        return ToImageDataFromUC3(mat);
    }

    private int SobelGetX(SobelEdgeType edgeType)
    {
        return edgeType switch
        {
            SobelEdgeType.EAST => 1,
            SobelEdgeType.NORTH_EAST => 1,
            SobelEdgeType.NORTH => 0,
            SobelEdgeType.NORTH_WEST => -1,
            SobelEdgeType.WEST => -1,
            SobelEdgeType.SOUTH_WEST => -1,
            SobelEdgeType.SOUTH => 0,
            SobelEdgeType.SOUTH_EAST => 1,
            _ => 0
        };
    }

    private int SobelGetY(SobelEdgeType edgeType)
    {
        return edgeType switch
        {
            SobelEdgeType.EAST => 0,
            SobelEdgeType.NORTH_EAST => -1,
            SobelEdgeType.NORTH => -1,
            SobelEdgeType.NORTH_WEST => -1,
            SobelEdgeType.WEST => 0,
            SobelEdgeType.SOUTH_WEST => 1,
            SobelEdgeType.SOUTH => 1,
            SobelEdgeType.SOUTH_EAST => 1,
            _ => 0
        };
    }

    public IImageData CannyOperatorEdgeDetection(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        var result = CannyOperatorEdgeDetection(mat);
        var mat2 = new Mat(result.Rows, result.Cols, MatType.CV_16SC3);
        Cv2.ConvertScaleAbs(result, mat2);
        return ToImageDataFromUC1(mat2);
    }

    public Mat CannyOperatorEdgeDetection(Mat mat, double threshold1 = 100, double threshold2 = 200)
    {
        var edges = new Mat(mat.Rows, mat.Cols, MatType.CV_16SC1);
        Cv2.Canny(mat, edges, threshold1, threshold2);
        return edges;
    }
}