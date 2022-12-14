using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public interface IImageServiceProvider
{
    public IOpenImageService OpenImageService { get; }
    public ISaveImageService SaveImageService { get; }
    public IDuplicateImageService DuplicateImageService { get; }
    public IBlurService BlurService { get; }
    public IHistogramService HistogramService { get; }
    public IWindowService WindowService { get; }
    public IStretchingOptionsService StretchingOptionsService { get; }
    public IProcessService ProcessService { get; }
    public ISelectImagesDialogService SelectImagesService { get; }
}