using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;

namespace ImageProcessorGUI.Models;

public class ImageModel
{
    private readonly IServiceProvider _serviceProvider;

    public ImageData ImageData { get; set; }
    
    internal ImageModel(){}
    public ImageModel(ImageData imageData, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        ImageData = imageData;
    }

    public void OpenImage()
    {
        _serviceProvider.FileService.OpenImage();
    }

    public void SaveImage()
    {
        _serviceProvider.FileService.SaveImage(ImageData);
    }

    public void DuplicateImage()
    {
        _serviceProvider.FileService.DuplicateImage(ImageData);
    }

    public void ShowValueHistogram()
    {
    }

    public void ShowRgbHistogram()
    {
    }

    public void ShowRHistogram()
    {
    }

    public void ShowGHistogram()
    {
    }

    public void ShowBHistogram()
    {
    }

    public void ShowScaledUp200Percent()
    {
    }

    public void ShowScaledUp150Percent()
    {
    }

    public void ShowScaledDown50Percent()
    {
    }

    public void ShowScaledDown25Percent()
    {
    }

    public void ShowScaledDown20Percent()
    {
    }

    public void ShowScaledDown10Percent()
    {
    }

    public void LinearStretching()
    {
    }

    public void GammaStretching()
    {
    }

    public void EqualizeHistogram()
    {
    }

    public void NegateImage()
    {
    }

    public void BinaryThreshold()
    {
    }

    public void GreyscaleThreshold()
    {
    }

    public void AddImage()
    {
    }

    public void AddImageWithoutSaturate()
    {
    }

    public void ImagesDifference()
    {
    }

    public void AddNumberToImage()
    {
    }

    public void SubtractNumberFromImage()
    {
    }

    public void BinaryAnd()
    {
    }

    public void BinaryOr()
    {
    }

    public void BinaryXor()
    {
    }

    public void BinaryNot()
    {
    }

    public void GetBinaryMask()
    {
    }

    public void Get8BitMask()
    {
    }

    public void GaussianBlur()
    {
    }

    public void EdgeSobelEast()
    {
    }

    public void EdgeSobelNorthEast()
    {
    }

    public void EdgeSobelNorth()
    {
    }

    public void EdgeSobelNorthWest()
    {
    }

    public void EdgeSobelWest()
    {
    }

    public void EdgeSobelSouthWest()
    {
    }

    public void EdgeSobelSouth()
    {
    }

    public void EdgeSobelSouthEast()
    {
    }

    public void FillBorderConstant()
    {
    }

    public void FillResultBorderConstant()
    {
    }

    public void FillBorderReflect()
    {
    }

    public void FillBorderWrap()
    {
    }

    public void CalculateMedian3x3()
    {
    }

    public void CalculateMedian5x5()
    {
    }

    public void CalculateMedian7x7()
    {
    }

    public void CalculateMedian9x9()
    {
    }

    public void SobelEdgeDetection()
    {
    }

    public void PrewittEdgeDetection()
    {
    }

    public void CannyOperatorEdgeDetection()
    {
    }

    public void OtsuSegmentation()
    {
    }

    public void AdaptativeThresholdSegmentation()
    {
    }

    public void MorphologyErosion()
    {
    }

    public void MorphologyDilation()
    {
    }

    public void MorphologyOpening()
    {
    }

    public void MorphologyClosing()
    {
    }

    public void CalculateFeatureVector()
    {
    }

    public void ModifyAmplitudeSpectrum()
    {
    }

    public void MedianBlur()
    {
    }

    public void Sharpening()
    {
    }
}