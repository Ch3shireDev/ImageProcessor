using System.IO;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class PeriodicNoiseViewModel : ReactiveObject
{
    private readonly IImageData imageData;
    private readonly IImageData outputImage;

    public PeriodicNoiseViewModel(IImageData imageData, IImageData outputImage)
    {
        this.imageData = imageData;
        this.outputImage = outputImage;
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public IImage FourierBeforeImage{ get; set; }

    public IImage FourierAfterImage{ get; set; }

    private double _value1;

    public double Value1
    {
        get => _value1;
        set
        {
            _value1 = value;
            this.RaisePropertyChanged();
        }
    }

    private double _value2;

    public double Value2
    {
        get => _value2;
        set
        {
            _value2 = value;
            this.RaisePropertyChanged();
        }
    }

    private double _value3;

    public double Value3
    {
        get => _value3;
        set
        {
            _value3 = value;
            this.RaisePropertyChanged();
        }
    }


    IImageData FourierBeforeImageData
    {
        set
        {
            FourierBeforeImage = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierBeforeImage));
        }
    }

    IImageData FourierAfterImageData
    {
        set
        {
            FourierAfterImage = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAfterImage));
        }
    }


    private readonly FftService fftService = new FftService();
    public void Refresh()
    {
        var complex = new ComplexData(imageData);
        
        var fourier = fftService.ForwardFFT(complex);
        
        FourierBeforeImageData = GetFourierImageData(fourier);

        var complex2 = fftService.AddPeriodicNoise(complex, Value1, Value2, Value3);
        var fourier2 = fftService.ForwardFFT(complex2);
        FourierAfterImageData = GetFourierImageData(fourier2);

        var result = fftService.ToImageData(complex2);
        outputImage.Update(result);
    }

    private ImageData GetFourierImageData(ComplexData fourier)
    {
        var fourier2 = fftService.FFTShift(fourier);
        var fourier3 = fftService.LogN(fourier2);
        var fourier4 = fftService.Normalize(fourier3);
        var fourierImageData = fftService.ToImageData(fourier4);
        return fourierImageData;
    }
}