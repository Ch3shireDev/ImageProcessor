﻿using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests.Mockups;

public class MockSelectImagesDialogService : ISelectImagesDialogService
{
    private readonly ImageData[] _images;


    public MockSelectImagesDialogService(params ImageData[] images)
    {
        _images = images;
    }

    public Task<ImageData[]> SelectImages()
    {
        return Task.FromResult(_images);
    }
}

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