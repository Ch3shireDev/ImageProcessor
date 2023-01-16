using System;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class OptionsTwoValuesViewModel<T1, T2> : OptionsOneValueViewModel<T1>
{
    private readonly Func<IImageData, T1, T2, IImageData> _transform;
    private T2? value2;

    public OptionsTwoValuesViewModel(ImageData imageData, Func<IImageData, T1, T2, IImageData> transform):base(imageData)
    {
        _transform = transform;
    }

    public T2 Value2Min { get; set; }
    public T2 Value2Max { get; set; }
    public string Value2Label { get; set; }

    public T2 Value2
    {
        get => value2;
        set
        {
            value2 = value;
            if (AutoRefresh) Refresh();
            this.RaisePropertyChanged();
        }
    }

    public override ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public override void Refresh()
    {
        var newImageData = _transform(OriginalImageData, Value1, Value2);
        ImageData.Update(newImageData);
    }
}