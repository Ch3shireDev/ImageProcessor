namespace ImageProcessorLibrary.Services;

public class FileSystemService : IFileSystemService
{
    public async Task WriteAllBytesAsync(string filename, byte[]? filebytes)
    {
        await File.WriteAllBytesAsync(filename, filebytes);
    }
}