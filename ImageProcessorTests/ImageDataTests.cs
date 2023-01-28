using System.Drawing;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorTests;

[TestClass]
public class ImageDataTests
{
    [TestMethod]
    public void CloneTest()
    {
        var imageData = new ImageData(new[,]
        {
            { Color.Red, Color.Green, Color.Blue },
            { Color.Cyan, Color.Magenta, Color.Yellow },
            { Color.YellowGreen, Color.Beige, Color.Chocolate }
        });

        var imageData2 = new ImageData(imageData);

        Assert.AreEqual(3, imageData2.Width);
        Assert.AreEqual(3, imageData2.Height);

        CollectionAssert.AreEqual(imageData.Filebytes, imageData2.Filebytes);

        Assert.AreEqual(Color.Red.ToArgb(), imageData2.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), imageData2.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.Blue.ToArgb(), imageData2.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Cyan.ToArgb(), imageData2.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.Magenta.ToArgb(), imageData2.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Yellow.ToArgb(), imageData2.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.YellowGreen.ToArgb(), imageData2.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Beige.ToArgb(), imageData2.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Chocolate.ToArgb(), imageData2.GetPixelRgb(2, 2).ToArgb());
    }

    [TestMethod]
    public void FileSizeTest()
    {
        var imageData = new ImageData("lion.jpg", File.ReadAllBytes("Resources/lion.jpg"));
        Assert.IsTrue(imageData?.Filebytes?.Length > 0);
    }

    [TestMethod]
    public void HorizontalResolutionTest()
    {
        var imageData = new ImageData("lion.jpg", File.ReadAllBytes("Resources/lion.jpg"));
        Assert.AreEqual(96, imageData.HorizontalDPI, 0.5);
    }

    [TestMethod]
    public void VerticalResolutionTest()
    {
        var imageData = new ImageData("lion.jpg", File.ReadAllBytes("Resources/lion.jpg"));
        Assert.AreEqual(96, imageData.VerticalDPI, 0.5);
    }

    [TestMethod]
    public void WidthHeightTest()
    {
        var x = new[,] { { true, true }, { true, true }, { true, true } };
        var imageData = new ImageData(x);
        Assert.AreEqual(2, x.GetLength(1));
        Assert.AreEqual(3, x.GetLength(0));
        Assert.AreEqual(2, imageData.Width);
        Assert.AreEqual(3, imageData.Height);
    }

    [TestMethod]
    public void BinaryTest()
    {
        var imageData = new ImageData(new[,] { { true, false }, { false, true }, { true, false } });

        Assert.AreEqual(true, imageData.GetPixelBinary(0, 0));
        Assert.AreEqual(false, imageData.GetPixelBinary(1, 0));

        Assert.AreEqual(false, imageData.GetPixelBinary(0, 1));
        Assert.AreEqual(true, imageData.GetPixelBinary(1, 1));

        Assert.AreEqual(true, imageData.GetPixelBinary(0, 2));
        Assert.AreEqual(false, imageData.GetPixelBinary(1, 2));
    }

    [TestMethod]
    public void GetImageTest()
    {
        var imageData = new ImageData(new[,] { { true, false }, { false, true }, { true, false } });

        var image = imageData.Bitmap;

        Assert.AreEqual(2, image.Size.Width);
        Assert.AreEqual(3, image.Size.Height);
    }

    [TestMethod]
    public void UpdateTest()
    {
        var imageData1 = new ImageData(new[,] { { true, false }, { false, true }, { true, false } });
        var imageData2 = new ImageData(new[,] { { false, true, true }, { true, false, false }, { false, true, false } });

        var isImageChanged = false;

        imageData1.ImageChanged += (a, b) => { isImageChanged = true; };

        Assert.AreEqual(2, imageData1.Width);
        Assert.AreEqual(3, imageData1.Height);
        Assert.AreEqual(255, imageData1.GetPixelRgb(0, 0).R);
        Assert.AreEqual(255, imageData1.GetPixelRgb(0, 0).G);
        Assert.AreEqual(255, imageData1.GetPixelRgb(0, 0).B);
        Assert.AreEqual(false, isImageChanged);

        imageData1.Update(imageData2);

        Assert.AreEqual(3, imageData1.Width);
        Assert.AreEqual(3, imageData1.Height);
        Assert.AreEqual(0, imageData1.GetPixelRgb(0, 0).R);
        Assert.AreEqual(0, imageData1.GetPixelRgb(0, 0).G);
        Assert.AreEqual(0, imageData1.GetPixelRgb(0, 0).B);
        Assert.AreEqual(true, isImageChanged);
    }
}