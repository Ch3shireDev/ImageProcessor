﻿using System.Drawing;
using System.Drawing.Imaging;
using ImageProcessorLibrary.Helpers;

namespace ImageProcessorLibrary.DataStructures;

/// <summary>
///     Klasa przechowująca dane obrazu.
/// </summary>
public class ImageData
{
    /// <summary>
    ///     Konstruktor klasy ImageData.
    /// </summary>
    /// <param name="binaryPixels">
    ///     Piksel w postaci binarnej:
    ///     1 - biały piksel,
    ///     0 - czarny piksel.
    /// </param>
    public ImageData(bool[,] binaryPixels)
    {
        Pixels = ToPixels(binaryPixels);
    }

    /// <summary>
    ///     Konstruktor klasy ImageData.
    /// </summary>
    /// <param name="pixels"></param>
    public ImageData(Color[,] pixels)
    {
        Pixels = pixels;
    }

    /// <summary>
    ///    Konstruktor klasy ImageData.
    /// </summary>
    /// <param name="pixels"></param>
    public ImageData(byte[,] pixels)
    {
        Pixels = ToPixels(pixels);
    }

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public ImageData(int width, int height)
    {
        Pixels = new Color[height, width];
    }

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="filepath"></param>
    /// <param name="filebytes"></param>
    public ImageData(string filepath, byte[]? filebytes)
    {
        Filepath = filepath;
        Filename = Path.GetFileName(filepath);
        Extension = Path.GetExtension(Filepath);
        Pixels = ToPixels(new Bitmap(new MemoryStream(filebytes)));
    }

    /// <summary>
    /// Konstruktor.
    /// </summary>
    /// <param name="imageData"></param>
    public ImageData(ImageData imageData)
    {
        Filepath = imageData.Filename;
        Filename = Path.GetFileName(Filepath);
        Extension = imageData.Extension;
        Pixels = ToPixels(imageData.Pixels);
    }

    /// <summary>
    /// Rozdzielczość w osi poziomej.
    /// </summary>
    public double HorizontalDPI => Bitmap.HorizontalResolution;
    /// <summary>
    /// Rozdzielczość w osi pionowej.
    /// </summary>
    public double VerticalDPI => Bitmap.VerticalResolution;

    /// <summary>
    /// Bitmapa.
    /// </summary>
    public Bitmap Bitmap => GetBitmap();

    /// <summary>
    /// Macierz pikseli.
    /// </summary>
    public Color[,] Pixels { get; set; }


    /// <summary>
    ///     Nazwa pliku.
    /// </summary>
    public string Filename { get; set; } = "untitled.png";

    /// <summary>
    ///     Ścieżka pliku.
    /// </summary>

    public string Filepath { get; set; } = "untitled.png";

    /// <summary>
    ///     Plik w postaci binarnej.
    /// </summary>

    public byte[]? Filebytes => GetFilebytes();

    /// <summary>
    ///     Rozszerzenie pliku.
    /// </summary>
    public string Extension { get; set; } = ".png";

    /// <summary>
    ///     Szerokość obrazu.
    /// </summary>

    public int Width => Pixels.GetLength(1);

    /// <summary>
    ///     Wysokość obrazu.
    /// </summary>

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

    /// <summary>
    /// Przedstawienie piksela z pozycji x,y w postaci binarną.
    /// </summary>
    /// <param name="x">Współrzędna pozioma.</param>
    /// <param name="y">Współrzędna pionowa.</param>
    /// <returns></returns>
    public bool GetPixelBinary(int x, int y)
    {
        var pixel = Pixels[y, x];
        return !(pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
    }

    /// <summary>
    /// Zdarzenie wywoływane w momencie zmiany obrazu.
    /// </summary>
    public event EventHandler<EventArgs>? ImageChanged;

    /// <summary>
    /// Aktualizacja obrazu. Wyzwala zdarzenie ImageChanged.
    /// </summary>
    /// <param name="result"></param>
    public void Update(ImageData result)
    {
        Filepath = result.Filepath;
        Extension = result.Extension;

        Pixels = ToPixels(result.Pixels);

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
        Bitmap.Save(filepath);
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

    private static Color[,] ToPixels(byte[,] pixels)
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
        var bitmap = new Bitmap(Width, Height);

        for (var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                bitmap.SetPixel(x, y, GetPixelRgb(x, y));
            }
        }

        return bitmap;
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