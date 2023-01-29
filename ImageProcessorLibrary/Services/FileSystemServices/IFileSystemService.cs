namespace ImageProcessorLibrary.Services.FileSystemServices;

/// <summary>
///     Interfejs operacji zapisu danych do pliku.
/// </summary>
public interface IFileSystemService
{
    /// <summary>
    ///     Operacja asynchroniczna zapisu danych binarnych do pliku.
    /// </summary>
    /// <param name="filename">Nazwa pliku.</param>
    /// <param name="filebytes">Zawartość bitowa pliku.</param>
    /// <returns></returns>
    Task WriteAllBytesAsync(string filename, byte[]? filebytes);
}