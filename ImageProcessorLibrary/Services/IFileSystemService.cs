namespace ImageProcessorLibrary.Services;

public interface IFileSystemService
{
    Task WriteAllBytesAsync(string filename, byte[]? filebytes);
}