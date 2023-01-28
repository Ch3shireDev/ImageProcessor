using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorTests.Mockups;

public class MockSelectImagesDialogService : ISelectImagesDialogService
{
    private readonly ImageData[] _images;


    public MockSelectImagesDialogService(params ImageData[] images)
    {
        _images = images;
    }

    public Task<ImageData[]> SelectImages(bool allowMultiple = false)
    {
        if (!allowMultiple) return Task.FromResult(new[] { _images.First() });
        return Task.FromResult(_images);
    }
}