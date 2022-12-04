using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public class ImageServiceProvider : IImageServiceProvider
{
    public ISelectImagesDialogService? SelectImagesDialogService { get; set; }
    public IOpenImageService? OpenImageService { get; set; }
    public ISaveImageService? SaveImageService { get; set; }
    public IDuplicateImageService? DuplicateImageService { get; set; }
    public IBinaryOperationService? BinaryOperationService { get; set; }
    public IBlurService? BlurService { get; set; }
    public IWindowService? WindowService { get; set; }
    public IStretchingOptionsService? StretchingOptionsService { get; set; }
    public IHistogramService? HistogramService { get; set; }
    public IProcessService? ProcessService { get; set; }
    public ISelectImagesDialogService? SelectImagesService { get; set; }
}