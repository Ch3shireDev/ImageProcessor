using ImageProcessorGUI.Services;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class StretchingServiceTests
{
    private StretchingOptionsService _stretchingOptionsService;
    private MockWindowService windowService;

    [TestInitialize]
    public void TestInitialize()
    {
        windowService = new MockWindowService();
        _stretchingOptionsService = new StretchingOptionsService(windowService);
    }

    [TestMethod]
    public void EqualizeHistogramTest()
    {
        var filebytes = File.ReadAllBytes("Resources/lion.jpg");
        var imageData = new ImageData("lion.jpg", filebytes);
        var result = _stretchingOptionsService.GetEqualizedImage(imageData);
        Assert.AreEqual(imageData.Width, result.Width);
        Assert.AreEqual(imageData.Height, result.Height);
    }
}