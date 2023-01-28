using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorLibrary.Services.DuplicateImageServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.NegateImageServices;
using ImageProcessorLibrary.Services.OpenImageServices;
using ImageProcessorLibrary.Services.SaveImageServices;

namespace ImageProcessorLibrary.ServiceProviders;

/// <summary>
///     Serwis dostarczający usługi do obsługi obrazów.
/// </summary>
public class ImageServiceProvider : IImageServiceProvider
{
    /// <summary>
    ///     Serwis do otwierania obrazów.
    /// </summary>
    public ISelectImagesDialogService? SelectImagesDialogService { get; set; }

    /// <summary>
    ///     Serwis do otwierania obrazów.
    /// </summary>
    public IOpenImageService? OpenImageService { get; set; }

    /// <summary>
    ///     Serwis do zapisywania obrazów.
    /// </summary>
    public ISaveImageService? SaveImageService { get; set; }

    /// <summary>
    ///     Serwis do duplikacji obrazów.
    /// </summary>
    public IDuplicateImageService? DuplicateImageService { get; set; }

    /// <summary>
    ///     Serwis do otwierania okien.
    /// </summary>
    public IWindowService? WindowService { get; set; }

    /// <summary>
    ///     Serwis do wyświetlania histogramów.
    /// </summary>
    public IHistogramService? HistogramService { get; set; }

    /// <summary>
    ///     Serwis do negatywowania obrazów.
    /// </summary>
    public IImageProcessor? ImageProcessor { get; set; }

    /// <summary>
    ///     Serwis do wybierania plików obrazów.
    /// </summary>
    public ISelectImagesDialogService? SelectImagesService { get; set; }
}