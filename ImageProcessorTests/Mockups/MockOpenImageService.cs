using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockOpenImageService : IOpenImageService
{
    public bool IsOpen { get; set; }
    public bool IsSaved { get; set; }
    public bool IsDuplicated { get; set; }

    public async Task OpenImage()
    {
        IsOpen = true;
    }

    public void DuplicateImage(ImageData imageData)
    {
        IsDuplicated = true;
    }

    public void SaveImage(ImageData imageData)
    {
        IsSaved = true;
    }
}