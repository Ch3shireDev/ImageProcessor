using System;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

/// <summary>
/// Klasa do obsługi opcji z dwoma wartościami.
/// </summary>
/// <typeparam name="T1"></typeparam>
/// <typeparam name="T2"></typeparam>
public class OptionsTwoValuesViewModel<T1, T2> : OptionsOneValueViewModel<T1>
{
    private readonly Func<ImageData, T1, T2, ImageData> _transform;
    private T2? value2;

    public OptionsTwoValuesViewModel(ImageData imageData, Func<ImageData, T1, T2, ImageData> transform) : base(imageData)
    {
        _transform = transform;
    }

    public T2 Value2Min { get; set; } = default!;
    public T2 Value2Max { get; set; } = default!;
    public string Value2Label { get; set; } = "";

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