using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services.OpenCvServices;

/// <summary>
///     Serwis do przeprowadzania operacji morfologicznych.
/// </summary>
public class MorphologyService : OpenCvService
{
    /// <summary>
    ///     Erozja.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData Erosion(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Erosion(mat);
        return ToImageDataFromUC3(mat);
    }

    /// <summary>
    ///     Dylatacja.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData Dilation(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Dilation(mat);
        return ToImageDataFromUC3(mat);
    }

    /// <summary>
    ///     Otwarcie.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData Opening(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Opening(mat);
        return ToImageDataFromUC3(mat);
    }

    /// <summary>
    ///     Zamknięcie.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData Closing(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Closing(mat);
        return ToImageDataFromUC3(mat);
    }

    /// <summary>
    ///     Erozja.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    private static Mat Erosion(Mat mat)
    {
        Cv2.Erode(mat, mat, new Mat());
        return mat;
    }

    /// <summary>
    ///     Dylatacja.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    private static Mat Dilation(Mat mat)
    {
        Cv2.Dilate(mat, mat, new Mat());
        return mat;
    }

    /// <summary>
    ///     Otwarcie.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    private static Mat Opening(Mat mat)
    {
        Cv2.MorphologyEx(mat, mat, MorphTypes.Open, new Mat());
        return mat;
    }

    /// <summary>
    ///     Zamknięcie.
    /// </summary>
    /// <param name="mat"></param>
    /// <returns></returns>
    private static Mat Closing(Mat mat)
    {
        Cv2.MorphologyEx(mat, mat, MorphTypes.Close, new Mat());
        return mat;
    }
}