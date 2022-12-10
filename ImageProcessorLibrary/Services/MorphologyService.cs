using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class MorphologyService: OpenCvService
{
    public IImageData Erosion(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Erosion(mat);
        return ToImageData(mat);
    }

    public IImageData Dilation(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Dilation(mat);
        return ToImageData(mat);
    }

    public IImageData Opening(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Opening(mat);
        return ToImageData(mat);
    }

    public IImageData Closing(IImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Closing(mat);
        return ToImageData(mat);
    }
    public Mat Erosion(Mat mat)
    {
        Cv2.Erode(mat, mat, new Mat());
        return mat;
    }

    public Mat Dilation(Mat mat)
    {
        Cv2.Dilate(mat, mat, new Mat());
        return mat;
    }

    public Mat Opening(Mat mat)
    {
        Cv2.MorphologyEx(mat, mat, MorphTypes.Open, new Mat());
        return mat;
    }

    public Mat Closing(Mat mat)
    {
        Cv2.MorphologyEx(mat, mat, MorphTypes.Close, new Mat());
        return mat;
    }



}