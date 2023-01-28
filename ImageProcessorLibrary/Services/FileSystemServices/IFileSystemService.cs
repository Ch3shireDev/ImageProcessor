namespace ImageProcessorLibrary.Services.FileSystemServices;

/// <summary>
///     Interfejs operacji zapisu danych do pliku.
/// </summary>
public interface IFileSystemService
{
    Task WriteAllBytesAsync(string filename, byte[]? filebytes);
}