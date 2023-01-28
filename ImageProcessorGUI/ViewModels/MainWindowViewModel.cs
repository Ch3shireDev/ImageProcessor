using System.Windows.Input;
using Avalonia.Media;
using ImageProcessorGUI.Models;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

/// <summary>
///    Widok modelu głównego okna aplikacji.
/// </summary>
public class MainWindowViewModel : ReactiveObject
{
    public MainWindowViewModel(MainModel mainModel)
    {
        MainModel = mainModel;
        MainModel.ImageChanged += (a, b) => { this.RaisePropertyChanged(nameof(Image)); };
    }

    public string Title => MainModel?.ImageData?.Filename ?? "";

    public ICommand OpenImageCommand => ReactiveCommand.Create(async () => await MainModel?.OpenImage()!);
    public ICommand SaveImageCommand => ReactiveCommand.Create(() => MainModel?.SaveImage());
    public ICommand DuplicateImageCommand => ReactiveCommand.Create(() => MainModel?.DuplicateImage());
    public ICommand ShowValueHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowValueHistogram());
    public ICommand ShowRgbHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowRgbHistogram());
    public ICommand ShowRHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowRHistogram());
    public ICommand ShowGHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowGHistogram());
    public ICommand ShowBHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowBHistogram());

    public ICommand ShowNormalScaleCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ResetScale();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledUp200PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledUp200Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledUp150PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledUp150Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown50PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown50Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown25PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown25Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown20PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown20Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown10PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown10Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    public ICommand CreateGrayscaleCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.CreateGrayscale();
        this.RaisePropertyChanged(nameof(Image));
    });

    public ICommand BinaryOperationCommand => ReactiveCommand.Create(() => MainModel?.BinaryOperation());
    public ICommand SwapHorizontalCommand => ReactiveCommand.Create(() => MainModel?.SwapHorizontal());
    public ICommand LinearStretchingCommand => ReactiveCommand.Create(() => MainModel?.OpenLinearStretchingWindow());
    public ICommand GammaStretchingCommand => ReactiveCommand.Create(() => MainModel?.OpenGammaStretchingWindow());
    public ICommand EqualizeHistogramCommand => ReactiveCommand.Create(() => MainModel?.OpenEqualizeHistogramWindow());
    public ICommand NegateImageCommand => ReactiveCommand.Create(() => MainModel?.NegateImage());
    public ICommand BinaryThresholdCommand => ReactiveCommand.Create(() => MainModel?.BinaryThreshold());
    public ICommand GreyscaleThresholdOneSliderCommand => ReactiveCommand.Create(() => MainModel?.GreyscaleThresholdOneSlider());
    public ICommand GreyscaleThresholdTwoSlidersCommand => ReactiveCommand.Create(() => MainModel?.GreyscaleThresholdTwoSliders());
    public ICommand AddImageCommand => ReactiveCommand.Create(() => MainModel?.AddImages());
    public ICommand MathOperationCommand => ReactiveCommand.Create(() => MainModel?.MathOperation());
    public ICommand MedianBlurWithoutWeightsCommand => ReactiveCommand.Create(() => MainModel?.MedianBlurWithoutWeights());
    public ICommand MedianBlurWithWeightsCommand => ReactiveCommand.Create(() => MainModel?.MedianBlurWithWeights());
    public ICommand GaussianBlurCommand => ReactiveCommand.Create(() => MainModel?.GaussianBlur());
    public ICommand SharpeningMask1Command => ReactiveCommand.Create(() => MainModel?.SharpeningMask1());
    public ICommand SharpeningMask2Command => ReactiveCommand.Create(() => MainModel?.SharpeningMask2());
    public ICommand SharpeningMask3Command => ReactiveCommand.Create(() => MainModel?.SharpeningMask3());
    public ICommand EdgeSobelEastCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelEast());
    public ICommand EdgeSobelNorthEastCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelNorthEast());
    public ICommand EdgeSobelNorthCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelNorth());
    public ICommand EdgeSobelNorthWestCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelNorthWest());
    public ICommand EdgeSobelWestCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelWest());
    public ICommand EdgeSobelSouthWestCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelSouthWest());
    public ICommand EdgeSobelSouthCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelSouth());
    public ICommand EdgeSobelSouthEastCommand => ReactiveCommand.Create(() => MainModel?.EdgeSobelSouthEast());
    public ICommand CalculateMedianCommand => ReactiveCommand.Create(() => MainModel?.CalculateMedian());
    public ICommand SobelEdgeDetectionCommand => ReactiveCommand.Create(() => MainModel?.SobelEdgeDetection());
    public ICommand PrewittEdgeDetectionCommand => ReactiveCommand.Create(() => MainModel?.PrewittEdgeDetection());
    public ICommand CannyOperatorEdgeDetectionCommand => ReactiveCommand.Create(() => MainModel?.CannyOperatorEdgeDetection());
    public ICommand OtsuSegmentationCommand => ReactiveCommand.Create(() => MainModel?.OtsuSegmentation());
    public ICommand AdaptiveThresholdSegmentationCommand => ReactiveCommand.Create(() => MainModel?.AdaptativeThresholdSegmentation());
    public ICommand MorphologyErosionCommand => ReactiveCommand.Create(() => MainModel?.MorphologyErosion());
    public ICommand MorphologyDilationCommand => ReactiveCommand.Create(() => MainModel?.MorphologyDilation());
    public ICommand MorphologyOpeningCommand => ReactiveCommand.Create(() => MainModel?.MorphologyOpening());
    public ICommand MorphologyClosingCommand => ReactiveCommand.Create(() => MainModel?.MorphologyClosing());
    public ICommand CalculateFeatureVectorCommand => ReactiveCommand.Create(() => MainModel?.CalculateFeatureVector());
    public ICommand RemovePeriodicNoiseCommand => ReactiveCommand.Create(() => MainModel?.RemovePeriodicNoise());
    public ICommand AddPeriodicNoiseCommand => ReactiveCommand.Create(() => MainModel?.AddPeriodicNoise());
    public double ImageWidth => MainModel?.ImageWidth ?? 0;
    public double ImageHeight => MainModel?.ImageHeight ?? 0;
    public IImage? Image => MainModel?.AvaloniaImage;
    public MainModel? MainModel { get; set; }
    public ICommand ToBinaryImageCommand => ReactiveCommand.Create(() => MainModel?.ToBinaryImage());
}