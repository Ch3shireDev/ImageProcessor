using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class StretchingService
{
    public ImageData LinearStretching(ImageData imageData, int Lmin, int Lmax)
    {
        var bitmap = imageData.WBitmap;

        double min = 1;
        double max = 0;

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var rgb = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(rgb);
            var intensity = hsl.L;
            if (intensity > max) max = intensity;
            if (intensity < min) min = intensity;
        }

        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(pixel);

            var oldIntensity = hsl.L;
            var newIntensity = GetNewIntensity(oldIntensity, min, max, Lmin / 255.0f, Lmax / 255.0f);
            hsl.L = newIntensity;
            var newPixel = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, newPixel);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }

    private double GetNewIntensity(double oldIntensity, double min, double max, double Lmin, double Lmax)
    {
        var newIntensity = oldIntensity;

        if (newIntensity < min) newIntensity = Lmin;
        if (newIntensity > max) newIntensity = Lmax;
        newIntensity = (newIntensity - min) * Lmax / (max - min);
        if (newIntensity > 1) return 1;
        if (newIntensity < 0) return 0;
        return newIntensity;
    }

    public ImageData GammaStretching(ImageData imageData, double gammaValue)
    {
        if (gammaValue == 0) return imageData;
        var bitmap = imageData.WBitmap;
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var rgb = bitmap.GetPixel(x, y);
            var hsl = ColorTools.RGBToHSL(rgb);
            var newLight = Math.Pow(hsl.L, 1 / gammaValue);
            hsl.L = newLight;
            var newRgb = ColorTools.HSLToRGB(hsl);
            bitmap.SetPixel(x, y, newRgb);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }

    public ImageData EqualizeStretching(ImageData imageData)
    {
        var bitmap = imageData.WBitmap;

        var red = new int[256];
        var green = new int[256];
        var blue = new int[256];
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            red[pixel.R]++;
            green[pixel.G]++;
            blue[pixel.B]++;
        }

        var allPointsCount = bitmap.Width * bitmap.Height;

        var lutRed = CalculateLUTForHistogramEqualization(red, allPointsCount);
        var lutGreen = CalculateLUTForHistogramEqualization(green, allPointsCount);
        var lutBlue = CalculateLUTForHistogramEqualization(blue, allPointsCount);
        
        for (var x = 0; x < bitmap.Width; x++)
        for (var y = 0; y < bitmap.Height; y++)
        {
            var pixel = bitmap.GetPixel(x, y);
            var newPixel = Color.FromArgb(lutRed[pixel.R], lutGreen[pixel.G], lutBlue[pixel.B]);
            bitmap.SetPixel(x, y, newPixel);
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return new ImageData(imageData.Filepath, stream.ToArray());
    }
    
    private int[] CalculateLUTForHistogramEqualization(int[] values, int allPointsCount)
    {
        var distributionFunction = GetDistributionFunction(values, allPointsCount);
        var D0 = distributionFunction[0];

        var M = GetNumberOfDifferentValues(values);

        var result = new int[256];
        for (var i = 0; i < 256; i++)
        {
            var DI = distributionFunction[i];
            result[i] = (int)((DI - D0) / (1 - D0) * (M - 1));
        }

        return result;
    }

    private static int GetNumberOfDifferentValues(int[] values)
    {
        var M = values.Distinct().Count();
        return M;
    }

    private static double[] GetDistributionFunction(int[] values, int allPointsCount)
    {
        var D = new double[256];

        double sum = 0;

        for (var i = 0; i < 256; i++)
        {
            sum += values[i] / (double)allPointsCount;
            D[i] = sum;
        }

        return D;
    }
}