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
    public void PrewittYOperatorTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 255, 255, 255, 255 },
            { 255, 255, 255, 255 },
            { 255, 255, 255, 255 },
            { 100, 100, 100, 100 },
            { 100, 100, 100, 100 },
            { 100, 100, 100, 100 }
        });

        var result = edgeDetectionService.PrewittEdgeDetection(imageData, prewittType: PrewittType.PREWITT_Y);

        Assert.AreEqual(4, result.Width);
        Assert.AreEqual(6, result.Height);

        Assert.AreEqual(0, result.GetGrayValue(0, 0));
        Assert.AreEqual(0, result.GetGrayValue(0, 1));
        Assert.AreEqual(255, result.GetGrayValue(0, 2));
        Assert.AreEqual(255, result.GetGrayValue(0, 3));
        Assert.AreEqual(0, result.GetGrayValue(0, 4));
        Assert.AreEqual(0, result.GetGrayValue(0, 5));
    }

    [TestMethod]
    public void PrewittXTest()
    {
        Assert.Fail();
    }
    [TestMethod]
    public void PrewittXYTest()
    {
        Assert.Fail();
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