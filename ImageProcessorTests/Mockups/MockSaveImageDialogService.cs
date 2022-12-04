using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockSaveImageDialogService : ISaveImageDialogService
{
    public MockSaveImageDialogService(string filename)
    {
        Filename = filename;
    }

    public string Filename { get; set; }

    public Task<string?> GetSaveImageFileName(IImageData imageData)
    {
        return Task.FromResult(Filename);
    }
}