﻿using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class MorphologyService : OpenCvService
{
    public ImageData Erosion(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Erosion(mat);
        return ToImageDataFromUC3(mat);
    }

    public ImageData Dilation(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Dilation(mat);
        return ToImageDataFromUC3(mat);
    }

    public ImageData Opening(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Opening(mat);
        return ToImageDataFromUC3(mat);
    }

    public ImageData Closing(ImageData imageData)
    {
        var mat = ToMatrix(imageData);
        mat = Closing(mat);
        return ToImageDataFromUC3(mat);
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