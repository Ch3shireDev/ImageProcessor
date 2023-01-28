using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.DuplicateImageServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.NegateImageServices;
using ImageProcessorLibrary.Services.OpenImageServices;
using ImageProcessorLibrary.Services.SaveImageServices;

namespace ImageProcessorLibrary.ServiceProviders;

/// <summary>
///     Interfejs dostarczający serwisy do przetwarzania obrazów.
/// </summary>
public interface IImageServiceProvider
{
    /// <summary>
    ///     Serwis do otwierania obrazów.
    /// </summary>
    public IOpenImageService OpenImageService { get; }

    /// <summary>
    ///     Serwis do zapisywania obrazów.
    /// </summary>
    public ISaveImageService SaveImageService { get; }

    /// <summary>
    ///     Serwis do duplikacji obrazów.
    /// </summary>
    public IDuplicateImageService DuplicateImageService { get; }

    /// <summary>
    ///     Serwis do wyświetlania histogramów.
    /// </summary>
    public IHistogramService HistogramService { get; }

    /// <summary>
    ///     Serwis do wyświetlania okna.
    /// </summary>
    public IWindowService WindowService { get; }

    /// <summary>
    ///     Serwis do negatywowania obrazów.
    /// </summary>
    public IImageProcessor ImageProcessor { get; }

    /// <summary>
    ///     Serwis do wyświetlania okna wyboru plików obrazów.
    /// </summary>
    public ISelectImagesDialogService SelectImagesService { get; }
}