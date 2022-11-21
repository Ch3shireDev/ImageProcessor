using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class OpenImageServiceTests
{
    private MockSelectImagesDialogService _selectImagesDialogService;
    private OpenImageService _openImageService;
    private ImageData ImageData;
    private MockWindowService windowService;

    [TestInitialize]
    public void TestInitialize()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        ImageData = new ImageData("Resources/lion.jpg", bytes);
        _selectImagesDialogService = new MockSelectImagesDialogService(ImageData);
        windowService = new MockWindowService();
        _openImageService = new OpenImageService(_selectImagesDialogService, windowService);
    }

    /// <summary>
    ///     Wywołanie metody OpenImage związane jest z wybraniem pliku przez otwarcie okna dialogowego z SelectImagesDialogService, a
    ///     następnie otwarciem okna z wybranym obrazem.
    /// </summary>
    /// <returns></returns>
    [TestMethod]
    public async Task OpenImageTest()
    {
        Assert.IsFalse(windowService.IsShowImageWindowCalled);
        Assert.IsNull(windowService.ImageData);
        await _openImageService.OpenImage();
        Assert.IsTrue(windowService.IsShowImageWindowCalled);
        Assert.AreEqual(65146, windowService.ImageData.Filebytes.Length);
    }
}