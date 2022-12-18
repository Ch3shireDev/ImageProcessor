using System.Drawing;

namespace ImageProcessorLibrary.Services;

public interface IImageData
{
    int Width { get; }
    int Height { get; }
    byte[] Filebytes { get; set; }
    string Filepath { get; }
    string Filename { get; }
    string Extension { get; }
    Color[,] Pixels { get; }
    bool GetPixelBinary(int x, int y);
    event EventHandler<EventArgs>? ImageChanged;
    void Update(IImageData result);
    Color GetPixelRgb(int x, int y);
    HSL GetPixelHsl(int x, int y);
    void SetPixel(int x, int y, Color color);
    void Write(string filepath);
    bool IsEqual(IImageData image);
    byte GetGrayValue(int x, int y);

    Color this[int x, int y] { get; set; }
    byte this[int x, int y, int channel] { get; set; }
}