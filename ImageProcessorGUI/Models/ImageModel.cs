using System.Threading.Tasks;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;

namespace ImageProcessorGUI.Models;

public class ImageModel
{
    private readonly IServiceProvider _serviceProvider;

    public ImageModel(ImageData imageData, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        ImageData = imageData;
    }

    public ImageData ImageData { get; set; }
    public double ImageWidth => (double)((decimal)ImageData.Width * Scale);
    public double ImageHeight => (double)((decimal)ImageData.Height * Scale);

    public decimal Scale { get; set; } = 1;

    public void OpenImage()
    {
        _serviceProvider.OpenImageService.OpenImage();
    }

    public async Task SaveImage()
    {
        await _serviceProvider.SaveImageService.SaveImageAsync(ImageData);
    }

    public void DuplicateImage()
    {
        _serviceProvider.DuplicateImageService.DuplicateImage(ImageData);
    }

    public void ShowValueHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetValueHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRgbHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetRgbHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetRedHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowGHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetGreenHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowBHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetBlueHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowScaledUp200Percent()
    {
        Scale *= 2;
    }

    public void ShowScaledUp150Percent()
    {
        Scale *= 1.5m;
    }

    public void ShowScaledDown50Percent()
    {
        Scale *= 0.5m;
    }

    public void ShowScaledDown25Percent()
    {
        Scale *= 0.25m;
    }

    public void ShowScaledDown20Percent()
    {
        Scale *= 0.2m;
    }

    public void ShowScaledDown10Percent()
    {
        Scale *= 0.1m;
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