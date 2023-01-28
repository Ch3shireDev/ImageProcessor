using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Helpers;
using ImageProcessorLibrary.Services.Enums;

namespace ImageProcessorLibrary.Services.ImageServices;

/// <summary>
///     Serwis do przeprowadzania operacji matematycznych.
/// </summary>
public class MathService
{
    /// <summary>
    ///     Operacja matematyczna.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="value"></param>
    /// <param name="operation"></param>
    /// <param name="withSaturation"></param>
    /// <returns></returns>
    public ImageData Operation(ImageData imageData, double value, MathOperation operation, bool withSaturation)
    {
        var bitmap = imageData.Bitmap;

        var width = imageData.Width;
        var height = imageData.Height;

        var values = new double[width, height];
        double max = 0;

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);

            switch (operation)
            {
                case MathOperation.Add:
                    values[x, y] = (hsl.L * 255 + value) / 255.0;
                    break;
                case MathOperation.Subtract:
                    values[x, y] = (int)(hsl.L * 255 - value) / 255.0;
                    break;
                case MathOperation.Multiply:
                    values[x, y] = (int)(hsl.L * 255 * value) / 255.0;
                    break;
                case MathOperation.Divide:
                    values[x, y] = (int)(hsl.L * 255 / value) / 255.0;
                    break;
            }

            if (max < values[x, y]) max = values[x, y];
        }

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl2 = ColorTools.RGBToHSL(pixel);
            var light = values[x, y];
            if (light < 0) light = 0;

            if (withSaturation)
            {
                if (light > 1.0) light = 1.0;
                if (light < 0) light = 0;
                bitmap.SetPixel(x, y, ColorTools.HSLToRGB(new HSL(hsl2.H, hsl2.S, light)));
            }
            else
            {
                if (max > 255) light /= max * 1.0;

                if (light > 1.0) light = 1.0;
                if (light < 0) light = 0;
                bitmap.SetPixel(x, y, ColorTools.HSLToRGB(new HSL(hsl2.H, hsl2.S, light)));
            }
        }


        var memoryStream = new MemoryStream();
        bitmap.Save(memoryStream, ImageFormat.Png);
        return new ImageData("result.png", memoryStream.ToArray());
    }
}