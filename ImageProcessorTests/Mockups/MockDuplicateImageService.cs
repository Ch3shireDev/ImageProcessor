using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockDuplicateImageService : IDuplicateImageService
{
    public bool IsDuplicated { get; set; }

    public IImageData ImageData { get; set; }

    public void DuplicateImage(IImageData imageData)
    {
        IsDuplicated = true;
        ImageData = imageData;
    }
}