using System.Drawing;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public interface IImageData
{
    int Width { get; }
    int Height { get; }
    byte[] Filebytes { get; set; }
    bool GetPixelBinary(int x, int y);
    event EventHandler<EventArgs>? ImageChanged;
    string Filepath { get; }
    string Filename { get; }
    string Extension{ get; }
    void Update(IImageData result);
    Color GetPixelRgb(int x, int y);
    HSL GetPixelHsl(int x, int y);
    void SetPixel(int x, int y, Color color);
    Color[,] Pixels { get; }
}