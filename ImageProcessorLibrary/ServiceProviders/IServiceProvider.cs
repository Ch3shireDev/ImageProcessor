using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public interface IServiceProvider
{
    public IFileService FileService { get; }
    
    public IBinaryOperationService BinaryOperationService{ get; }
    public IBlurService BlurService { get; }
}