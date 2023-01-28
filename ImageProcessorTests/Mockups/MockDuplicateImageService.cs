using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.ImageServices;

namespace ImageProcessorTests.Mockups;

public class MockDuplicateImageService : IDuplicateImageService
{
    public bool IsDuplicated { get; set; }

    public ImageData ImageData { get; set; }

    public void DuplicateImage(ImageData imageData)
    {
        IsDuplicated = true;
        ImageData = imageData;
    }
}