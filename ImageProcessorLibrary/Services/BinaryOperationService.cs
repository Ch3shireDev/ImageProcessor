using System.Drawing;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class BinaryOperationService
{
    public ImageData BinaryAnd(ImageData image1, ImageData image2)
    {
        return TwoArgumentOperation(image1, image2, (a, b) => (byte)(a & b));
    }

    public ImageData BinaryOr(ImageData image1, ImageData image2)
    {
        return TwoArgumentOperation(image1, image2, (a, b) => (byte)(a | b));
    }

    public ImageData BinaryXor(ImageData image1, ImageData image2)
    {
        return TwoArgumentOperation(image1, image2, (a, b) => (byte)(a ^ b));
    }

    private static ImageData TwoArgumentOperation(ImageData image1, ImageData image2, Func<byte, byte, byte> operation)
    {
        var width1 = image1.Width;
        var height1 = image1.Height;

        var width2 = image2.Width;
        var height2 = image2.Height;

        var width = Math.Min(width1, width2);
        var height = Math.Min(height1, height2);

        var bitmap = new ImageData(image1);
        
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var A = image1.GetPixelRgb(x, y);
                var B = image2.GetPixelRgb(x, y);
                var r = operation(A.R, B.R);
                var g = operation(A.G, B.G);
                var b = operation(A.B, B.B);
                var color = Color.FromArgb(r, g, b);
                bitmap.SetPixel(x, y, color);
            }
        }

        return bitmap;
    }


    public ImageData BinaryNot(ImageData image1)
    {
        var width = image1.Width;
        var height = image1.Height;
        var result = new bool[height, width];

        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var a = image1.GetPixelBinary(x, y);
            result[y, x] = !a;
        }

        return new ImageData(result);
    }

    public ImageData ToBinaryMask(ImageData image1)
    {
        var imageData = new ImageData(image1.Width, image1.Height);

        for (var x = 0; x < image1.Width; x++)
        {
            for (var y = 0; y < image1.Height; y++)
            {
                var hsl = image1.GetPixelHsl(x, y);
                var hsl2 = new HSL(0, 0, hsl.L > 0.5 ? 1 : 0);
                imageData.SetPixel(x, y, hsl2);
            }
        }

        return imageData;
    }

    public ImageData To8BitMask(ImageData image1)
    {
        var imageData = new ImageData(image1.Width, image1.Height);

        for (var x = 0; x < image1.Width; x++)
        {
            for (var y = 0; y < image1.Height; y++)
            {
                var hsl = image1.GetPixelHsl(x, y);
                var hsl2 = new HSL(0, 0, hsl.L);
                imageData.SetPixel(x, y, hsl2);
            }
        }

        return imageData;
    }
}