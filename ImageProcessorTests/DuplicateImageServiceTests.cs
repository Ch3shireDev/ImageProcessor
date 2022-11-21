using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class DuplicateImageServiceTests
{
    private MockWindowService WindowService;
    private DuplicateImageService _duplicateImageService;
    [TestInitialize]
    public void TestInitialize()
    {
        WindowService = new MockWindowService();
        _duplicateImageService = new DuplicateImageService(WindowService);
    }

    [TestMethod]
    public void DuplicateImageTest()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        var imageData = new ImageData("Resources/lion.jpg", bytes);
        _duplicateImageService.DuplicateImage(imageData);
        Assert.IsTrue(WindowService.IsShowImageWindowCalled);
        Assert.AreEqual(65146, WindowService.ImageData.Filebytes.Length);
        Assert.AreNotEqual(WindowService.ImageData, imageData);
    }
}