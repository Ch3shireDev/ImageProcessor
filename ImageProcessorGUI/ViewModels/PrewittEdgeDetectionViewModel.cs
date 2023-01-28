using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ImageProcessorLibrary.Services.Enums;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class PrewittEdgeDetectionViewModel : ReactiveObject
{
    private readonly ImageData ImageData;
    private readonly ImageData OriginalImageData;
    private readonly EdgeDetectionService service = new();

    private string errorMessage = "";

    public PrewittEdgeDetectionViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public string Label { get; set; } = "Detekcja krawędzi metodą Prewitta";

    public List<PrewittType> EdgeTypes { get; set; } = new()
    {
        PrewittType.PREWITT_XY,
        PrewittType.PREWITT_X,
        PrewittType.PREWITT_Y
    };

    public PrewittType SelectedEdgeType { get; set; }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

    public string ErrorMessage
    {
        get => errorMessage;
        set
        {
            errorMessage = value;
            this.RaisePropertyChanged();
        }
    }

    public void Refresh()
    {
        try
        {
            var result = service.PrewittEdgeDetection(OriginalImageData, SelectedEdgeType);
            ImageData.Update(result);
            ErrorMessage = "";
        }
        catch (Exception e)
        {
            ErrorMessage = $"Wystąpił błąd transformacji: {e.Message}";
        }
    }
}