using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services
{
    public interface IFileSystemService
    {
        Task<byte[]> ReadAllBytesAsync(string filename);
        Task WriteAllBytesAsync(string filename, byte[] filebytes);
    }
}
