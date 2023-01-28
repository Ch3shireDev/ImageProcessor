namespace ImageProcessorLibrary.Services;

public class FileSystemService : IFileSystemService
{
    public async Task<byte[]?> ReadAllBytesAsync(string filename)
    {
        return await File.ReadAllBytesAsync(filename);
    }

    public async Task WriteAllBytesAsync(string filename, byte[]? filebytes)
    {
        await File.WriteAllBytesAsync(filename, filebytes);
    }
}