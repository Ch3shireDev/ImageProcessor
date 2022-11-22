using Avalonia.Media.Imaging;

namespace ImageProcessorLibrary.DataStructures;

public class ImageData
{
    public ImageData(string filename, byte[] filebytes)
    {
        Filename = filename;
        Filebytes = filebytes;
        Extension = Path.GetExtension(Filename);
    }

    public ImageData(ImageData imageData)
    {
        Filename = imageData.Filename;
        Filebytes = imageData.Filebytes.Clone() as byte[];
        Extension = imageData.Extension;
    }


    public Bitmap Bitmap => GetBitmap();

    private System.Drawing.Bitmap WBitmap => new(new MemoryStream(Filebytes));

    public string Filename { get; set; }
    public byte[] Filebytes { get; set; }
    public string Extension { get; set; }
    public double HorizontalDPI => WBitmap.HorizontalResolution;
    public double VerticalDPI => WBitmap.VerticalResolution;
    public double Width => WBitmap.Width;
    public double Height => WBitmap.Height;

    private Bitmap GetBitmap()
    {
        var stream = new MemoryStream(Filebytes);
        stream.Position = 0;
        return new Bitmap(stream);
    }
}