using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockFileSystemService : IFileSystemService
{
    public string Filename{ get; set; }
    public byte[] Filebytes{ get; set; }
    
    public Task<byte[]> ReadAllBytesAsync(string filename)
    {
        return Task.FromResult(Filebytes);
    }

    public async Task WriteAllBytesAsync(string filename, byte[] filebytes)
    {
        await Task.Delay(1);
        Filename = filename;
        Filebytes = filebytes;
    }
}