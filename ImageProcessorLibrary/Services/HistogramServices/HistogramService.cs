using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.LutServices;
using ScottPlot;

namespace ImageProcessorLibrary.Services.HistogramServices;

/// <summary>
///     Interfejs do obsługi histogramów.
/// </summary>
public class HistogramService : IHistogramService
{
    private readonly ILutService _lutService = new LutService();

    /// <summary>
    ///     Zwraca histogram RGB.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData GetRgbHistogram(ImageData imageData)
    {
        var plot = new Plot(600, 400);
        GetHistogram(imageData, plot, x => _lutService.GetIntensityHistogram(x.Filebytes), Color.Gray);
        GetHistogram(imageData, plot, x => _lutService.GetRedHistogram(x.Filebytes), Color.Red);
        GetHistogram(imageData, plot, x => _lutService.GetGreenHistogram(x.Filebytes), Color.Green);
        GetHistogram(imageData, plot, x => _lutService.GetBlueHistogram(x.Filebytes), Color.Blue);

        plot.Title("All histograms");

        var bytes = plot.GetImageBytes();

        var image = new ImageData("histogram.png", bytes);

        return image;
    }

    /// <summary>
    ///     Zwraca histogram wartości jasności.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData GetValueHistogram(ImageData imageData)
    {
        var plot = new Plot(600, 400);
        GetHistogram(imageData, plot, x => _lutService.GetIntensityHistogram(x.Filebytes), Color.Gray, "Intensity");
        var bytes = plot.GetImageBytes();

        var image = new ImageData("intensity histogram.png", bytes);

        return image;
    }

    /// <summary>
    ///     Zwraca histogram wartości czerwieni.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData GetRedHistogram(ImageData imageData)
    {
        var plot = new Plot(600, 400);
        GetHistogram(imageData, plot, x => _lutService.GetRedHistogram(x.Filebytes), Color.Red, "Red");
        var bytes = plot.GetImageBytes();

        var image = new ImageData("histogram red.png", bytes);

        return image;
    }

    /// <summary>
    ///     Zwraca histogram wartości zieleni.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData GetGreenHistogram(ImageData imageData)
    {
        var plot = new Plot(600, 400);
        GetHistogram(imageData, plot, x => _lutService.GetGreenHistogram(x.Filebytes),
            Color.Green, "Green");
        var bytes = plot.GetImageBytes();

        var image = new ImageData("histogram green.png", bytes);


        return image;
    }

    /// <summary>
    ///     Zwraca histogram wartości niebieskiego.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public ImageData GetBlueHistogram(ImageData imageData)
    {
        var plot = new Plot(600, 400);
        GetHistogram(imageData, plot, x => _lutService.GetBlueHistogram(x.Filebytes), Color.Blue, "Blue");
        var bytes = plot.GetImageBytes();

        var image = new ImageData("histogram blue.png", bytes);
        return image;
    }

    /// <summary>
    ///     Zwraca histogram wartości jasności.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="plot"></param>
    /// <param name="func"></param>
    /// <param name="color"></param>
    /// <param name="title"></param>
    private static void GetHistogram(ImageData imageData, Plot plot, Func<ImageData, int[]> func, Color color,
        string title = "")
    {
        var dataX = Enumerable.Range(0, 256).Select(x => (double)x).ToArray();
        var dataY = func(imageData).Select(x => (double)x).ToArray();
        var bar = plot.AddBar(dataY, dataX, color);
        bar.BarWidth = 1;
        plot.Title(title);
        plot.YAxis.Label("Count");
        plot.XAxis.Label("Pixel intensity");
        plot.SetAxisLimits(yMin: 0);
    }
}