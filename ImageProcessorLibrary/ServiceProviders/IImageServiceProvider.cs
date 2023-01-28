using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.DuplicateImageServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.NegateImageServices;
using ImageProcessorLibrary.Services.OpenImageServices;
using ImageProcessorLibrary.Services.SaveImageServices;

namespace ImageProcessorLibrary.ServiceProviders;

public interface IImageServiceProvider
{
    public IOpenImageService OpenImageService { get; }
    public ISaveImageService SaveImageService { get; }
    public IDuplicateImageService DuplicateImageService { get; }
    public IHistogramService HistogramService { get; }
    public IWindowService WindowService { get; }
    public INegateImageService NegateImageService { get; }
    public ISelectImagesDialogService SelectImagesService { get; }
}