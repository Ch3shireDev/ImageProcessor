using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.DuplicateImageServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.NegateImageServices;
using ImageProcessorLibrary.Services.OpenImageServices;
using ImageProcessorLibrary.Services.SaveImageServices;
using ImageProcessorLibrary.Services.StretchingServices;

namespace ImageProcessorLibrary.ServiceProviders;

public class ImageServiceProvider : IImageServiceProvider
{
    public ISelectImagesDialogService? SelectImagesDialogService { get; set; }
    public IStretchingOptionsService? StretchingOptionsService { get; set; }
    public IOpenImageService? OpenImageService { get; set; }
    public ISaveImageService? SaveImageService { get; set; }
    public IDuplicateImageService? DuplicateImageService { get; set; }
    public IWindowService? WindowService { get; set; }
    public IHistogramService? HistogramService { get; set; }
    public INegateImageService? NegateImageService { get; set; }
    public ISelectImagesDialogService? SelectImagesService { get; set; }
}