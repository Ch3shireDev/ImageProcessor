using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.Services;
using OpenCvSharp;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class FilterBorderViewModel : ReactiveObject
{
    private readonly IWindowService _windowService;
    private readonly IImageData ImageData;

    private readonly OpenCvService openCvService = new();

    private string errorMessage;

    public FilterBorderViewModel(IImageData imageData, IWindowService windowService, double[,] kernel, string title = "Filtr")
    {
        Kernel = kernel;
        Title = title;
        ImageData = imageData;
        _windowService = windowService;
        Kernel = openCvService.Normalize(kernel);
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
            var inputArray = openCvService.ToMatrix(ImageData);
            if (BorderBeforeTransform) inputArray = AddBorder(inputArray);
            var outputArray = openCvService.Filter(inputArray, GetKernel(), SelectedBorderType);
            if (BorderAfterTransform) outputArray = AddBorder(outputArray);
            var result = openCvService.ToImageData(outputArray);
            _windowService.ShowImageWindow(result);
        }
        catch (Exception e)
        {
            ErrorMessage = $"Operacja niedozwolona. Informacja o błędzie: {e.Message}";
        }
    }

    private Mat GetKernel()
    {
        var kernel = openCvService.GetKernel(Kernel);
        return kernel;
    }

    private Mat AddBorder(Mat inputArray)
    {
        return openCvService.AddBorder(inputArray, SelectedBorderType, BorderPixels, GetScalar());
    }

    private Scalar GetScalar()
    {
        return new Scalar(ValueN, ValueN, ValueN);
    }
}
