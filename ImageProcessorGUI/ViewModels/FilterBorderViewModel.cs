using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.Services;
using OpenCvSharp;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class FilterBorderViewModel : ReactiveObject
{
    public FilterBorderViewModel(IImageData imageData, IWindowService windowService, double[,] kernel, string title = "Filtr")
    {
        Kernel = kernel;
        Title = title;
        ImageData = imageData;
        _windowService = windowService;
        
        Kernel = openCvService.Normalize(kernel);
    }

    private readonly OpenCvService openCvService = new OpenCvService();
    IImageData ImageData;
    private readonly IWindowService _windowService;


    public string Title { get; set; }
    double[,] Kernel{ get; set; }

    public List<BorderTypes> BorderTypesList { get; set; } = new List<BorderTypes>
    {
      BorderTypes.Constant,
      BorderTypes.Reflect,
      BorderTypes.Wrap
    };
    public BorderTypes SelectedBorderType { get; set; }
    public int ValueN { get; set; }

    public ICommand ShowCommand => ReactiveCommand.Create(Show);

    public void Show()
    {
        var result = openCvService.Filter(ImageData, Kernel, SelectedBorderType, ValueN);
        _windowService.ShowImageWindow(result);
    }

 
}
