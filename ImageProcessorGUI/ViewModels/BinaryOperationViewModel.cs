using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class BinaryOperationViewModel : ReactiveObject
{
    private readonly IImageServiceProvider _serviceProvider;
    public string filepath;

    public ImageData? SelectedImage;

    public BinaryOperationViewModel()
    {
    }

    public BinaryOperationViewModel(IImageServiceProvider serviceProvider, ImageData imageData)
    {
        _serviceProvider = serviceProvider;
        ImageData = imageData;
        OriginalImageData = new ImageData(imageData);
    }

    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }

    public List<BinaryOperationType> Operations { get; set; } = new()
    {
        BinaryOperationType.BINARY_AND,
        BinaryOperationType.BINARY_OR,
        BinaryOperationType.BINARY_XOR,
        BinaryOperationType.BINARY_NOT,
        BinaryOperationType.TO_8BIT_MASK,
        BinaryOperationType.TO_BINARY_MASK
    };

    public BinaryOperationType SelectedOperation { get; set; }

    public ICommand ShowCommand => ReactiveCommand.Create(Show);

    public string Filepath
    {
        get => filepath;
        set
        {
            filepath = value;
            this.RaisePropertyChanged();
        }
    }

    public ICommand OpenFileCommand => ReactiveCommand.Create(OpenFile);

    public void Show()
    {
        var binaryOperation = new BinaryOperationService();

        switch (SelectedOperation)
        {
            case BinaryOperationType.BINARY_AND:
                if (SelectedImage == null) return;
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.BinaryAnd(ImageData, SelectedImage));
                break;
            case BinaryOperationType.BINARY_OR:
                if (SelectedImage == null) return;
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.BinaryOr(ImageData, SelectedImage));
                break;
            case BinaryOperationType.BINARY_XOR:
                if (SelectedImage == null) return;
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.BinaryXor(ImageData, SelectedImage));
                break;
            case BinaryOperationType.BINARY_NOT:
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.BinaryNot(ImageData));
                return;
            case BinaryOperationType.TO_8BIT_MASK:
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.To8BitMask(ImageData));
                return;
            case BinaryOperationType.TO_BINARY_MASK:
                _serviceProvider.WindowService.ShowImageWindow(binaryOperation.ToBinaryMask(ImageData));
                return;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public async Task OpenFile()
    {
        var files = await _serviceProvider.SelectImagesService.SelectImages();
        if (files.Length == 0) return;
        SelectedImage = files[0];
        Filepath = SelectedImage.Filename;
    }
}