using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class LutServiceTests
{
    private static byte[] data;

    private static LutService lutService;

    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        // 3x3 24-bit bmp image file.
        data = new byte[]
        {
            // Header.
            // Signature.
            0x42, 0x4d,
            // File size.
            0x5a, 0x0, 0x0, 0x0,
            // Reserved.
            0x0, 0x0, 0x0, 0x0,
            // DataOffset
            0x36, 0x0, 0x0, 0x0,
            // Info header.
            // Size 
            0x28, 0x0, 0x0, 0x0,
            // Width.
            0x3, 0x0, 0x0, 0x0,
            // Height.
            0x3, 0x0, 0x0, 0x0,
            //Planes.
            0x1, 0x0,
            // Bit count (24 bit).
            0x18, 0x0,
            // Compression.
            0x0, 0x0, 0x0, 0x0,
            // Image size.
            0x24, 0x0, 0x0, 0x0,
            // X pixels per meter.
            0xc3, 0xe, 0x0, 0x0,
            // Y pixels per meter.
            0xc3, 0xe, 0x0, 0x0,
            // Colors used.
            0x0, 0x0, 0x0, 0x0,
            // Colors important.
            0x0, 0x0, 0x0, 0x0,
            // Color table
            // Dark gray.
            0xc2, 0xc3, 0xc4,
            // Light gray.
            0x7f, 0x7f, 0x7f,
            // Black.
            0x0, 0x0, 0x0,
            // 0 pixel - black
            0x0, 0x0, 0x0,
            // 1 pixel - dark gray
            0xc2, 0xc3, 0xc4,
            // 2 pixel - black
            0x0, 0x0, 0x0,
            // 3 pixel - white.
            0xff, 0xff, 0xff,
            // 4 pixel - black
            0x0, 0x0, 0x0,
            // 5 pixel - black.
            0x0, 0x0, 0x0,
            // 6 pixel - light gray,
            0x7f, 0x7f, 0x7f,
            // 7 pixel - dark gray
            0xc2, 0xc3, 0xc4,
            // 8 pixel - black
            0x0, 0x0, 0x0
        };

        lutService = new LutService();
    }

    [TestMethod]
    public void GetGrayHistogramTest()
    {
        var histogram = lutService.GetIntensityHistogram(data);

        Assert.AreEqual(3, histogram[0]);
        Assert.AreEqual(2, histogram[127]);
        Assert.AreEqual(3, histogram[195]);
        Assert.AreEqual(1, histogram[255]);

        Assert.AreEqual(9, histogram.Sum());
        Assert.AreEqual(0, histogram.Min());
    }

    [TestMethod]
    public void GetRedHistogramTest()
    {
        var histogram = lutService.GetRedHistogram(data);

        Assert.AreEqual(3, histogram[0]);
        Assert.AreEqual(2, histogram[127]);
        Assert.AreEqual(3, histogram[196]);
        Assert.AreEqual(1, histogram[255]);

        Assert.AreEqual(9, histogram.Sum());
        Assert.AreEqual(0, histogram.Min());
    }

    [TestMethod]
    public void GetGreenHistogramTest()
    {
        var histogram = lutService.GetGreenHistogram(data);

        Assert.AreEqual(3, histogram[0]);
        Assert.AreEqual(2, histogram[127]);
        Assert.AreEqual(3, histogram[195]);
        Assert.AreEqual(1, histogram[255]);

        Assert.AreEqual(9, histogram.Sum());
        Assert.AreEqual(0, histogram.Min());
    }

    [TestMethod]
    public void GetBlueHistogramTest()
    {
        var histogram = lutService.GetBlueHistogram(data);

        Assert.AreEqual(3, histogram[0]);
        Assert.AreEqual(2, histogram[127]);
        Assert.AreEqual(3, histogram[194]);
        Assert.AreEqual(1, histogram[255]);

        Assert.AreEqual(9, histogram.Sum());
        Assert.AreEqual(0, histogram.Min());
    }
}