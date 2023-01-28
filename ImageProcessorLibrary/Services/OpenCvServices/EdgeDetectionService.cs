using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.Enums;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services.OpenCvServices;

/// <summary>
///     Serwis wykrywania krawędzi.
/// </summary>
public class EdgeDetectionService : OpenCvService
{
    /// <summary>
    ///     Wykrywanie krawędzi metodą Sobela.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="edgeType"></param>
    /// <returns></returns>
    public ImageData SobelEdgeDetection(ImageData imageData, SobelEdgeType edgeType = SobelEdgeType.EAST)
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

    /// <summary>
    ///     Wykrywanie krawędzi metodą Sobela.
    /// </summary>
    /// <param name="matrix"></param>
    /// <param name="edgeType"></param>
    /// <returns></returns>
    public Mat SobelEdgeDetection(Mat matrix, SobelEdgeType edgeType = SobelEdgeType.EAST)
    {
        var x = SobelGetX(edgeType);
        var y = SobelGetY(edgeType);

        var mat2 = new Mat(matrix.Rows, matrix.Cols, MatType.CV_8UC3);
        Cv2.Sobel(matrix, mat2, MatType.CV_16S, x, y);
        return mat2;
    }

    /// <summary>
    ///     Wykrywanie krawędzi metodą Prewitta.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="prewittType"></param>
    /// <returns></returns>
    public ImageData PrewittEdgeDetection(ImageData imageData, PrewittType prewittType = PrewittType.PREWITT_XY)
    {
        var mat = ToMatrix(imageData);
        Mat kernel;

        if (prewittType == PrewittType.PREWITT_Y)
        {
            var kernelNums = new[,]
            {
                { 1, 1, 1 },
                { 0, 0, 0 },
                { -1, -1, -1 }
            };

            kernel = new Mat(3, 3, MatType.CV_32S, kernelNums);
        }
        else if (prewittType == PrewittType.PREWITT_X)
        {
            var kernelNums = new[,]
            {
                { 1, 0, -1 },
                { 1, 0, -1 },
                { 1, 0, -1 }
            };

            kernel = new Mat(3, 3, MatType.CV_32S, kernelNums);
        }
        else
        {
            var image1 = PrewittEdgeDetection(imageData, PrewittType.PREWITT_X);
            var image2 = PrewittEdgeDetection(imageData, PrewittType.PREWITT_Y);
            return ImageData.Combine(image1, image2);
        }

        Cv2.Filter2D(mat, mat, MatType.CV_8UC3, kernel);

        var mat2 = new Mat(mat.Rows, mat.Cols, MatType.CV_8UC3);
        Cv2.ConvertScaleAbs(mat, mat2);

        return ToImageDataFromUC3(mat);
    }

    /// <summary>
    ///     Pobiera wartość X dla wybranego typu krawędzi.
    /// </summary>
    /// <param name="edgeType"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Pobiera wartość Y dla wybranego typu krawędzi.
    /// </summary>
    /// <param name="edgeType"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Wykrywanie krawędzi metodą Canny'ego.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="threshold1"></param>
    /// <param name="threshold2"></param>
    /// <returns></returns>
    public ImageData CannyOperatorEdgeDetection(ImageData imageData, int threshold1 = 100, int threshold2 = 200)
    {
        var mat = ToMatrix(imageData);
        var result = CannyOperatorEdgeDetection(mat, threshold1, threshold2);
        var mat2 = new Mat(result.Rows, result.Cols, MatType.CV_16SC3);
        Cv2.ConvertScaleAbs(result, mat2);
        return ToImageDataFromUC1(mat2);
    }

    /// <summary>
    ///     Wykrywanie krawędzi metodą Canny'ego.
    /// </summary>
    /// <param name="mat"></param>
    /// <param name="threshold1"></param>
    /// <param name="threshold2"></param>
    /// <returns></returns>
    public Mat CannyOperatorEdgeDetection(Mat mat, double threshold1 = 100, double threshold2 = 200)
    {
        var edges = new Mat(mat.Rows, mat.Cols, MatType.CV_16SC1);
        Cv2.Canny(mat, edges, threshold1, threshold2);
        return edges;
    }
}