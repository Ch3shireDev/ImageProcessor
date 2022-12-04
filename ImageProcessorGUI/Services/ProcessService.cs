using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

    public ImageData ToGrayscale(ImageData imageData)
    {
        var bitmap = imageData.WBitmap;
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);
            hsl.S = 0;
            var pixel2 = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, pixel2);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filename, stream.ToArray());
    }

    public ImageData SwapHorizontal(ImageData imageData)
    {
        var bitmap = imageData.WBitmap;
        for (var x = 0; x < bitmap.Width / 2; x++)
            for (var y = 0; y < bitmap.Height; y++)
            {
                var pixel1 = bitmap.GetPixel(x, y);
                var pixel2 = bitmap.GetPixel(bitmap.Width - x - 1, y);
                bitmap.SetPixel(x, y, pixel2);
                bitmap.SetPixel(bitmap.Width - x - 1, y, pixel1);
            }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filename, stream.ToArray());
    }
}