using System.IO;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.FourierServices;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class AddPeriodicNoiseViewModel : ReactiveObject
{
    private readonly FftService fftService = new();
    private readonly ImageData imageData;
    private readonly ImageData outputImage;
    private double amplitude = 10;

    private double frequencyX = 0.5;
    private double frequencyY = 0.5;
    private double phase;

    public AddPeriodicNoiseViewModel(ImageData imageData, ImageData outputImage)
    {
        this.imageData = imageData;
        this.outputImage = outputImage;
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public IImage FourierBeforeImage { get; set; }

    public IImage FourierAfterImage { get; set; }

    public double Amplitude
    {
        get => amplitude;
        set
        {
            amplitude = value;
            this.RaisePropertyChanged();
        }
    }

    public double FrequencyX
    {
        get => frequencyX;
        set
        {
            frequencyX = value;
            this.RaisePropertyChanged();
        }
    }

    public double FrequencyY
    {
        get => frequencyY;
        set
        {
            frequencyY = value;
            this.RaisePropertyChanged();
        }
    }

    public double Phase
    {
        get => phase;
        set
        {
            phase = value;
            this.RaisePropertyChanged();
        }
    }

    private ImageData FourierBeforeImageData
    {
        set
        {
            FourierBeforeImage = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierBeforeImage));
        }
    }

    private ImageData FourierAfterImageData
    {
        set
        {
            FourierAfterImage = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAfterImage));
        }
    }

    public void Refresh()
    {
        var complex = new ComplexData(imageData);

        var fourier = fftService.ForwardFFT(complex);
        FourierBeforeImageData = GetFourierImageData(fourier);
        var complex2 = fftService.AddPeriodicNoise(complex, FrequencyX, FrequencyY, Amplitude, Phase);
        var fourier2 = fftService.ForwardFFT(complex2);
        FourierAfterImageData = GetFourierImageData(fourier2);

        var result = complex2.ToImageData();
        outputImage.Update(result);
    }

    private ImageData GetFourierImageData(ComplexData fourier)
    {
        var fourier2 = fftService.FFTShift(fourier);
        var fourier3 = fftService.LogN(fourier2);
        var fourier4 = fftService.Normalize(fourier3);
        var fourierImageData = fourier4.ToImageData();
        return fourierImageData;
    }
}