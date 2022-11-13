using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public class ServiceProvider : IServiceProvider
{
    public IFileService FileService { get; set; }
    public IBinaryOperationService BinaryOperationService { get; set; }
    public IBlurService BlurService { get; set; }
}