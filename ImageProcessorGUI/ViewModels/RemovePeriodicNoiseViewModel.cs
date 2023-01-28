using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.Enums;
using ImageProcessorLibrary.Services.FourierServices;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class RemovePeriodicNoiseViewModel : ReactiveObject
{
    private readonly FftService fftService = new();
    private readonly ImageData inputImageData;
    private readonly ImageData outputImageData;

    private int x1, width, y1, height;


    public RemovePeriodicNoiseViewModel(ImageData inputImageData, ImageData outputImageData)
    {
        this.inputImageData = inputImageData;
        this.outputImageData = outputImageData;
        FourierMaxWidth = fftService.FindWidthForPowerOfTwo(inputImageData);
        X1 = FourierMaxWidth * 5.0 / 16.0;
        Y1 = FourierMaxWidth * 5.0 / 16.0;
        Width = FourierMaxWidth / 8.0;
        Height = FourierMaxWidth / 8.0;
    }

    public int FourierMaxWidth { get; set; }

    public double X1
    {
        get => x1;
        set
        {
            x1 = SetInRange(value);
            this.RaisePropertyChanged();
        }
    }

    public double Y1
    {
        get => y1;
        set
        {
            y1 = SetInRange(value);
            this.RaisePropertyChanged();
        }
    }


    public double Width
    {
        get => width;
        set
        {
            width = SetInRange(value);
            this.RaisePropertyChanged();
        }
    }


    public double Height
    {
        get => height;
        set
        {
            height = SetInRange(value);
            this.RaisePropertyChanged();
        }
    }

    public List<RemoveRecanglesModeEnum> Modes { get; set; } = new()
    {
        RemoveRecanglesModeEnum.NONE,
        RemoveRecanglesModeEnum.SINGLE,
        RemoveRecanglesModeEnum.DOUBLE,
        RemoveRecanglesModeEnum.QUAD
    };

    public RemoveRecanglesModeEnum Mode { get; set; } = RemoveRecanglesModeEnum.DOUBLE;

    public IImage FourierAmplitudeBefore { get; private set; }

    public ImageData FourierAmplitudeBeforeImageData
    {
        set
        {
            if (value.Filebytes != null) FourierAmplitudeBefore = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAmplitudeBefore));
        }
    }

    public IImage FourierAmplitudeAfter { get; private set; }

    public ImageData FourierAmplitudeAfterImageData
    {
        set
        {
            if (value.Filebytes != null) FourierAmplitudeAfter = new Bitmap(new MemoryStream(value.Filebytes));
            this.RaisePropertyChanged(nameof(FourierAmplitudeAfter));
        }
    }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    private int SetInRange(double value)
    {
        if (value < 0) value = 0;
        if (value > FourierMaxWidth) value = FourierMaxWidth;
        return (int)value;
    }

    public void Refresh()
    {
        var complexDataInput = new ComplexData(inputImageData);

        var fourier1 = fftService.ForwardFFT(complexDataInput);
        var fourier2 = fftService.FFTShift(fourier1);
        FourierAmplitudeBeforeImageData = ToImageData(fourier2);
        var fourier3 = fftService.RemoveRectangles(fourier2, x1, y1, x1 + width, y1 + height, Mode);
        FourierAmplitudeAfterImageData = ToImageData(fourier3);
        var fourier4 = fftService.FFTShift(fourier3);

        var complexDataOutput = fftService.InverseFFT(fourier4);

        outputImageData.Update(complexDataOutput.ToImageData());
    }

    private ImageData ToImageData(ComplexData fourier2)
    {
        var fourier3 = fftService.LogN(fourier2);
        var fourier4 = fftService.Normalize(fourier3);
        var fourierImageData = fourier4.ToImageData();
        return fourierImageData;
    }
}