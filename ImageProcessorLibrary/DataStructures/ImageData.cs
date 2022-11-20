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

    public Bitmap Bitmap => new Bitmap(new MemoryStream(Filebytes));
    public string Filename { get; set; }
    public byte[] Filebytes { get; set; }
    public string Extension{ get; set; }
}