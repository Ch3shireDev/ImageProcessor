using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ImageProcessorGUI.Models;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase, INotifyPropertyChanged
{
    public MainWindowViewModel()
    {
    }

    public MainWindowViewModel(ImageModel imageModel)
    {
        ImageModel = imageModel;
    }

    public string Title { get; set; } = "Image processor";
    //public MenuViewModel MenuViewModel { get; set; } = new();

    public ICommand OpenImageCommand => ReactiveCommand.Create(() => ImageModel.OpenImage());
    public ICommand SaveImageCommand => ReactiveCommand.Create(() => ImageModel.SaveImage());
    public ICommand DuplicateImageCommand => ReactiveCommand.Create(() => ImageModel.DuplicateImage());
    public ICommand ShowValueHistogramCommand => ReactiveCommand.Create(() => ImageModel.ShowValueHistogram());
    public ICommand ShowRgbHistogramCommand => ReactiveCommand.Create(() => ImageModel.ShowRgbHistogram());
    public ICommand ShowRHistogramCommand => ReactiveCommand.Create(() => ImageModel.ShowRHistogram());
    public ICommand ShowGHistogramCommand => ReactiveCommand.Create(() => ImageModel.ShowGHistogram());
    public ICommand ShowBHistogramCommand => ReactiveCommand.Create(() => ImageModel.ShowBHistogram());

    public ICommand ShowScaledUp200PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledUp200Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledUp150PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledUp150Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown50PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledDown50Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown25PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledDown25Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown20PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledDown20Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand ShowScaledDown10PercentCommand => ReactiveCommand.Create(() =>
    {
        ImageModel.ShowScaledDown10Percent();
        OnPropertyChanged(nameof(ImageWidth));
        OnPropertyChanged(nameof(ImageHeight));
    });

    public ICommand LinearStretchingCommand => ReactiveCommand.Create(() => ImageModel.LinearStretching());
    public ICommand GammaStretchingCommand => ReactiveCommand.Create(() => ImageModel.GammaStretching());
    public ICommand EqualizeHistogramCommand => ReactiveCommand.Create(() => ImageModel.EqualizeHistogram());
    public ICommand NegateImageCommand => ReactiveCommand.Create(() => ImageModel.NegateImage());
    public ICommand BinaryThresholdCommand => ReactiveCommand.Create(() => ImageModel.BinaryThreshold());
    public ICommand GreyscaleThresholdCommand => ReactiveCommand.Create(() => ImageModel.GreyscaleThreshold());
    public ICommand AddImageCommand => ReactiveCommand.Create(() => ImageModel.AddImage());

    public ICommand AddImageWithoutSaturateCommand =>
        ReactiveCommand.Create(() => ImageModel.AddImageWithoutSaturate());

    public ICommand ImagesDifferenceCommand => ReactiveCommand.Create(() => ImageModel.ImagesDifference());
    public ICommand AddNumberToImageCommand => ReactiveCommand.Create(() => ImageModel.AddNumberToImage());

    public ICommand SubtractNumberFromImageCommand =>
        ReactiveCommand.Create(() => ImageModel.SubtractNumberFromImage());

    public ICommand BinaryAndCommand => ReactiveCommand.Create(() => ImageModel.BinaryAnd());
    public ICommand BinaryOrCommand => ReactiveCommand.Create(() => ImageModel.BinaryOr());
    public ICommand BinaryXorCommand => ReactiveCommand.Create(() => ImageModel.BinaryXor());
    public ICommand BinaryNotCommand => ReactiveCommand.Create(() => ImageModel.BinaryNot());
    public ICommand GetBinaryMaskCommand => ReactiveCommand.Create(() => ImageModel.GetBinaryMask());
    public ICommand Get8BitMaskCommand => ReactiveCommand.Create(() => ImageModel.Get8BitMask());
    public ICommand MedianBlurCommand => ReactiveCommand.Create(() => ImageModel.MedianBlur());
    public ICommand GaussianBlurCommand => ReactiveCommand.Create(() => ImageModel.GaussianBlur());
    public ICommand SharpeningCommand => ReactiveCommand.Create(() => ImageModel.Sharpening());
    public ICommand EdgeSobelEastCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelEast());
    public ICommand EdgeSobelNorthEastCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelNorthEast());
    public ICommand EdgeSobelNorthCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelNorth());
    public ICommand EdgeSobelNorthWestCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelNorthWest());
    public ICommand EdgeSobelWestCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelWest());
    public ICommand EdgeSobelSouthWestCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelSouthWest());
    public ICommand EdgeSobelSouthCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelSouth());
    public ICommand EdgeSobelSouthEastCommand => ReactiveCommand.Create(() => ImageModel.EdgeSobelSouthEast());
    public ICommand FillBorderConstantCommand => ReactiveCommand.Create(() => ImageModel.FillBorderConstant());

    public ICommand FillResultBorderConstantCommand =>
        ReactiveCommand.Create(() => ImageModel.FillResultBorderConstant());

    public ICommand FillBorderReflectCommand => ReactiveCommand.Create(() => ImageModel.FillBorderReflect());
    public ICommand FillBorderWrapCommand => ReactiveCommand.Create(() => ImageModel.FillBorderWrap());
    public ICommand CalculateMedian3x3Command => ReactiveCommand.Create(() => ImageModel.CalculateMedian3x3());
    public ICommand CalculateMedian5x5Command => ReactiveCommand.Create(() => ImageModel.CalculateMedian5x5());
    public ICommand CalculateMedian7x7Command => ReactiveCommand.Create(() => ImageModel.CalculateMedian7x7());
    public ICommand CalculateMedian9x9Command => ReactiveCommand.Create(() => ImageModel.CalculateMedian9x9());
    public ICommand SobelEdgeDetectionCommand => ReactiveCommand.Create(() => ImageModel.SobelEdgeDetection());
    public ICommand PrewittEdgeDetectionCommand => ReactiveCommand.Create(() => ImageModel.PrewittEdgeDetection());

    public ICommand CannyOperatorEdgeDetectionCommand =>
        ReactiveCommand.Create(() => ImageModel.CannyOperatorEdgeDetection());

    public ICommand OtsuSegmentationCommand => ReactiveCommand.Create(() => ImageModel.OtsuSegmentation());

    public ICommand AdaptiveThresholdSegmentationCommand =>
        ReactiveCommand.Create(() => ImageModel.AdaptativeThresholdSegmentation());

    public ICommand MorphologyErosionCommand => ReactiveCommand.Create(() => ImageModel.MorphologyErosion());
    public ICommand MorphologyDilationCommand => ReactiveCommand.Create(() => ImageModel.MorphologyDilation());
    public ICommand MorphologyOpeningCommand => ReactiveCommand.Create(() => ImageModel.MorphologyOpening());
    public ICommand MorphologyClosingCommand => ReactiveCommand.Create(() => ImageModel.MorphologyClosing());
    public ICommand CalculateFeatureVectorCommand => ReactiveCommand.Create(() => ImageModel.CalculateFeatureVector());

    public ICommand ModifyAmplitudeSpectrumCommand =>
        ReactiveCommand.Create(() => ImageModel.ModifyAmplitudeSpectrum());

    public ImageModel? ImageModel { get; set; }

    public double ImageWidth => ImageModel.ImageWidth;
    public double ImageHeight => ImageModel.ImageHeight;

    public Bitmap Bitmap => ImageModel.ImageData.Bitmap;
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}