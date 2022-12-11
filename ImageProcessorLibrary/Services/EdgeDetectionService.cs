using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class EdgeDetectionService : OpenCvService
{
    public IImageData SobelEdgeDetection(IImageData imageData, SobelEdgeType edgeType = SobelEdgeType.EAST)
    {
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
            _ => 0,
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
            _ => 0,
        };
    }

    public IImageData PrewittEdgeDetection(IImageData imageData)
    {
        return imageData;
    }

    public IImageData CannyOperatorEdgeDetection(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        var result = CannyOperatorEdgeDetection(mat);
        return ToImageDataFromUC1(result);
    }

    public Mat CannyOperatorEdgeDetection(Mat mat, double threshold1=100, double threshold2=200)
    {
        var edges = new Mat(mat.Rows, mat.Cols, MatType.CV_16SC1);
        Cv2.Canny(mat, edges, threshold1, threshold2);
        return edges;
    }
}