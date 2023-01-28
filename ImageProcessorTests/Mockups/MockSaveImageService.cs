using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockSaveImageService : ISaveImageService
{
    public bool IsSaved { get; set; }
    public ImageData ImageData { get; set; }

    public async Task SaveImageAsync(ImageData imageData)
    {
        IsSaved = true;
        ImageData = imageData;
        await Task.Delay(1);
    }
}