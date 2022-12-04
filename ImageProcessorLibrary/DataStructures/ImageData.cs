using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.Services;

namespace ImageProcessorLibrary.DataStructures;

public class ImageData : IImageData
{
    private byte[]? filebytes;

    public ImageData(bool[,] binaryPixels)
    {
        Pixels = ToPixels(binaryPixels);
    }

    public ImageData(Color[,] pixels)
    {
        Pixels = pixels;
    }

    public ImageData(int width, int height)
    {
        Pixels = new Color[height, width];
    }

    public ImageData(string filepath, byte[] filebytes)
    {
        Filepath = filepath;
        Filename = Path.GetFileName(filepath);
        Filebytes = filebytes;
        Extension = Path.GetExtension(Filepath);
        Pixels = ToPixels(new Bitmap(new MemoryStream(filebytes)));
    }

    public ImageData(IImageData imageData)
    {
        Filepath = imageData.Filename;
        Filename = Path.GetFileName(Filepath);
        Filebytes = imageData.Filebytes.Clone() as byte[];
        Extension = imageData.Extension;
        Pixels = ToPixels(imageData.Pixels);
    }

    public Bitmap Bitmap => GetBitmap();
    public double HorizontalDPI => Bitmap.HorizontalResolution;
    public double VerticalDPI => Bitmap.VerticalResolution;

    public Color[,] Pixels { get; set; }


    public string Filename { get; set; }

    public string Filepath { get; set; }

    public byte[] Filebytes
    {
        get => GetFilebytes();
        set => filebytes = value;
    }

    public string Extension { get; set; }

    public bool GetPixelBinary(int x, int y)
    {
        var pixel = Pixels[y, x];
        return !(pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
    }

    public int Width => Pixels.GetLength(1);

    public int Height => Pixels.GetLength(0);

    public event EventHandler<EventArgs>? ImageChanged;

    public void Update(IImageData result)
    {
        Filepath = result.Filepath;
        Filebytes = result.Filebytes;
        Extension = result.Extension;

        Pixels = ToPixels(new Bitmap(new MemoryStream( result.Filebytes)));

        ImageChanged?.Invoke(null, EventArgs.Empty);
    }

    public Color GetPixelRgb(int x, int y)
    {
        return Pixels[y, x];
    }

    public HSL GetPixelHsl(int x, int y)
    {
        return ColorTools.RGBToHSL(Pixels[y, x]);
    }

    public void SetPixel(int x, int y, Color pixel)
    {
        Pixels[y, x] = pixel;
    }

    public void SetPixel(int x, int y, HSL pixel)
    {
        Pixels[y, x] = ColorTools.HSLToRGB(pixel);
    }

    private Bitmap GetBitmap()
    {
        return new Bitmap(new MemoryStream(Filebytes));
    }

    private Color[,] ToPixels(bool[,] bools)
    {
        var height = bools.GetLength(0);
        var width = bools.GetLength(1);
        var pixels = new Color[height, width];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                pixels[y, x] = bools[y, x] ? Color.White : Color.Black;
            }
        }

        return pixels;
    }

    private Color[,] ToPixels(Bitmap bitmap)
    {
        var width = bitmap.Width;
        var height = bitmap.Height;
        var pixels = new Color[height, width];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                pixels[y, x] = bitmap.GetPixel(x, y);
            }
        }

        return pixels;
    }

    private Color[,] ToPixels(Color[,] bitmap)
    {
        var width = bitmap.GetLength(1);
        var height = bitmap.GetLength(0);
        var pixels = new Color[height, width];

        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                pixels[y, x] = bitmap[y, x];
            }
        }

        return pixels;
    }

    private byte[] GetFilebytes()
    {
        var bitmap = new Bitmap(Width, Height);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                bitmap.SetPixel(x, y, GetPixelRgb(x, y));
            }
        }

        var stream = new MemoryStream();
        bitmap.Save(stream, ImageFormat.Png);
        return stream.ToArray();
    }
}