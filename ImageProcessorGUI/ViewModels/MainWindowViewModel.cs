using System.Windows.Input;
using ImageProcessorGUI.Models;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public string Title { get; set; } = "Image processor";
        public ICommand OpenImageCommand => ReactiveCommand.Create(() => MenuModel.OpenImage());
        public ICommand SaveImageCommand => ReactiveCommand.Create(() => MenuModel.SaveImage());
        public ICommand DuplicateImageCommand => ReactiveCommand.Create(() => MenuModel.DuplicateImage());
        public ICommand ShowValueHistogramCommand => ReactiveCommand.Create(() => MenuModel.ShowValueHistogram());
        public ICommand ShowRgbHistogramCommand => ReactiveCommand.Create(() => MenuModel.ShowRgbHistogram());
        public ICommand ShowRHistogramCommand => ReactiveCommand.Create(() => MenuModel.ShowRHistogram());
        public ICommand ShowGHistogramCommand => ReactiveCommand.Create(() => MenuModel.ShowGHistogram());
        public ICommand ShowBHistogramCommand => ReactiveCommand.Create(() => MenuModel.ShowBHistogram());
        public ICommand ShowScaledUp200PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledUp200Percent());
        public ICommand ShowScaledUp150PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledUp150Percent());
        public ICommand ShowScaledDown50PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledDown50Percent());
        public ICommand ShowScaledDown25PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledDown25Percent());
        public ICommand ShowScaledDown20PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledDown20Percent());
        public ICommand ShowScaledDown10PercentCommand => ReactiveCommand.Create(() => MenuModel.ShowScaledDown10Percent());
        public ICommand LinearStretchingCommand => ReactiveCommand.Create(() => MenuModel.LinearStretching());
        public ICommand GammaStretchingCommand => ReactiveCommand.Create(() => MenuModel.GammaStretching());
        public ICommand EqualizeHistogramCommand => ReactiveCommand.Create(() => MenuModel.EqualizeHistogram());
        public ICommand NegateImageCommand => ReactiveCommand.Create(() => MenuModel.NegateImage());
        public ICommand BinaryThresholdCommand => ReactiveCommand.Create(() => MenuModel.BinaryThreshold());
        public ICommand GreyscaleThresholdCommand => ReactiveCommand.Create(() => MenuModel.GreyscaleThreshold());
        public ICommand AddImageCommand => ReactiveCommand.Create(() => MenuModel.AddImage());
        public ICommand AddImageWithoutSaturateCommand => ReactiveCommand.Create(() => MenuModel.AddImageWithoutSaturate());
        public ICommand ImagesDifferenceCommand => ReactiveCommand.Create(() => MenuModel.ImagesDifference());
        public ICommand AddNumberToImageCommand => ReactiveCommand.Create(() => MenuModel.AddNumberToImage());
        public ICommand SubtractNumberFromImageCommand => ReactiveCommand.Create(() => MenuModel.SubtractNumberFromImage());
        public ICommand BinaryAndCommand => ReactiveCommand.Create(() => MenuModel.BinaryAnd());
        public ICommand BinaryOrCommand => ReactiveCommand.Create(() => MenuModel.BinaryOr());
        public ICommand BinaryXorCommand => ReactiveCommand.Create(() => MenuModel.BinaryXor());
        public ICommand BinaryNotCommand => ReactiveCommand.Create(() => MenuModel.BinaryNot());
        public ICommand GetBinaryMaskCommand => ReactiveCommand.Create(() => MenuModel.GetBinaryMask());
        public ICommand Get8BitMaskCommand => ReactiveCommand.Create(() => MenuModel.Get8BitMask());
        public ICommand MedianBlurCommand => ReactiveCommand.Create(() => MenuModel.MedianBlur());
        public ICommand GaussianBlurCommand => ReactiveCommand.Create(() => MenuModel.GaussianBlur());
        public ICommand SharpeningCommand => ReactiveCommand.Create(() => MenuModel.Sharpening());
        public ICommand EdgeSobelEastCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelEast());
        public ICommand EdgeSobelNorthEastCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelNorthEast());
        public ICommand EdgeSobelNorthCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelNorth());
        public ICommand EdgeSobelNorthWestCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelNorthWest());
        public ICommand EdgeSobelWestCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelWest());
        public ICommand EdgeSobelSouthWestCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelSouthWest());
        public ICommand EdgeSobelSouthCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelSouth());
        public ICommand EdgeSobelSouthEastCommand => ReactiveCommand.Create(() => MenuModel.EdgeSobelSouthEast());
        public ICommand FillBorderConstantCommand => ReactiveCommand.Create(() => MenuModel.FillBorderConstant());
        public ICommand FillResultBorderConstantCommand => ReactiveCommand.Create(() => MenuModel.FillResultBorderConstant());
        public ICommand FillBorderReflectCommand => ReactiveCommand.Create(() => MenuModel.FillBorderReflect());
        public ICommand FillBorderWrapCommand => ReactiveCommand.Create(() => MenuModel.FillBorderWrap());
        public ICommand CalculateMedian3x3Command => ReactiveCommand.Create(() => MenuModel.CalculateMedian3x3());
        public ICommand CalculateMedian5x5Command => ReactiveCommand.Create(() => MenuModel.CalculateMedian5x5());
        public ICommand CalculateMedian7x7Command => ReactiveCommand.Create(() => MenuModel.CalculateMedian7x7());
        public ICommand CalculateMedian9x9Command => ReactiveCommand.Create(() => MenuModel.CalculateMedian9x9());
        public ICommand SobelEdgeDetectionCommand => ReactiveCommand.Create(() => MenuModel.SobelEdgeDetection());
        public ICommand PrewittEdgeDetectionCommand => ReactiveCommand.Create(() => MenuModel.PrewittEdgeDetection());
        public ICommand CannyOperatorEdgeDetectionCommand => ReactiveCommand.Create(() => MenuModel.CannyOperatorEdgeDetection());
        public ICommand OtsuSegmentationCommand => ReactiveCommand.Create(() => MenuModel.OtsuSegmentation());
        public ICommand AdaptiveThresholdSegmentationCommand => ReactiveCommand.Create(() => MenuModel.AdaptativeThresholdSegmentation());
        public ICommand MorphologyErosionCommand => ReactiveCommand.Create(() => MenuModel.MorphologyErosion());
        public ICommand MorphologyDilationCommand => ReactiveCommand.Create(() => MenuModel.MorphologyDilation());
        public ICommand MorphologyOpeningCommand => ReactiveCommand.Create(() => MenuModel.MorphologyOpening());
        public ICommand MorphologyClosingCommand => ReactiveCommand.Create(() => MenuModel.MorphologyClosing());
        public ICommand CalculateFeatureVectorCommand => ReactiveCommand.Create(() => MenuModel.CalculateFeatureVector());
        public ICommand ModifyAmplitudeSpectrumCommand => ReactiveCommand.Create(() => MenuModel.ModifyAmplitudeSpectrum());

        public MenuModel MenuModel { get; set; } = new();
    }
}