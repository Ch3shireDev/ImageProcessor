using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Helpers;

namespace ImageProcessorLibrary.Services.NegateImageServices;

public class NegateImageService : INegateImageService
{
    public ImageData NegateImage(ImageData imageData)
    {
        var bitmap = new Bitmap(imageData.Width, imageData.Height);


        for (var x = 0; x < imageData.Width; x++)
        for (var y = 0; y < imageData.Height; y++)
        {
            var pixel = imageData.GetPixelRgb(x, y);
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
        var bitmap = new Bitmap(imageData.Width, imageData.Height);

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = imageData.GetPixelRgb(x, y);
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
        var bitmap = new Bitmap(imageData.Width, imageData.Height);

        for (var x = 0; x < imageData.Width / 2 + 1; x++)
        for (var y = 0; y < imageData.Height; y++)
        {
            var pixel1 = imageData.GetPixelRgb(x, y);
            var pixel2 = imageData.GetPixelRgb(imageData.Width - x - 1, y);
            bitmap.SetPixel(x, y, pixel2);
            bitmap.SetPixel(imageData.Width - x - 1, y, pixel1);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filename, stream.ToArray());
    }
}