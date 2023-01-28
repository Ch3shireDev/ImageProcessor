using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using ImageProcessorLibrary.Services.Enums;

namespace ImageProcessorTests;

[TestClass]
public class EdgeDetectionServiceTests
{
    private EdgeDetectionService? edgeDetectionService;

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

        var result = edgeDetectionService?.SobelEdgeDetection(imageData);

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

        var result = edgeDetectionService?.PrewittEdgeDetection(imageData);

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

        var result = edgeDetectionService?.PrewittEdgeDetection(imageData, PrewittType.PREWITT_Y);

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
        var imageData = new ImageData(new byte[,]
        {
            { 255, 255, 100, 100 },
            { 255, 255, 100, 100 },
            { 255, 255, 100, 100 },
            { 255, 255, 100, 100 },
            { 255, 255, 100, 100 },
            { 255, 255, 100, 100 }
        });

        var result = edgeDetectionService?.PrewittEdgeDetection(imageData, PrewittType.PREWITT_X);

        Assert.AreEqual(4, result.Width);
        Assert.AreEqual(6, result.Height);

        Assert.AreEqual(0, result.GetGrayValue(0, 0));
        Assert.AreEqual(255, result.GetGrayValue(1, 0));
        Assert.AreEqual(255, result.GetGrayValue(2, 0));
        Assert.AreEqual(0, result.GetGrayValue(3, 0));
    }

    [TestMethod]
    public void PrewittXYTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100 },
            { 100, 255, 255, 100 },
            { 100, 255, 255, 100 },
            { 100, 255, 255, 100 },
            { 100, 255, 255, 100 },
            { 100, 100, 100, 100 }
        });

        var result = edgeDetectionService?.PrewittEdgeDetection(imageData);

        Assert.AreEqual(4, result.Width);
        Assert.AreEqual(6, result.Height);

        result.Save("image.jpg");
        Assert.AreEqual(0, result.GetGrayValue(0, 0));
        Assert.AreEqual(0, result.GetGrayValue(1, 0));
        Assert.AreEqual(255, result.GetGrayValue(2, 0));
        Assert.AreEqual(0, result.GetGrayValue(3, 0));

        Assert.AreEqual(0, result.GetGrayValue(0, 1));
        Assert.AreEqual(0, result.GetGrayValue(1, 1));
        Assert.AreEqual(255, result.GetGrayValue(2, 1));
        Assert.AreEqual(0, result.GetGrayValue(3, 1));

        Assert.AreEqual(0, result.GetGrayValue(0, 2));
        Assert.AreEqual(0, result.GetGrayValue(1, 2));
        Assert.AreEqual(255, result.GetGrayValue(2, 2));
        Assert.AreEqual(0, result.GetGrayValue(3, 2));

        Assert.AreEqual(0, result.GetGrayValue(0, 3));
        Assert.AreEqual(0, result.GetGrayValue(1, 3));
        Assert.AreEqual(255, result.GetGrayValue(2, 3));
        Assert.AreEqual(0, result.GetGrayValue(3, 3));

        Assert.AreEqual(255, result.GetGrayValue(0, 4));
        Assert.AreEqual(255, result.GetGrayValue(1, 4));
        Assert.AreEqual(255, result.GetGrayValue(2, 4));
        Assert.AreEqual(255, result.GetGrayValue(3, 4));

        Assert.AreEqual(0, result.GetGrayValue(0, 5));
        Assert.AreEqual(0, result.GetGrayValue(1, 5));
        Assert.AreEqual(255, result.GetGrayValue(2, 5));
        Assert.AreEqual(0, result.GetGrayValue(3, 5));
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

        var result = edgeDetectionService?.CannyOperatorEdgeDetection(imageData);

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

        var result = edgeDetectionService?.CannyOperatorEdgeDetection(imageData);

        Assert.AreEqual(10, result.Width);
        Assert.AreEqual(12, result.Height);

        var expected = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 255, 255, 255, 255, 255, 255, 0, 0 },
            { 0, 255, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 255, 0, 255, 255, 255, 255, 0, 255, 0 },
            { 0, 255, 0, 255, 0, 0, 255, 0, 255, 0 },
            { 0, 255, 0, 255, 0, 0, 255, 0, 255, 0 },
            { 0, 255, 0, 255, 255, 255, 255, 0, 255, 0 },
            { 0, 255, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 255, 255, 255, 255, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        };

        CollectionAssert.AreEqual(expected, result.GetGrayArray());

        var image2 = new ImageData(result.GetGrayArray());

        CollectionAssert.AreEqual(expected, image2.GetGrayArray());
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

        var result = edgeDetectionService?.SobelEdgeDetection(imageData);

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