using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.Helpers;

namespace ImageProcessorLibrary.DataStructures;

public class ImageData
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

    public ImageData(byte[,] pixels)
    {
        Pixels = ToPixels(pixels);
    }

    public ImageData(int width, int height)
    {
        Pixels = new Color[height, width];
    }

    public ImageData(string filepath, byte[]? filebytes)
    {
        Filepath = filepath;
        Filename = Path.GetFileName(filepath);
        Filebytes = filebytes;
        Extension = Path.GetExtension(Filepath);
        Pixels = ToPixels(new Bitmap(new MemoryStream(filebytes)));
    }

    public ImageData(ImageData imageData)
    {
        Filepath = imageData.Filename;
        Filename = Path.GetFileName(Filepath);
        Filebytes = imageData.Filebytes.Clone() as byte[];
        Extension = imageData.Extension;
        Pixels = ToPixels(imageData.Pixels);
    }

    public double HorizontalDPI => Bitmap.HorizontalResolution;
    public double VerticalDPI => Bitmap.VerticalResolution;

    public Bitmap Bitmap => GetBitmap();

    public Color[,] Pixels { get; set; }


    public string Filename { get; set; }

    public string Filepath { get; set; }

    public byte[]? Filebytes
    {
        get => GetFilebytes();
        set => filebytes = value;
    }

    public string Extension { get; set; }

    public int Width => Pixels.GetLength(1);

    public int Height => Pixels.GetLength(0);

    public Color this[int x, int y]
    {
        get => GetPixelRgb(y, x);
        set => SetPixel(y, x, value);
    }

    public byte this[int x, int y, int channel]
    {
        get => channel switch
        {
            0 => GetPixelRgb(y, x).R,
            1 => GetPixelRgb(y, x).G,
            2 => GetPixelRgb(y, x).B,
            _ => throw new ArgumentOutOfRangeException(nameof(channel))
        };
        set => SetPixel(y, x, channel switch
        {
            0 => Color.FromArgb(value, GetPixelRgb(y, x).G, GetPixelRgb(y, x).B),
            1 => Color.FromArgb(GetPixelRgb(y, x).R, value, GetPixelRgb(y, x).B),
            2 => Color.FromArgb(GetPixelRgb(y, x).R, GetPixelRgb(y, x).G, value),
            _ => throw new ArgumentOutOfRangeException(nameof(channel))
        });
    }

    public bool GetPixelBinary(int x, int y)
    {
        var pixel = Pixels[y, x];
        return !(pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
    }

    public event EventHandler<EventArgs>? ImageChanged;

    public void Update(ImageData result)
    {
        Filepath = result.Filepath;
        Filebytes = result.Filebytes;
        Extension = result.Extension;

        Pixels = ToPixels(new Bitmap(new MemoryStream(result.Filebytes)));

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

    public void Write(string filepath)
    {
        Filepath = filepath;
        Filename = Path.GetFileName(filepath);
        File.WriteAllBytes(filepath, Filebytes);
    }

    public bool IsEqual(ImageData imageData)
    {
        if (Width != imageData.Width || Height != imageData.Height)
        {
            return false;
        }

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                var pixel1 = GetPixelRgb(x, y);
                var pixel2 = imageData.GetPixelRgb(x, y);
                if (pixel1.R != pixel2.R) return false;
                if (pixel1.G != pixel2.G) return false;
                if (pixel1.B != pixel2.B) return false;
            }
        }

        return true;
    }

    public byte GetGrayValue(int x, int y)
    {
        var pixel = GetPixelRgb(x, y);
        var value = 0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B;
        if (value > 255) value = 255;
        if (value < 0) value = 0;
        return (byte)value;
    }

    public void Save(string filePath)
    {
        File.WriteAllBytes(filePath, Filebytes);
    }

    public byte[,] GetGrayArray()
    {
        var array = new byte[Height, Width];

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                array[y, x] = GetGrayValue(x, y);
            }
        }

        return array;
    }

    private Color[,] ToPixels(byte[,] pixels)
    {
        var colorPixels = new Color[pixels.GetLength(0), pixels.GetLength(1)];

        for (var i = 0; i < pixels.GetLength(0); i++)
        {
            for (var j = 0; j < pixels.GetLength(1); j++)
            {
                colorPixels[i, j] = Color.FromArgb(pixels[i, j], pixels[i, j], pixels[i, j]);
            }
        }

        return colorPixels;
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

    private byte[]? GetFilebytes()
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

    public static ImageData Read(string imagePath)
    {
        return new ImageData(imagePath, File.ReadAllBytes(imagePath));
    }

    public static ImageData Combine(params ImageData[] imageDataList)
    {
        var height = imageDataList[0].Height;
        var width = imageDataList[0].Width;

        var tab = new Color[height, width];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                var R = imageDataList.Max(x => x[i, j].R);
                var G = imageDataList.Max(x => x[i, j].G);
                var B = imageDataList.Max(x => x[i, j].B);
                tab[i, j] = Color.FromArgb(R, G, B);
            }
        }

        return new ImageData(tab);
    }

    public void SetGrayValue(int x, int y, double pixel)
    {
        SetPixel(x, y, Color.FromArgb((int)pixel, (int)pixel, (int)pixel));
    }
}