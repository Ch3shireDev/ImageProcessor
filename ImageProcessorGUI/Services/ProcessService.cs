using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Services;

public class ProcessService : IProcessService
{
    public ImageData NegateImage(ImageData imageData)
    {
        var bitmap = imageData.WBitmap;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var r = 255 - pixel.R;
            var g = 255 - pixel.G;
            var b = 255 - pixel.B;

            bitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filename, stream.ToArray());
    }

    public void OpenBinaryThresholdWindow(ImageData imageData)
    {
        var window = new BinaryThresholdOptionsWindow
        {
            DataContext = new BinaryThresholdViewModel(imageData)
        };

        window.Show();
    }

    public void OpenGreyscaleThresholdOneSliderWindow(ImageData imageData)
    {
        var window = new GreyscaleThresholdOneSliderWindow
        {
            DataContext = new GreyscaleThresholdOneSliderViewModel(imageData)
        };

        window.Show();
    }

    public void OpenGreyscaleThresholdTwoSlidersWindow(ImageData imageData)
    {
        var window = new GreyscaleThresholdTwoSlidersWindow
        {
            DataContext = new GreyscaleThresholdTwoSlidersViewModel(imageData)
        };

        window.Show();
    }
}