using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Helpers;
using ImageProcessorLibrary.Services.Enums;

namespace ImageProcessorLibrary.Services.ImageServices;

/// <summary>
///     Klasa wykonująca operacje na obrazach.
/// </summary>
public class ImageOperationService
{
    /// <summary>
    ///     Dodaje obrazy.
    /// </summary>
    /// <param name="image1"></param>
    /// <param name="image2"></param>
    /// <param name="operation"></param>
    /// <param name="withSaturation"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public ImageData AddImages(ImageData image1, ImageData image2, ImageCombinationsEnum operation, bool withSaturation = false)
    {
        var bitmap1 = image1.Bitmap;
        var bitmap2 = image2.Bitmap;

        var width = Math.Min(bitmap1.Width, bitmap2.Width);
        var height = Math.Min(bitmap1.Height, bitmap2.Height);

        var bitmap = new Bitmap(width, height);

        var values = new double[width, height];

        double maxL = 0;

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var pixel1 = bitmap1.GetPixel(x, y);
            var pixel2 = bitmap2.GetPixel(x, y);

            var hsl1 = ColorTools.RGBToHSL(pixel1);
            var hsl2 = ColorTools.RGBToHSL(pixel2);

            double L;

            switch (operation)
            {
                case ImageCombinationsEnum.ADD_IMAGES:
                    L = hsl1.L + hsl2.L;
                    if (maxL < L) maxL = L;
                    break;
                case ImageCombinationsEnum.SUBTRACT_IMAGES:
                    L = Math.Abs(hsl1.L - hsl2.L);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(operation), operation, null);
            }

            if (L < 0) L = 0;

            values[x, y] = L;
        }


        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel1 = bitmap1.GetPixel(x, y);

                var hsl = ColorTools.RGBToHSL(pixel1);
                var light = values[x, y];

                if (withSaturation)
                {
                    if (light > 1) light = 1;
                }
                else
                {
                    if (maxL > 0 && maxL > 1) light /= maxL;
                }

                hsl.L = light;

                var pixel = ColorTools.HSLToRGB(hsl);
                bitmap.SetPixel(x, y, pixel);
            }
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        var filebytes = stream.ToArray();
        return new ImageData("result.png", filebytes);
    }
}