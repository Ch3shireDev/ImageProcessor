using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class BinaryOperationServiceTests
{
    private BinaryOperationService? service;

    [TestInitialize]
    public void TestInitialize()
    {
        service = new BinaryOperationService();
    }

    [TestMethod]
    public void DifferentShapesTest()
    {
        var image1 = new ImageData(3, 5);
        var image2 = new ImageData(5, 3);

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(3, result?.Width);
        Assert.AreEqual(5, result?.Height);
    }

    [TestMethod]
    public void ColorsAndTest()
    {
        var image1 = new ImageData(new[,]
        {
            { Color.DarkRed, Color.Green, Color.Red },
            { Color.Blue, Color.White, Color.Black },
            { Color.Black, Color.Purple, Color.Green }
        });

        Assert.AreEqual(Color.DarkRed.ToArgb(), image1.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), image1.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.Red.ToArgb(), image1.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Blue.ToArgb(), image1.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.White.ToArgb(), image1.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), image1.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), image1.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Purple.ToArgb(), image1.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), image1.GetPixelRgb(2, 2).ToArgb());

        var image2 = new ImageData(new[,]
        {
            { true, true, true },
            { false, false, false },
            { false, true, true }
        });


        var image = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(Color.DarkRed.ToArgb(), image.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), image.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.Red.ToArgb(), image.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Purple.ToArgb(), image.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), image.GetPixelRgb(2, 2).ToArgb());
    }

    [TestMethod]
    public void BinaryAndForGray()
    {
        var image1 = new ImageData(new[,] { { Color.FromArgb(0b11001100, 0, 0) } });
        var image2 = new ImageData(new[,] { { Color.FromArgb(0b10101010, 0, 0) } });

        var a = 0b11001100;
        var b = 0b10101010;

        var c = (byte)(a & b);

        Assert.AreEqual(0b10001000, c);

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual((byte)0b10001000, result?.GetPixelRgb(0, 0).R);
    }

    [TestMethod]
    public void BinaryXorForGray()
    {
        var image1 = new ImageData(new[,] { { Color.FromArgb(0b11001100, 0, 0) } });
        var image2 = new ImageData(new[,] { { Color.FromArgb(0b10101010, 0, 0) } });

        var a = 0b11001100;
        var b = 0b10101010;

        var c = (byte)(a ^ b);

        Assert.AreEqual(0b01100110, c);

        var result = service?.BinaryXor(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual((byte)0b01100110, result?.GetPixelRgb(0, 0).R);
        Assert.AreEqual((byte)255, result?.GetPixelRgb(0, 0).A);
    }

    [TestMethod]
    public void UseMaskAnd()
    {
        var image1 = new ImageData(new[,]
        {
            { Color.Fuchsia, Color.Gainsboro, Color.GhostWhite },
            { Color.Gray, Color.Cornsilk, Color.Cyan },
            { Color.DarkViolet, Color.FloralWhite, Color.Lime }
        });

        var image2 = new ImageData(new[,]
        {
            { Color.White, Color.White, Color.White },
            { Color.Black, Color.Black, Color.Black },
            { Color.White, Color.Black, Color.White }
        });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(3, result?.Width);
        Assert.AreEqual(3, result?.Height);

        Assert.AreEqual(Color.Fuchsia.ToArgb(), result?.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.Gainsboro.ToArgb(), result?.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.GhostWhite.ToArgb(), result?.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), result?.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), result?.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), result?.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.DarkViolet.ToArgb(), result?.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), result?.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Lime.ToArgb(), result?.GetPixelRgb(2, 2).ToArgb());
    }

    [TestMethod]
    public void BinaryAnd1()
    {
        var image1 = new ImageData(new[,] { { true } });
        var image2 = new ImageData(new[,] { { true } });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual(true, result?.GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void BinaryAnd2()
    {
        var image1 = new ImageData(new[,] { { true } });
        var image2 = new ImageData(new[,] { { false } });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual(false, result?.GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void BinaryAnd3()
    {
        var image1 = new ImageData(new[,] { { false } });
        var image2 = new ImageData(new[,] { { true } });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual(false, result?.GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void BinaryAnd4()
    {
        var image1 = new ImageData(new[,] { { false } });
        var image2 = new ImageData(new[,] { { false } });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(1, result?.Width);
        Assert.AreEqual(1, result?.Height);

        Assert.AreEqual(false, result?.GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void WidthHeightTest()
    {
        var x = new[,] { { true, true }, { true, true }, { true, true } };

        Assert.AreEqual(2, x.GetLength(1));
        Assert.AreEqual(3, x.GetLength(0));

        var image1 = new ImageData(new[,] { { true, true }, { true, true }, { true, true } });
        var image2 = new ImageData(new[,] { { true, true }, { true, true }, { true, true } });

        var result = service?.BinaryAnd(image1, image2);

        Assert.AreEqual(2, result?.Width);
        Assert.AreEqual(3, result?.Height);
    }

    [TestMethod]
    public void BinaryOrTest()
    {
        Assert.AreEqual(true, service?.BinaryOr(new ImageData(new[,] { { true } }), new ImageData(new[,] { { true } })).GetPixelBinary(0, 0));
        Assert.AreEqual(true, service?.BinaryOr(new ImageData(new[,] { { true } }), new ImageData(new[,] { { false } })).GetPixelBinary(0, 0));
        Assert.AreEqual(true, service?.BinaryOr(new ImageData(new[,] { { false } }), new ImageData(new[,] { { true } })).GetPixelBinary(0, 0));
        Assert.AreEqual(false, service?.BinaryOr(new ImageData(new[,] { { false } }), new ImageData(new[,] { { false } })).GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void BinaryXorTest()
    {
        Assert.AreEqual(false, service?.BinaryXor(new ImageData(new[,] { { true } }), new ImageData(new[,] { { true } })).GetPixelBinary(0, 0));
        Assert.AreEqual(true, service?.BinaryXor(new ImageData(new[,] { { true } }), new ImageData(new[,] { { false } })).GetPixelBinary(0, 0));
        Assert.AreEqual(true, service?.BinaryXor(new ImageData(new[,] { { false } }), new ImageData(new[,] { { true } })).GetPixelBinary(0, 0));
        Assert.AreEqual(false, service?.BinaryXor(new ImageData(new[,] { { false } }), new ImageData(new[,] { { false } })).GetPixelBinary(0, 0));
    }

    [TestMethod]
    public void BinaryNotTest()
    {
        Assert.AreEqual(false, service?.BinaryNot(new ImageData(new[,] { { true } })).GetPixelBinary(0, 0));
        Assert.AreEqual(true, service?.BinaryNot(new ImageData(new[,] { { false } })).GetPixelBinary(0, 0));
    }
}