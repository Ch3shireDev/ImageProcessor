using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class SobelEdgeDetectionViewModel:ReactiveObject
{
    private readonly EdgeDetectionService service = new();

    private readonly ImageData ImageData;
    private readonly ImageData OriginalImageData;

    public SobelEdgeDetectionViewModel(ImageData imageData)
    {
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public List<SobelEdgeType> EdgeTypes { get; set; } = new()
    {
        SobelEdgeType.ALL,
        SobelEdgeType.EAST,
        //SobelEdgeType.NORTH_EAST,
        //SobelEdgeType.NORTH,
        //SobelEdgeType.NORTH_WEST,
        //SobelEdgeType.WEST,
        //SobelEdgeType.SOUTH_WEST,
        SobelEdgeType.SOUTH,
        SobelEdgeType.SOUTH_EAST
    };

    public SobelEdgeType SelectedEdgeType { get; set; }

    public ICommand RefreshCommand => ReactiveCommand.Create(Refresh);

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

    private string errorMessage;

    public string ErrorMessage
    {
        get => errorMessage;
        set
        {
            errorMessage = value;
            this.RaisePropertyChanged();
        }
    }
}