using System;
using System.Collections.Generic;
using System.Windows.Input;
using ImageProcessorLibrary.Services;
using OpenCvSharp;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class UniversalMedianOperationViewModel : ReactiveObject
{
    private readonly IWindowService _windowService;

    private string errorMessage;

    private readonly OpenCvService openCvService = new();

    public UniversalMedianOperationViewModel(IImageData imageData, IWindowService windowService, string title = "Uniwersalna operacja medianowa")
    {
        _windowService = windowService;
        ImageData = imageData;
        Title = title;
    }

    private IImageData ImageData { get; }


    public string Title { get; set; }

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

    public List<MedianBlurType> MedianBlurTypes { get; set; } = new()
    {
        MedianBlurType.MEDIAN_BLUR_3X3,
        MedianBlurType.MEDIAN_BLUR_5X5,
        MedianBlurType.MEDIAN_BLUR_7X7,
        MedianBlurType.MEDIAN_BLUR_9X9
    };

    public MedianBlurType SelectedMedianBlurType { get; set; }


    private Mat AddBorder(Mat inputArray)
    {
        return openCvService.AddBorder(inputArray, SelectedBorderType, BorderPixels, GetScalar());
    }

    private Scalar GetScalar()
    {
        return new Scalar(ValueN, ValueN, ValueN);
    }

    public void Show()
    {
        try
        {
            ErrorMessage = "";
            var inputArray = openCvService.ToInputArray(ImageData);
            if (BorderBeforeTransform) inputArray = AddBorder(inputArray);
            var outputArray = openCvService.MedianBlur(inputArray, GetMedianBoxSize());
            if (BorderAfterTransform) outputArray = AddBorder(outputArray);
            var result = openCvService.ToImageData(outputArray);
            _windowService.ShowImageWindow(result);
        }
        catch (Exception e)
        {
            ErrorMessage = $"Operacja niedozwolona. Informacja o błędzie: {e.Message}";
        }
    }

    public int GetMedianBoxSize()
    {
        switch (SelectedMedianBlurType)
        {
            case MedianBlurType.MEDIAN_BLUR_3X3:
                return 3;
            case MedianBlurType.MEDIAN_BLUR_5X5:
                return 5;
            case MedianBlurType.MEDIAN_BLUR_7X7:
                return 7;
            case MedianBlurType.MEDIAN_BLUR_9X9:
                return 9;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum MedianBlurType
{
    MEDIAN_BLUR_3X3,
    MEDIAN_BLUR_5X5,
    MEDIAN_BLUR_7X7,
    MEDIAN_BLUR_9X9
}