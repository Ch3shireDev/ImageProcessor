using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class EdgeDetectionService : OpenCvService
{
    public IImageData SobelEdgeDetection(IImageData imageData, SobelEdgeType edgeType = SobelEdgeType.EAST)
    {
        if (edgeType == SobelEdgeType.ALL)
        {
            var i1 = SobelEdgeDetection(imageData);
            var i2 = SobelEdgeDetection(imageData, SobelEdgeType.SOUTH);
            var i3 = SobelEdgeDetection(imageData, SobelEdgeType.SOUTH_EAST);

            return Combine(i1, i2, i3);
        }

        var mat = ToMatrix(imageData);
        var result = SobelEdgeDetection(mat, edgeType);
        var mat2 = new Mat(result.Rows, result.Cols, MatType.CV_16SC3);
        Cv2.ConvertScaleAbs(result, mat2);
        return ToImageDataFromUC3(mat2);
    }

    private IImageData Combine(params IImageData[] imageDataList)
    {
        var height = imageDataList[0].Height;
        var width = imageDataList[0].Width;

        var tab = new Color[height, width];
        
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var R = imageDataList.Max(x => x[i, j].R);
                var G = imageDataList.Max(x => x[i, j].G);
                var B = imageDataList.Max(x => x[i, j].B);
                tab[i, j] = Color.FromArgb(R, G, B);
            }
        }

        return new ImageData(tab);
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

    public Mat CannyOperatorEdgeDetection(Mat mat, double threshold1 = 100, double threshold2 = 200)
    {
        var edges = new Mat(mat.Rows, mat.Cols, MatType.CV_16SC1);
        Cv2.Canny(mat, edges, threshold1, threshold2);
        return edges;
    }
}