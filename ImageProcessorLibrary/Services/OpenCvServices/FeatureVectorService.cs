using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services.OpenCvServices;

/// <summary>
///     Serwis do wyznaczania wektorów cech.
/// </summary>
public class FeatureVectorService : OpenCvService
{
    /// <summary>
    ///     Wylicza momenty przestrzenne dla macierzy.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public List<double> GetSpatialMoments(Mat mat)
    {
        var moments = Cv2.Moments(mat);
        var spatialMoments = new List<double>
        {
            moments.M00,
            moments.M10,
            moments.M01,
            moments.M20,
            moments.M11,
            moments.M02
        };


        return spatialMoments;
    }

    /// <summary>
    ///     Wylicza powierzchnię konturową.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public double GetCountourArea(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        return GetCountourArea(mat);
    }

    /// <summary>
    ///     Wylicza powierzchnię konturową.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    public double GetCountourArea(Mat mat)
    {
        return Cv2.ContourArea(mat);
    }

    /// <summary>
    ///     Wylicza momenty centralne dla macierzy.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public List<double> GetSpatialMoments(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        return GetSpatialMoments(mat);
    }

    /// <summary>
    ///     Wylicza momenty centralne dla macierzy.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public List<double> GetCentralMoments(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        var centralMoments = GetCentralMoments(mat);
        return centralMoments;
    }

    /// <summary>
    ///     Wylicza znormalizowane momenty centralne dla macierzy.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public List<double> GetNormalizedCentralMoments(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        var normalizedCentralMoments = GetNormalizedCentralMoments(mat);
        return normalizedCentralMoments;
    }

    /// <summary>
    ///     Wylicza wektor cech dla obrazu.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public IEnumerable<FeatureVector> GetFeatureVectors(ImageData imageData)
    {
        var mat = ToGrayMatrix(imageData);
        var contours = GetContours(mat);

        foreach (var contour in contours)
        {
            var moments = Cv2.Moments(mat);
            var area = GetContourArea(contour);
            var length = GetContourLength(contour);
            var W1 = GetW1(contour);
            var W2 = GetW2(contour);
            var W3 = GetW3(contour);
            var W9 = GetW9(contour);
            var solidity = GetSolidity(contour);
            var equivalentDiameter = GetEquivalentDiameter(contour);

            yield return new FeatureVector
            {
                M00 = moments.M00,
                M10 = moments.M10,
                M01 = moments.M01,
                M20 = moments.M20,
                M11 = moments.M11,
                M02 = moments.M02,
                SurfaceArea = area,
                Circumference = length,
                W1 = W1,
                W2 = W2,
                W3 = W3,
                W9 = W9,
                Solidity = solidity,
                EquivalentDiameter = equivalentDiameter
            };
        }
    }

    /// <summary>
    ///     Wylicza średnicę równoważną.
    /// </summary>
    /// <param name="contour"></param>
    /// <returns></returns>
    private double GetEquivalentDiameter(Mat<Point> contour)
    {
        var S = Cv2.ContourArea(contour);
        return Math.Sqrt(4 * S / Math.PI);
    }

    /// <summary>
    ///     Wylicza masywność.
    /// </summary>
    /// <param name="contour"></param>
    /// <returns></returns>
    private double GetSolidity(Mat<Point> contour)
    {
        var area = GetContourArea(contour);
        var mat = new Mat(contour.Height, contour.Width, contour.Type());
        Cv2.ConvexHull(contour, mat);
        var hullArea = Cv2.ContourArea(mat);
        var solidity = area / hullArea;
        return solidity;
    }

    private double GetW1(Mat<Point> contour)
    {
        var S = GetContourArea(contour);
        return 2 * Math.Sqrt(S / Math.PI);
    }

    private double GetW2(Mat<Point> contour)
    {
        var L = GetContourLength(contour);
        return L / Math.PI;
    }

    private double GetW3(Mat<Point> contour)
    {
        var L = GetContourLength(contour);
        var S = GetContourArea(contour);
        return L / (2 * Math.Sqrt(S * Math.PI)) - 1;
    }

    private double GetW9(Mat<Point> contour)
    {
        var L = GetContourLength(contour);
        var S = GetContourArea(contour);

        return 2 * Math.Sqrt(Math.PI * S) / L;
    }

    private static double GetContourArea(Mat<Point> contour)
    {
        var area = Cv2.ContourArea(contour);
        return area;
    }

    private static double GetContourLength(Mat<Point> contour)
    {
        var length = Cv2.ArcLength(contour, true);
        return length;
    }

    private static Mat<Point>[] GetContours(Mat mat)
    {
        var contours = Cv2.FindContoursAsMat(mat, RetrievalModes.List, ContourApproximationModes.ApproxSimple);
        return contours;
    }

    private static List<double> GetNormalizedCentralMoments(Mat mat)
    {
        var moments = Cv2.Moments(mat);
        var normalizedCentralMoments = new List<double>
        {
            moments.Nu20,
            moments.Nu11,
            moments.Nu02,
            moments.Nu30,
            moments.Nu21,
            moments.Nu12,
            moments.Nu03
        };

        return normalizedCentralMoments;
    }

    private static List<double> GetCentralMoments(Mat mat)
    {
        var moments = Cv2.Moments(mat);
        var centralMoments = new List<double>
        {
            moments.Mu20,
            moments.Mu11,
            moments.Mu02,
            moments.Mu30,
            moments.Mu21,
            moments.Mu12,
            moments.Mu03
        };

        return centralMoments;
    }
}