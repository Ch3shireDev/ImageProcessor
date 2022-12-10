using System;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class OptionsOneValueViewModel<T1> : ReactiveObject
{
    private readonly Func<ImageData, T1, ImageData>? _transform;
    private T1? value1;

    public OptionsOneValueViewModel(ImageData imageData, Func<ImageData, T1, ImageData> transform)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
        _transform = transform;
    }

    public OptionsOneValueViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public string? Value1Label { get; set; }
    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }

    T1 value1Min;
    T1 value1Max;

    public T1 Value1Min
    {
        get => value1Min;
        set
        {
            value1Min = value;
            this.RaisePropertyChanged();
        }
    }

    public T1 Value1Max
    {
        get => value1Max;
        set
        {
            value1Max = value;
            this.RaisePropertyChanged();
        }
    }

    public T1? Value1
    {
        get => value1;
        set
        {
            this.value1 = value;
            this.RaisePropertyChanged();
        }
    }
    
    public string? Header { get; set; }

    public virtual ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public void Refresh()
    {
        if (_transform == null) return;
        var newImageData = _transform(OriginalImageData, Value1);
        ImageData.Update(newImageData);
    }
}