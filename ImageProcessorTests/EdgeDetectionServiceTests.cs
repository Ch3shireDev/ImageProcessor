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
            { 0, 0, 0 }
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
            { 0, 0, 0 }
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
            { 0, 0, 0 }
        });

        var result = edgeDetectionService.CannyOperatorEdgeDetection(imageData);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(4, result.Height);
    }

    [TestMethod]
    public void CannyOperatorTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 }
        });

        var result = edgeDetectionService.CannyOperatorEdgeDetection(imageData);

        Assert.AreEqual(10, result.Width);
        Assert.AreEqual(12, result.Height);

        for (var y = 0; y < result.Height; y++)
        {
            for (var x = 0; x < result.Width; x++)
            {
                Console.Write($"{result.GetPixelRgb(x, y).R}, ");
            }

            Console.WriteLine();
        }

        result.Write("a.png");
    }

    [TestMethod]
    public void SobelOperatorTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 200, 200, 200, 200, 111, 200, 200 },
            { 200, 200, 111, 111, 111, 111, 111, 111, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 },
            { 200, 200, 200, 200, 200, 200, 200, 200, 200, 200 }
        });

        var result = edgeDetectionService.SobelEdgeDetection(imageData);

        var expected = new ImageData(new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 89, 89, 0, 0, 0, 0, 89, 89, 0 },
            { 0, 255, 178, 89, 0, 0, 89, 178, 255, 0 },
            { 0, 255, 89, 255, 0, 0, 255, 89, 255, 0 },
            { 0, 255, 0, 255, 0, 0, 255, 0, 255, 0 },
            { 0, 255, 0, 255, 0, 0, 255, 0, 255, 0 },
            { 0, 255, 89, 255, 0, 0, 255, 89, 255, 0 },
            { 0, 255, 178, 89, 0, 0, 89, 178, 255, 0 },
            { 0, 89, 89, 0, 0, 0, 0, 89, 89, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        });

        Assert.IsTrue(expected.IsEqual(result));
    }
}