using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public class ServiceProvider : IServiceProvider
{
    public IOpenImageService OpenImageService { get; set; }
    public ISaveImageService SaveImageService { get; set; }
    public IDuplicateImageService DuplicateImageService { get; set; }
    public IBinaryOperationService BinaryOperationService { get; set; }
    public IBlurService BlurService { get; set; }
    public ISelectImagesDialogService SelectImagesDialogService { get; set; }
    public IWindowService WindowService { get;set; }
    public IHistogramService HistogramService{ get; set; }
}