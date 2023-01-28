using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.ImageServices;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class DuplicateImageServiceTests
{
    private DuplicateImageService? _duplicateImageService;
    private MockWindowService? _windowService;

    [TestInitialize]
    public void TestInitialize()
    {
        _windowService = new MockWindowService();
        _duplicateImageService = new DuplicateImageService(_windowService);
    }

    [TestMethod]
    public void DuplicateImageTest()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        var imageData = new ImageData("Resources/lion.jpg", bytes);
        _duplicateImageService?.DuplicateImage(imageData);
        Assert.IsTrue(_windowService?.IsShowImageWindowCalled);
        //Assert.AreEqual(65146, _windowService.ImageData.Filebytes.Length);
        Assert.AreNotEqual(_windowService?.ImageData, imageData);
    }
}