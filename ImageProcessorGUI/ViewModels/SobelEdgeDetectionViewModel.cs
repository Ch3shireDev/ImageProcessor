using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.Enums;
using ImageProcessorLibrary.Services.OpenCvServices;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

/// <summary>
///     Model widoku dla detekcji krawędzi metodą Sobela.
/// </summary>
public class SobelEdgeDetectionViewModel : ReactiveObject
{
    private readonly ImageData ImageData;
    private readonly ImageData OriginalImageData;
    private readonly EdgeDetectionService service = new();

    private string errorMessage = "";

    public SobelEdgeDetectionViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public string Label { get; set; } = "Detekcja krawędzi metodą Sobela";

    public List<SobelEdgeType> EdgeTypes { get; set; } = new()
    {
        SobelEdgeType.ALL,
        SobelEdgeType.EAST,
        SobelEdgeType.SOUTH,
        SobelEdgeType.SOUTH_EAST
    };

    public SobelEdgeType SelectedEdgeType { get; set; }

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
            var result = service.SobelEdgeDetection(OriginalImageData, SelectedEdgeType);
            ImageData.Update(result);
            ErrorMessage = "";
        }
        catch (Exception e)
        {
            ErrorMessage = $"Wystąpił błąd transformacji: {e.Message}";
        }
    }
}