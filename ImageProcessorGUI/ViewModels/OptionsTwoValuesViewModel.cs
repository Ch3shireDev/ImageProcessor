using System;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class OptionsTwoValuesViewModel<T1, T2> : OptionsOneValueViewModel<T1>
{
    private readonly Func<ImageData, T1, T2, ImageData> _transform;
    private T2? value2;

    public OptionsTwoValuesViewModel(ImageData imageData, Func<ImageData, T1, T2, ImageData> transform):base(imageData)
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
            this.RaisePropertyChanged();
        }
    }

    public override ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    private void Refresh()
    {
        var newImageData = _transform(OriginalImageData, Value1, Value2);
        ImageData.Update(newImageData);
    }
}