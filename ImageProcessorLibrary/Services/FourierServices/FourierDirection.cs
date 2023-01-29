namespace ImageProcessorLibrary.Services.FourierServices;

/// <summary>
///     Kierunek transformacji Fouriera.
/// </summary>
public enum FourierDirection
{
    /// <summary>
    ///     Transformacja Fouriera w przód.
    /// </summary>
    Forward = 1,

    /// <summary>
    ///     Odwrotna transformacja Fouriera.
    /// </summary>
    Backward = -1
}