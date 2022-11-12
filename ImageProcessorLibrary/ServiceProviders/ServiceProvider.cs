using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders;

public class ServiceProvider : IServiceProvider
{
    public IFileService FileService { get; set; }
}