using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public interface IServiceProvider
{
    public IOpenImageService OpenImageService { get; }
    public ISaveImageService SaveImageService { get; }
    public IDuplicateImageService DuplicateImageService { get; }
    public IBinaryOperationService BinaryOperationService { get; }
    public IBlurService BlurService { get; }
    public IHistogramService HistogramService { get; }
    public IWindowService WindowService { get; }
    public IStretchingOptionsService StretchingOptionsService { get; }
    public IProcessService ProcessService { get; }
}