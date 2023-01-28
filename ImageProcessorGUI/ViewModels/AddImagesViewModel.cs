using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services.Enums;
using ImageProcessorLibrary.Services.ImageServices;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

/// <summary>
/// Model widoku dodawania obrazów.
/// </summary>
public class AddImagesViewModel : ReactiveObject
{
    private readonly IImageServiceProvider _imageServiceProvider;
    private readonly Action<ImageData> _onApply;

    private readonly ImageOperationService imageOperationService = new();

    public AddImagesViewModel(IImageServiceProvider imageServiceProvider, ImageData imageData, Action<ImageData> onApply)
    {
        ImageData = imageData;
        _imageServiceProvider = imageServiceProvider;
        OriginalImageData = new ImageData(imageData);
        _onApply = onApply;
    }

    public List<ImageCombinationsEnum> Operations { get; set; } = new()
    {
        ImageCombinationsEnum.ADD_IMAGES,
        ImageCombinationsEnum.SUBTRACT_IMAGES
    };


    public ImageCombinationsEnum SelectedOperation { get; set; } = ImageCombinationsEnum.ADD_IMAGES;

    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }
    public ImageData? AddedImageData { get; set; }
    public string Filepath { get; set; }
    public ICommand SelectImageCommand => ReactiveCommand.CreateFromTask(SelectFile);
    public ICommand ApplyCommand => ReactiveCommand.Create(Apply);

    public bool AddWithSaturation { get; set; }

    public async Task SelectFile()
    {
        var result = await _imageServiceProvider.SelectImagesService.SelectImages();
        if (result.Length == 0) return;
        AddedImageData = result[0];
        Filepath = AddedImageData.Filepath;
        this.RaisePropertyChanged(nameof(Filepath));
    }

    public void Apply()
    {
        if (AddedImageData == null) return;
        var result = imageOperationService.AddImages(OriginalImageData, AddedImageData, SelectedOperation, AddWithSaturation);
        ImageData.Update(result);
        _onApply.Invoke(ImageData);
    }
}