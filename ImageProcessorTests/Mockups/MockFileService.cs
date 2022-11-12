using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockFileService : IFileService
{
    public bool IsOpen { get; set; } = false;
    public void OpenImage()
    {
        IsOpen = true;
    }
    public void SaveImage()
    {
        throw new NotImplementedException();
    }

}