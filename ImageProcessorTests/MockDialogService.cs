using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

public class MockDialogService : IDialogService
{
    private readonly ImageData[] _images;

    public MockDialogService(params ImageData[] images)
    {
        _images = images;
    }

    public Task<ImageData[]> SelectImages()
    {
        return Task.FromResult(_images);
    }
}