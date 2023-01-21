using System.IO;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class FourierTransformViewModel : ReactiveObject
{
    private readonly FftService fftService = new();
    private readonly IImageData inputImageData;
    private readonly IImageData outputImageData;


    public FourierTransformViewModel(IImageData inputImageData, IImageData outputImageData)
    {
        this.inputImageData = inputImageData;
        this.outputImageData = outputImageData;
    }

    public Bitmap FourierAmplitudeBefore { get; private set; }

    public IImageData FourierAmplitudeBeforeImageData
    {
        set
        {
            FourierAmplitudeBefore = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged();
        }
    }

    public Bitmap FourierAmplitudeAfter { get; private set; }

    public IImageData FourierAmplitudeAfterImageData
    {
        set
        {
            FourierAmplitudeAfter = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged();
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public void Refresh()
    {
        var complex = fftService.ToComplexData(inputImageData);
        var fourier = fftService.ForwardFFT(complex);
        var fourier2 = fftService.FFTShift(fourier);
        var fourier3 = fftService.LogN(fourier2);
        var fourierImageData = fftService.ToImageData(fourier3);
        FourierAmplitudeBeforeImageData = fourierImageData;
        FourierAmplitudeAfterImageData = fourierImageData;
    }
}