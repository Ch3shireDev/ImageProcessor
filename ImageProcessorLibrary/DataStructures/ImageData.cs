using System.Drawing;
using Avalonia.Media;

namespace ImageProcessorLibrary.DataStructures;

public class ImageData
{
    public ImageData(string filepath, byte[] filebytes)
    {
        Filepath = filepath;
        Filename = Path.GetFileName(filepath);
        Filebytes = filebytes;
        Extension = Path.GetExtension(Filepath);
    }

    public ImageData(ImageData imageData)
    {
        Filepath = imageData.Filename;
        Filename = Path.GetFileName(Filepath);
        Filebytes = imageData.Filebytes.Clone() as byte[];
        Extension = imageData.Extension;
    }

    public event EventHandler<EventArgs>? ImageChanged;
    public string Filename{ get; set; }
    public IImage Bitmap => GetBitmap();
    
    public Bitmap WBitmap => new(new MemoryStream(Filebytes));

    public string Filepath { get; set; }
    public byte[] Filebytes { get; set; }
    public string Extension { get; set; }
    public double HorizontalDPI => WBitmap.HorizontalResolution;
    public double VerticalDPI => WBitmap.VerticalResolution;
    public double Width => WBitmap.Width;
    public double Height => WBitmap.Height;

    private Avalonia.Media.Imaging.Bitmap GetBitmap()
    {
        var stream = new MemoryStream(Filebytes);
        stream.Position = 0;
        return new Avalonia.Media.Imaging.Bitmap(stream);
    }

    public void Update(ImageData result)
    {
        Filepath = result.Filepath;
        Filebytes = result.Filebytes;
        Extension = result.Extension;
        ImageChanged?.Invoke(null, EventArgs.Empty);
    }
}