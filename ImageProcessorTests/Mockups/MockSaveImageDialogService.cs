using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorTests.Mockups;

public class MockSaveImageDialogService : ISaveImageDialogService
{
    public MockSaveImageDialogService(string filename)
    {
        Filename = filename;
    }

    public string Filename { get; set; }

    public Task<string?> GetSaveImageFileName(ImageData imageData)
    {
        return Task.FromResult(Filename);
    }
}