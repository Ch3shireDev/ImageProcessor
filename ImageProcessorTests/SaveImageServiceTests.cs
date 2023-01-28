using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.DialogServices;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class SaveImageServiceTests
{
    private MockSaveImageDialogService _saveImageDialogService;
    private SaveImageService _saveImageService;
    private MockFileSystemService fileSystemService;
    private ImageData ImageData;

    [TestInitialize]
    public void TestInitialize()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        ImageData = new ImageData("Resources/lion.jpg", bytes);
        _saveImageDialogService = new MockSaveImageDialogService("lion2.jpg");
        fileSystemService = new MockFileSystemService();
        _saveImageService = new SaveImageService(_saveImageDialogService, fileSystemService);
    }

    /// <summary>
    ///     Proces zapisu obrazu powinien zacząć się od wybrania ścieżki przez otwarcie okna dialogowego z
    ///     SelectImagesDialogService, a następnie zapisaniem tego obrazu pod wybraną ścieżką.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task SaveImageTest()
    {
        Assert.AreEqual(null, fileSystemService.Filename);
        Assert.AreEqual(null, fileSystemService.Filebytes);
        await _saveImageService.SaveImageAsync(ImageData);
        Assert.AreEqual("lion2.jpg", fileSystemService.Filename);
        Assert.IsTrue(fileSystemService.Filebytes.Length > 0);
    }
}