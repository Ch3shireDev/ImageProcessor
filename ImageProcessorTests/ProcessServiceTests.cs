using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.ImageServices;

namespace ImageProcessorTests;

[TestClass]
public class ProcessServiceTests
{
    private readonly ImageProcessor processService = new();

    [TestMethod]
    public void SwapHorizontalTest()
    {
        var imageData = new ImageData(new[,]
        {
            { true, true, false },
            { false, true, false },
            { false, false, true }
        });

        var result = processService.SwapHorizontal(imageData);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(3, result.Height);

        Assert.AreEqual(false, result.GetPixelBinary(0, 0));
        Assert.AreEqual(true, result.GetPixelBinary(1, 0));
        Assert.AreEqual(true, result.GetPixelBinary(2, 0));

        Assert.AreEqual(false, result.GetPixelBinary(0, 1));
        Assert.AreEqual(true, result.GetPixelBinary(1, 1));
        Assert.AreEqual(false, result.GetPixelBinary(2, 1));

        Assert.AreEqual(true, result.GetPixelBinary(0, 2));
        Assert.AreEqual(false, result.GetPixelBinary(1, 2));
        Assert.AreEqual(false, result.GetPixelBinary(2, 2));
    }
}