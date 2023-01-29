using System.Windows.Input;
using Avalonia.Media;
using ImageProcessorGUI.Models;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

/// <summary>
///     Widok modelu głównego okna aplikacji.
/// </summary>
public class MainWindowViewModel : ReactiveObject
{
    /// <summary>
    ///     Konstruktor.
    /// </summary>
    /// <param name="mainModel"></param>
    public MainWindowViewModel(MainModel mainModel)
    {
        MainModel = mainModel;
        MainModel.ImageChanged += (a, b) => { this.RaisePropertyChanged(nameof(Image)); };
    }

    /// <summary>
    ///     Model główny.
    /// </summary>
    public MainModel? MainModel { get; set; }

    /// <summary>
    ///     Tytuł okna.
    /// </summary>
    public string Title => MainModel?.ImageData?.Filename ?? "";

    /// <summary>
    ///     Obraz.
    /// </summary>
    public IImage? Image => MainModel?.AvaloniaImage;

    /// <summary>
    ///     Obserwowana szerokość obrazu.
    /// </summary>
    public double ImageWidth => MainModel?.ImageWidth ?? 0;

    /// <summary>
    ///     Obserwowana wysokość obrazu.
    /// </summary>
    public double ImageHeight => MainModel?.ImageHeight ?? 0;


    /// <summary>
    ///     Polecenie otwarcia obrazu.
    /// </summary>
    public ICommand OpenImageCommand => ReactiveCommand.Create(async () => await MainModel?.OpenImage()!);

    /// <summary>
    ///     Polecenie zapisu obrazu.
    /// </summary>
    public ICommand SaveImageCommand => ReactiveCommand.Create(() => MainModel?.SaveImage());

    /// <summary>
    ///     Polecenie duplikacji okna obrazu.
    /// </summary>
    public ICommand DuplicateImageCommand => ReactiveCommand.Create(() => MainModel?.DuplicateImage());

    /// <summary>
    ///     Polecenie otwarcia histogramu wartości pikseli.
    /// </summary>
    public ICommand ShowValueHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowValueHistogram());

    /// <summary>
    ///     Polecenie otwarcia histogramów dla trzech barw.
    /// </summary>
    public ICommand ShowRgbHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowRgbHistogram());

    /// <summary>
    ///     Polecenie otwarcia histogramu dla koloru czerwonego.
    /// </summary>
    public ICommand ShowRHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowRHistogram());

    /// <summary>
    ///     Polecenie otwarcia histogramu dla koloru zielonego.
    /// </summary>
    public ICommand ShowGHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowGHistogram());

    /// <summary>
    ///     Polecenie otwarcia histogramu dla koloru niebieskiego.
    /// </summary>
    public ICommand ShowBHistogramCommand => ReactiveCommand.Create(() => MainModel?.ShowBHistogram());

    /// <summary>
    ///     Polecenie przedstawienia obrazu w pierwotnej skali..
    /// </summary>
    public ICommand ShowNormalScaleCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ResetScale();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 200% skali.
    /// </summary>
    public ICommand ShowScaledUp200PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledUp200Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 150% skali.
    /// </summary>
    public ICommand ShowScaledUp150PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledUp150Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 50% skali.
    /// </summary>
    public ICommand ShowScaledDown50PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown50Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 25% skali.
    /// </summary>
    public ICommand ShowScaledDown25PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown25Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 20% skali.
    /// </summary>
    public ICommand ShowScaledDown20PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown20Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w 10% skali.
    /// </summary>
    public ICommand ShowScaledDown10PercentCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.ShowScaledDown10Percent();
        this.RaisePropertyChanged(nameof(ImageWidth));
        this.RaisePropertyChanged(nameof(ImageHeight));
    });

    /// <summary>
    ///     Polecenie przedstawienia obrazu w skali szarości.
    /// </summary>
    public ICommand CreateGrayscaleCommand => ReactiveCommand.Create(() =>
    {
        MainModel?.CreateGrayscale();
        this.RaisePropertyChanged(nameof(Image));
    });

    /// <summary>
    ///     Polecenie wywołania okna operacji binarnych.
    /// </summary>
    public ICommand BinaryOperationCommand => ReactiveCommand.Create(() => MainModel?.BinaryOperation());

    /// <summary>
    ///     Polecenie odbicia obrazu w osi poziomej.
    /// </summary>
    public ICommand SwapHorizontalCommand => ReactiveCommand.Create(() => MainModel?.SwapHorizontal());

    /// <summary>
    ///     Polecenie rozciągnięcia liniowego histogramu obrazu.
    /// </summary>
    public ICommand LinearStretchingCommand => ReactiveCommand.Create(() => MainModel?.OpenLinearStretchingWindow());

    /// <summary>
    ///     Polecenie rozciągania gamma histogramu obrazu.
    /// </summary>
    public ICommand GammaStretchingCommand => ReactiveCommand.Create(() => MainModel?.OpenGammaStretchingWindow());

    /// <summary>
    ///     Polecenie equalizacji histogramu.
    /// </summary>
    public ICommand EqualizeHistogramCommand => ReactiveCommand.Create(() => MainModel?.OpenEqualizeHistogramWindow());

    /// <summary>
    ///     Polecenie negowania obrazu.
    /// </summary>
    public ICommand NegateImageCommand => ReactiveCommand.Create(() => MainModel?.NegateImage());

    /// <summary>
    ///     Polecenie progowania binarnego obrazu.
    /// </summary>
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
    public ICommand ToBinaryImageCommand => ReactiveCommand.Create(() => MainModel?.ToBinaryImage());
}