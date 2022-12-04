using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class MathOperationViewModel : ReactiveObject
{
    public MathOperationViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }

    public List<MathOperation> Operations { get; set; } = new()
    {
        MathOperation.Add,
        MathOperation.Subtract,
        MathOperation.Multiply,
        MathOperation.Divide
    };

    public MathOperation SelectedOperation { get; set; } = MathOperation.Add;

    public bool AddWithSaturation { get; set; }

    public ICommand ApplyCommand => ReactiveCommand.Create(Apply);

    public double Value
    {
        get => value;
        set
        {
            this.value = value;
            this.RaisePropertyChanged();
        }
    }

    private double value;

    public void Apply()
    {
        var result = new MathService().Operation(OriginalImageData, Value, SelectedOperation, AddWithSaturation);
        ImageData.Update(result);
    }
}