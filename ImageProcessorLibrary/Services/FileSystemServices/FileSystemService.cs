namespace ImageProcessorLibrary.Services.FileSystemServices;

/// <summary>
///     Serwis zapisu danych do pliku.
/// </summary>
public class FileSystemService : IFileSystemService
{
    /// <summary>
    ///     Zapisuje dane do pliku.
    /// </summary>
    /// <param name="filename"></param>
    /// <param name="filebytes"></param>
    /// <returns></returns>
    public async Task WriteAllBytesAsync(string filename, byte[]? filebytes)
    {
        if (filebytes == null) return;
        await File.WriteAllBytesAsync(filename, filebytes);
    }
}