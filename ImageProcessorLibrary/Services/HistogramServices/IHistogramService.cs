using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.HistogramServices;

/// <summary>
///     Interfejs serwisu do obliczania histogramów.
/// </summary>
public interface IHistogramService
{
    /// <summary>
    ///     Oblicza histogram jasności pikseli.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData GetValueHistogram(ImageData imageData);

    /// <summary>
    ///     Oblicza histogramy wartości pikseli w trzech kolorach.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData GetRgbHistogram(ImageData imageData);

    /// <summary>
    ///     Oblicza histogram wartości pikseli w kolorze czerwonym.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData GetRedHistogram(ImageData imageData);

    /// <summary>
    ///     Oblicza histogram wartości pikseli w kolorze zielonym.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData GetGreenHistogram(ImageData imageData);

    /// <summary>
    ///     Oblicza histogram wartości pikseli w kolorze niebieskim.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    ImageData GetBlueHistogram(ImageData imageData);
}