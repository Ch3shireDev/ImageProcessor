using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.ImageServices;

/// <summary>
///     Interfejs serwisu do operacji na obrazie.
/// </summary>
public interface IImageProcessor
{
    /// <summary>
    ///     Metoda zwracająca obraz po zastosowaniu operacji negatywu.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData NegateImage(ImageData imageData);

    /// <summary>
    ///     Metoda zwracająca obraz po zastosowaniu operacji przerzucenia obrazu w poziomie.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData SwapHorizontal(ImageData imageData);

    /// <summary>
    ///     Metoda zwracająca obraz po zastosowaniu operacji zamiany na skalę szarości.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData ToGrayscale(ImageData imageData);
}