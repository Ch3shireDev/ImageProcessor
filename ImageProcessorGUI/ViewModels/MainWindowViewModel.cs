using System.Windows.Input;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Image processor";
        public ICommand OpenImageCommand => ReactiveCommand.Create(() => Model.OpenImage());
        public ICommand SaveImageCommand => ReactiveCommand.Create(() => Model.SaveImage());
        public ICommand DuplicateImageCommand => ReactiveCommand.Create(() => Model.DuplicateImage());
        public ICommand ShowValueHistogramCommand => ReactiveCommand.Create(() => Model.ShowValueHistogram());
        public ICommand ShowRgbHistogramCommand => ReactiveCommand.Create(() => Model.ShowRgbHistogram());
        public ICommand ShowRHistogramCommand => ReactiveCommand.Create(() => Model.ShowRHistogram());
        public ICommand ShowGHistogramCommand => ReactiveCommand.Create(() => Model.ShowGHistogram());
        public ICommand ShowBHistogramCommand => ReactiveCommand.Create(() => Model.ShowBHistogram());
        public ICommand ShowScaledUp200PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledUp200Percent());
        public ICommand ShowScaledUp150PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledUp150Percent());
        public ICommand ShowScaledDown50PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledDown50Percent());
        public ICommand ShowScaledDown25PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledDown25Percent());
        public ICommand ShowScaledDown20PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledDown20Percent());
        public ICommand ShowScaledDown10PercentCommand => ReactiveCommand.Create(() => Model.ShowScaledDown10Percent());
        public ICommand LinearStretchingCommand => ReactiveCommand.Create(() => Model.LinearStretching());
        public ICommand GammaStretchingCommand => ReactiveCommand.Create(() => Model.GammaStretching());
        public ICommand EqualizeHistogramCommand => ReactiveCommand.Create(() => Model.EqualizeHistogram());
        public ICommand NegateImageCommand => ReactiveCommand.Create(() => Model.NegateImage());
        public ICommand BinaryThresholdCommand => ReactiveCommand.Create(() => Model.BinaryThreshold());
        public ICommand GreyscaleThresholdCommand => ReactiveCommand.Create(() => Model.GreyscaleThreshold());
        public ICommand AddImageCommand => ReactiveCommand.Create(() => Model.AddImage());
        public ICommand AddImageWithoutSaturateCommand => ReactiveCommand.Create(() => Model.AddImageWithoutSaturate());
        public ICommand ImagesDifferenceCommand => ReactiveCommand.Create(() => Model.ImagesDifference());
        public ICommand AddNumberToImageCommand => ReactiveCommand.Create(() => Model.AddNumberToImage());
        public ICommand SubtractNumberFromImageCommand => ReactiveCommand.Create(() => Model.SubtractNumberFromImage());
        public ICommand BinaryAndCommand => ReactiveCommand.Create(() => Model.BinaryAnd());
        public ICommand BinaryOrCommand => ReactiveCommand.Create(() => Model.BinaryOr());
        public ICommand BinaryXorCommand => ReactiveCommand.Create(() => Model.BinaryXor());
        public ICommand BinaryNotCommand => ReactiveCommand.Create(() => Model.BinaryNot());
        public ICommand GetBinaryMaskCommand => ReactiveCommand.Create(() => Model.GetBinaryMask());
        public ICommand Get8BitMaskCommand => ReactiveCommand.Create(() => Model.Get8BitMask());
        public ICommand MedianBlurCommand => ReactiveCommand.Create(() => Model.MedianBlur());
        public ICommand GaussianBlurCommand => ReactiveCommand.Create(() => Model.GaussianBlur());
        public ICommand SharpeningCommand => ReactiveCommand.Create(() => Model.Sharpening());
        public ICommand EdgeSobelEastCommand => ReactiveCommand.Create(() => Model.EdgeSobelEast());
        public ICommand EdgeSobelNorthEastCommand => ReactiveCommand.Create(() => Model.EdgeSobelNorthEast());
        public ICommand EdgeSobelNorthCommand => ReactiveCommand.Create(() => Model.EdgeSobelNorth());
        public ICommand EdgeSobelNorthWestCommand => ReactiveCommand.Create(() => Model.EdgeSobelNorthWest());
        public ICommand EdgeSobelWestCommand => ReactiveCommand.Create(() => Model.EdgeSobelWest());
        public ICommand EdgeSobelSouthWestCommand => ReactiveCommand.Create(() => Model.EdgeSobelSouthWest());
        public ICommand EdgeSobelSouthCommand => ReactiveCommand.Create(() => Model.EdgeSobelSouth());
        public ICommand EdgeSobelSouthEastCommand => ReactiveCommand.Create(() => Model.EdgeSobelSouthEast());
        public ICommand FillBorderConstantCommand => ReactiveCommand.Create(() => Model.FillBorderConstant());
        public ICommand FillResultBorderConstantCommand => ReactiveCommand.Create(() => Model.FillResultBorderConstant());
        public ICommand FillBorderReflectCommand => ReactiveCommand.Create(() => Model.FillBorderReflect());
        public ICommand FillBorderWrapCommand => ReactiveCommand.Create(() => Model.FillBorderWrap());
        public ICommand CalculateMedian3x3Command => ReactiveCommand.Create(() => Model.CalculateMedian3x3());
        public ICommand CalculateMedian5x5Command => ReactiveCommand.Create(() => Model.CalculateMedian5x5());
        public ICommand CalculateMedian7x7Command => ReactiveCommand.Create(() => Model.CalculateMedian7x7());
        public ICommand CalculateMedian9x9Command => ReactiveCommand.Create(() => Model.CalculateMedian9x9());
        public ICommand SobelEdgeDetectionCommand => ReactiveCommand.Create(() => Model.SobelEdgeDetection());
        public ICommand PrewittEdgeDetectionCommand => ReactiveCommand.Create(() => Model.PrewittEdgeDetection());
        public ICommand CannyOperatorEdgeDetectionCommand => ReactiveCommand.Create(() => Model.CannyOperatorEdgeDetection());
        public ICommand OtsuSegmentationCommand => ReactiveCommand.Create(() => Model.OtsuSegmentation());
        public ICommand AdaptiveThresholdSegmentationCommand => ReactiveCommand.Create(() => Model.AdaptativeThresholdSegmentation());
        public ICommand MorphologyErosionCommand => ReactiveCommand.Create(() => Model.MorphologyErosion());
        public ICommand MorphologyDilationCommand => ReactiveCommand.Create(() => Model.MorphologyDilation());
        public ICommand MorphologyOpeningCommand => ReactiveCommand.Create(() => Model.MorphologyOpening());
        public ICommand MorphologyClosingCommand => ReactiveCommand.Create(() => Model.MorphologyClosing());
        public ICommand CalculateFeatureVectorCommand => ReactiveCommand.Create(() => Model.CalculateFeatureVector());
        public ICommand ModifyAmplitudeSpectrumCommand => ReactiveCommand.Create(() => Model.ModifyAmplitudeSpectrum());

        public Model Model { get; set; } = new();
    }
}