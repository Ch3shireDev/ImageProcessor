using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class EdgeDetectionServiceTests
{
    private EdgeDetectionService edgeDetectionService;

    [TestInitialize]
    public void TestInitialize()
    {
        edgeDetectionService = new EdgeDetectionService();
    }

    [TestMethod]
    public void SobelOperatorSimpleTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
        });

        var result = edgeDetectionService.SobelEdgeDetection(imageData);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(4, result.Height);
    }
    [TestMethod]
    public void PrewittOperatorSimpleTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
        });

        var result = edgeDetectionService.PrewittEdgeDetection(imageData);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(4, result.Height);
    }
    [TestMethod]
    public void CannyOperatorSimpleTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
            { 0, 0, 0 },
        });

        var result = edgeDetectionService.CannyOperatorEdgeDetection(imageData);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(4, result.Height);
    }

    [TestMethod]
    public void SobelOperatorTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200, },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200, },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200, },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200, },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200, },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200, },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200, },
        });

        var result = edgeDetectionService.SobelEdgeDetection(imageData);

        File.WriteAllBytes("a.png", imageData.Filebytes);
        File.WriteAllBytes("b.png", result.Filebytes);
    }
}