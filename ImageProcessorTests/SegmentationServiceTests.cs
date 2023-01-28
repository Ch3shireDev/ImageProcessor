using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.OpenCvServices;

namespace ImageProcessorTests;

[TestClass]
public class SegmentationServiceTests
{
    private SegmentationService segmentationService;

    [TestInitialize]
    public void TestInitialize()
    {
        segmentationService = new SegmentationService();
    }

    [TestMethod]
    public void OtsuSegmentationBasicTest()
    {
        var image = new ImageData(new byte[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 }
        });

        var result = segmentationService.OtsuSegmentation(image);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(2, result.Height);
    }

    [TestMethod]
    public void OtsuSegmentationTest()
    {
        var image = new ImageData(new byte[,]
        {
            { 255, 255, 100 },
            { 100, 100, 255 },
            { 100, 100, 255 }
        });

        var result = segmentationService.OtsuSegmentation(image);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(3, result.Height);

        Assert.AreEqual(29, result.GetGrayValue(0, 2));
    }

    [TestMethod]
    public void AdaptiveThresholdingBasicTest()
    {
        var image = new ImageData(new byte[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 }
        });

        var result = segmentationService.OtsuSegmentation(image);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(2, result.Height);
    }

    [TestMethod]
    public void AdaptiveThresholdingTest()
    {
        var image = new ImageData(new byte[,]
        {
            { 255, 255, 100 },
            { 100, 100, 255 },
            { 100, 100, 255 }
        });

        var result = segmentationService.AdaptiveThresholding(image);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(3, result.Height);

        Assert.AreEqual(29, result.GetGrayValue(0, 2));
    }
}