using ImageProcessorLibrary.Services;
using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.ImageServices;

namespace ImageProcessorLibrary.ServiceProviders;

public interface IImageServiceProvider
{
    public IOpenImageService OpenImageService { get; }
    public ISaveImageService SaveImageService { get; }
    public IDuplicateImageService DuplicateImageService { get; }
    public IHistogramService HistogramService { get; }
    public IWindowService WindowService { get; }
    public IProcessService ProcessService { get; }
    public ISelectImagesDialogService SelectImagesService { get; }
}