﻿using ImageProcessorLibrary.Services.DialogServices;

namespace ImageProcessorLibrary.Services.OpenImageServices;

public class OpenImageService : IOpenImageService
{
    private readonly ISelectImagesDialogService _selectImagesDialogService;
    private readonly IWindowService _windowService;

    public OpenImageService(ISelectImagesDialogService selectImagesDialogService, IWindowService windowService)

    {
        _selectImagesDialogService = selectImagesDialogService;
        _windowService = windowService;
    }


    public async Task OpenImage()
    {
        var images = await _selectImagesDialogService.SelectImages();
        foreach (var image in images) _windowService.ShowImageWindow(image);
    }
}