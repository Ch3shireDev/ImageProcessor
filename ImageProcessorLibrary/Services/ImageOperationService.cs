using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class ImageOperationService
{
    public ImageData AddImages(ImageData image1, ImageData image2, bool withSaturation = false)
    {
        var bitmap1 = image1.WBitmap;
        var bitmap2 = image2.WBitmap;

        var width = Math.Min(bitmap1.Width, bitmap2.Width);
        var height = Math.Min(bitmap1.Height, bitmap2.Height);

        var bitmap = new Bitmap(width, height);

        var hsls = new HSL[width, height];

        double maxL = 0;

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var pixel1 = bitmap1.GetPixel(x, y);
            var pixel2 = bitmap2.GetPixel(x, y);

            var hsl1 = ColorTools.RGBToHSL(pixel1);
            var hsl2 = ColorTools.RGBToHSL(pixel2);

            var L = hsl1.L + hsl2.L;
            if (maxL < L) maxL = L;

            var hsl = new HSL(hsl1.H, hsl1.S, L);
            hsls[x, y] = hsl;
        }


        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            if (maxL > 0 && !withSaturation) hsls[x, y].L /= maxL;
            var pixel = ColorTools.HSLToRGB(hsls[x, y]);
            bitmap.SetPixel(x, y, pixel);
        }


        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        var filebytes = stream.ToArray();
        return new ImageData("result.png", filebytes);
    }
}