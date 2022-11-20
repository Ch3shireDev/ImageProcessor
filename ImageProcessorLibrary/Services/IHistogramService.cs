using ImageProcessorLibrary.DataStructures;
using ScottPlot;

using System.Drawing;
using Bitmap = Avalonia.Media.Imaging.Bitmap;

namespace ImageProcessorLibrary.Services;

public interface IHistogramService
{
    ImageData GetValueHistogram(ImageData imageData);
    ImageData GetRgbHistogram(ImageData imageData);
    ImageData GetRedHistogram(ImageData imageData);
    ImageData GetGreenHistogram(ImageData imageData);
    ImageData GetBlueHistogram(ImageData imageData);
}


    public class HistogramService : IHistogramService
    {
        private readonly ILutService _lutService;

        public HistogramService(ILutService lutService)
        {
            _lutService = lutService;
        }

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

        public ImageData GetValueHistogram(ImageData imageData)
        {
        
            var plot = new Plot(600, 400);
            GetHistogram(imageData, plot, x => _lutService.GetIntensityHistogram(x.Filebytes), Color.Gray, "Intensity");
            var bytes = plot.GetImageBytes();

            var image = new ImageData("intensity histogram.png", bytes);

            return image;
        }

        public ImageData GetRedHistogram(ImageData imageData)
        {
            var plot = new Plot(600, 400);
            GetHistogram(imageData, plot, x => _lutService.GetRedHistogram(x.Filebytes), Color.Red, "Red");
            var bytes = plot.GetImageBytes();

            var image = new ImageData("histogram red.png", bytes);

            return image;
        }

        public ImageData GetGreenHistogram(ImageData imageData)
        {
            var plot = new Plot(600, 400);
            GetHistogram(imageData, plot, x => _lutService.GetGreenHistogram(x.Filebytes),
                Color.Green, "Green");
            var bytes = plot.GetImageBytes();

            var image = new ImageData("histogram green.png", bytes);
        

            return image;
        }

        public ImageData GetBlueHistogram(ImageData imageData)
        {
            var plot = new Plot(600, 400);
            GetHistogram(imageData, plot, x => _lutService.GetBlueHistogram(x.Filebytes), Color.Blue, "Blue");
            var bytes = plot.GetImageBytes();

            var image = new ImageData("histogram blue.png", bytes);
            return image;
        }

        private void GetHistogram(ImageData imageData, Plot plot, Func<ImageData, int[]> func, Color color, string title="")
        {
            var dataX = Enumerable.Range(0, 256).Select(x => (double)x).ToArray();
            var dataY = func(imageData).Select(x => (double)x).ToArray();

            // display the histogram counts as a bar plot
            var bar = plot.AddBar(dataY, dataX, color);
            bar.BarWidth = 1;
            // customize the plot style
            plot.Title(title);
            plot.YAxis.Label("Count");
            plot.XAxis.Label("Pixel intensity");
            plot.SetAxisLimits(yMin: 0);
        }
    }
public interface ILutService
{
    int[] GetIntensityHistogram(byte[] image);
    int[] GetRedHistogram(byte[] image);
    int[] GetGreenHistogram(byte[] image);
    int[] GetBlueHistogram(byte[] image);
}



public class LutService : ILutService
{
    public int[] GetIntensityHistogram(byte[] image)
    {
        return GetHistogram(image, pixel => (pixel.R + pixel.G + pixel.B) / 3);
    }

    public int[] GetRedHistogram(byte[] image)
    {
        return GetHistogram(image, pixel => pixel.R);
    }
    public int[] GetGreenHistogram(byte[] image)
    {
        return GetHistogram(image, pixel => pixel.G);
    }
    public int[] GetBlueHistogram(byte[] image)
    {
        return GetHistogram(image, pixel => pixel.B);
    }

    private int[] GetHistogram(byte[] image, Func<Color, int> func)
    {
        var bitmap = new System.Drawing.Bitmap(new MemoryStream(image));
        var histogram = new int[256];

        for (var i = 0; i < bitmap.Width; i++)
        for (var j = 0; j < bitmap.Height; j++)
        {
            var pixel = bitmap.GetPixel(i, j);
            var gray = func(pixel);
            histogram[gray]++;
        }

        return histogram;
    }
}