using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorTests;

[TestClass]
public class ImageDataTests
{
    private static ImageData imageData;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        imageData = new ImageData("lion.jpg", File.ReadAllBytes("Resources/lion.jpg"));
    }

    [TestMethod]
    public void FileSizeTest()
    {
        Assert.AreEqual(65146, imageData.Filebytes.Length);
    }

    [TestMethod]
    public void HorizontalResolutionTest()
    {
        Assert.AreEqual(96, imageData.HorizontalDPI);
    }

    [TestMethod]
    public void VerticalResolutionTest()
    {
        Assert.AreEqual(96, imageData.VerticalDPI);
    }
}