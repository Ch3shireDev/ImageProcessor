using System.IO;
using System.Windows.Input;
using Avalonia.Media;
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

    public IImage FourierAmplitudeBefore { get; private set; }

    public IImageData FourierAmplitudeBeforeImageData
    {
        set
        {
            FourierAmplitudeBefore = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAmplitudeBefore));
        }
    }

    public IImage FourierAmplitudeAfter { get; private set; }

    public IImageData FourierAmplitudeAfterImageData
    {
        set
        {
            FourierAmplitudeAfter = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAmplitudeAfter));
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public void Refresh()
    {
        var complex = new ComplexData(inputImageData);
        var fourier = fftService.ForwardFFT(complex);
        var fourier2 = fftService.FFTShift(fourier);
        var fourier3 = fftService.LogN(fourier2);
        var fourier4 = fftService.Normalize(fourier3);
        var fourierImageData = fftService.ToImageData(fourier4);
        FourierAmplitudeBeforeImageData = fourierImageData;
        FourierAmplitudeAfterImageData = fourierImageData;

        var complex2 = fftService.InverseFFT(fourier);
        var image = fftService.ToImageData(complex2);
        outputImageData.Update(image);
    }
}