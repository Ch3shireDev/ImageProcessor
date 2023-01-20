﻿using ImageProcessorLibrary.DataStructures;
using OpenCvSharp;

namespace ImageProcessorLibrary.Services;

public class SegmentationService
{
    private readonly OpenCvService cvService = new OpenCvService();

    public IImageData OtsuSegmentation(IImageData image)
    {
        var mat = cvService.ToGrayMatrix(image);

        Cv2.Threshold(mat, mat, 0,255,  ThresholdTypes.Otsu);


        return cvService.ToImageDataFromUC3(mat);
    }

    public IImageData AdaptiveThresholding(IImageData imageData)
    {
        var mat = cvService.ToGrayMatrix(imageData);

        Cv2.AdaptiveThreshold(mat, mat, 255, AdaptiveThresholdTypes.MeanC, ThresholdTypes.Binary, 3, 0);

        return cvService.ToImageDataFromUC3(mat);
    }
}