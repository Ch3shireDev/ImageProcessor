using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class ThresholdService
{
    public ImageData BinaryThreshold(ImageData imageData, int thresholdValue)
    {
        var bitmap = imageData.Bitmap;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);
            var intensity = hsl.L;
            hsl.L = intensity > thresholdValue / 255.0f ? 1 : 0;
            var newPixel = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, newPixel);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }

    public ImageData GreyscaleThresholdOneSlider(ImageData imageData, int thresholdValue)
    {
        var bitmap = imageData.Bitmap;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);
            var intensity = hsl.L;
            hsl.L = intensity > thresholdValue / 255.0f ? intensity : 0;
            var newPixel = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, newPixel);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }

    public IImageData GreyscaleThresholdTwoSliders(IImageData imageData, int thresholdValue1, int thresholdValue2)
    {
        var bitmap = imageData.Bitmap;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);
            var intensity = hsl.L;

            if (intensity > thresholdValue1 / 255.0f && intensity < thresholdValue2 / 255.0f) hsl.L = intensity;
            else hsl.L = 0;

            var newPixel = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, newPixel);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }
}