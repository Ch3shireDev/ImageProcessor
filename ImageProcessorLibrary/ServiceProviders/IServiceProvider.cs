using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.ServiceProviders
{
    public interface IServiceProvider
    {
        public IFileService FileService{ get; }
    }
}
