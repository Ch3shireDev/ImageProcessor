using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.OpenCvServices;
using OpenCvSharp;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class FilterBorderViewModel : ReactiveObject
{
    private readonly FilterService _filterService = new();
    private readonly IWindowService _windowService;
    private readonly ImageData ImageData;

    private string errorMessage;

    public FilterBorderViewModel(ImageData imageData, IWindowService windowService, double[,] kernel, string title = "Filtr")
    {
        Kernel = kernel;
        Title = title;
        ImageData = imageData;
        _windowService = windowService;
        Kernel = _filterService.Normalize(kernel);
    }


    public string Title { get; set; }
    private double[,] Kernel { get; }

    public List<BorderTypes> BorderTypesList { get; set; } = new()
    {
        BorderTypes.Constant,
        BorderTypes.Reflect,
        BorderTypes.Wrap
    };

    public BorderTypes SelectedBorderType { get; set; }
    public int ValueN { get; set; }
    public int BorderPixels { get; set; }

    public ICommand ShowCommand => ReactiveCommand.Create(Show);

    public string ErrorMessage
    {
        get => errorMessage;
        set
        {
            errorMessage = value;
            this.RaisePropertyChanged();
        }
    }

    public bool BorderBeforeTransform { get; set; }
    public bool BorderAfterTransform { get; set; }

    public void Show()
    {
        try
        {
            ErrorMessage = "";
            var inputArray = _filterService.ToMatrix(ImageData);
            if (BorderBeforeTransform) inputArray = AddBorder(inputArray);
            var outputArray = _filterService.Filter(inputArray, GetKernel(), SelectedBorderType);
            if (BorderAfterTransform) outputArray = AddBorder(outputArray);
            var result = _filterService.ToImageDataFromUC3(outputArray);
            _windowService.ShowImageWindow(result);
        }
        catch (Exception e)
        {
            ErrorMessage = $"Operacja niedozwolona. Informacja o błędzie: {e.Message}";
        }
    }

    private Mat GetKernel()
    {
        var kernel = _filterService.GetKernel(Kernel);
        return kernel;
    }

    private Mat AddBorder(Mat inputArray)
    {
        return _filterService.AddBorder(inputArray, SelectedBorderType, BorderPixels, GetScalar());
    }

    private Scalar GetScalar()
    {
        return new Scalar(ValueN, ValueN, ValueN);
    }
}