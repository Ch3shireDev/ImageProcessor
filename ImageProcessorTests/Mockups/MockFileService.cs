using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockFileService : IFileService
{
    public bool IsOpen { get; set; } = false;
    public bool IsSaved { get; set; }
    public bool IsDuplicated { get; set; }

    public void OpenImage()
    {
        IsOpen = true;
    }

    public void DuplicateImage()
    {
        IsDuplicated = true;
    }

    public void SaveImage()
    {
        IsSaved = true;
    }

}