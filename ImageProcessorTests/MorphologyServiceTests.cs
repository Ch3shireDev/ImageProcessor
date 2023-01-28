using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.OpenCvServices;

namespace ImageProcessorTests;

[TestClass]
public class MorphologyServiceTests
{
    private MorphologyService service;

    [TestInitialize]
    public void TestInitialize()
    {
        service = new MorphologyService();
    }

    [TestMethod]
    public void SimpleErosionTest()
    {
        var image = new ImageData(3, 3);
        var result = service.Erosion(image);
        Assert.IsTrue(result.IsEqual(image));
    }

    [TestMethod]
    public void ErosionTest()
    {
        var image = new ImageData(new byte[,]
        {
            { 200, 200, 200, 200, 200 },
            { 200, 100, 100, 200, 200 },
            { 200, 100, 100, 200, 200 },
            { 200, 100, 200, 200, 200 },
            { 200, 200, 200, 200, 200 }
        });

        var result = service.Erosion(image);

        var expected = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 200 },
            { 100, 100, 100, 100, 200 },
            { 100, 100, 100, 100, 200 },
            { 100, 100, 100, 100, 200 },
            { 100, 100, 100, 200, 200 }
        });

        Assert.IsTrue(expected.IsEqual(result));
    }

    [TestMethod]
    public void SimpleDilationTest()
    {
        var image = new ImageData(3, 5);
        var result = service.Dilation(image);
        Assert.IsTrue(result.IsEqual(image));
        Assert.AreEqual(image.Width, result.Width);
        Assert.AreEqual(image.Height, result.Height);
    }

    [TestMethod]
    public void SimpleOpeningTest()
    {
        var image = new ImageData(3, 5);
        var result = service.Opening(image);
        Assert.IsTrue(result.IsEqual(image));
        Assert.AreEqual(image.Width, result.Width);
        Assert.AreEqual(image.Height, result.Height);
    }

    [TestMethod]
    public void SimpleClosingTest()
    {
        var image = new ImageData(3, 5);
        var result = service.Closing(image);
        Assert.IsTrue(result.IsEqual(image));
        Assert.AreEqual(image.Width, result.Width);
        Assert.AreEqual(image.Height, result.Height);
    }
}