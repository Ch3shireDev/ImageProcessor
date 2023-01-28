using System.Drawing;

namespace ImageProcessorLibrary.Services;

public class LutService : ILutService
{
    public int[] GetIntensityHistogram(byte[]? image)
    {
        return GetHistogram(image, pixel => (pixel.R + pixel.G + pixel.B) / 3);
    }

    public int[] GetRedHistogram(byte[]? image)
    {
        return GetHistogram(image, pixel => pixel.R);
    }

    public int[] GetGreenHistogram(byte[]? image)
    {
        return GetHistogram(image, pixel => pixel.G);
    }

    public int[] GetBlueHistogram(byte[]? image)
    {
        return GetHistogram(image, pixel => pixel.B);
    }

    private int[] GetHistogram(byte[]? image, Func<Color, int> func)
    {
        var bitmap = new Bitmap(new MemoryStream(image));
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