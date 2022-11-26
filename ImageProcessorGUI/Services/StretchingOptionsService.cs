using System;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class StretchingOptionsService : IStretchingOptionsService
{
    private readonly IWindowService _windowService;
    
    public StretchingOptionsService(IWindowService windowService)
    {
        _windowService = windowService;
    }

    public void ShowLinearStretchingWindow(ImageData imageData)
    {
        var optionWindow = new LinearStretchingOptionsWindow
        {
            DataContext = new LinearStretchingViewModel(imageData)
        };

        optionWindow.Show();
    }

    public void ShowGammaStretchingWindow(ImageData imageData)
    {
        var optionWindow = new GammaStretchingOptionsWindow
        {
            DataContext = new  GammaStretchingViewModel(imageData)
        };

        optionWindow.Show();
    }

    public ImageData GetEqualizedImage(ImageData imageData)
    {
        var stretchingService = new StretchingService();
        return stretchingService.EqualizeStretching(imageData);
    }
}