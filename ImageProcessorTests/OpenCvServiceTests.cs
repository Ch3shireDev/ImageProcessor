﻿using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using OpenCvSharp;

namespace ImageProcessorTests;

[TestClass]
public class OpenCvServiceTests
{
    private OpenCvService? openCvService;

    [TestInitialize]
    public void TestInitialize()
    {
        openCvService = new OpenCvService();
    }

    [TestMethod]
    public void ShapeTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 5, 3, 2 },
            { 10, 20, 30 },
            { 153, 22, 17 },
            { 153, 22, 17 }
        });

        Assert.AreEqual(3, imageData.Width);
        Assert.AreEqual(4, imageData.Height);

        var result = openCvService?.MedianBlur(imageData, 3);

        Assert.AreEqual(3, result.Width);
        Assert.AreEqual(4, result.Height);
    }

    [TestMethod]
    public void MonochromeTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 127, 127, 127 },
            { 127, 127, 127 },
            { 127, 127, 127 }
        });

        var result = openCvService?.MedianBlur(imageData, 3);

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                Assert.AreEqual(127, result.GetPixelRgb(x, y).R);
                Assert.AreEqual(127, result.GetPixelRgb(x, y).G);
                Assert.AreEqual(127, result.GetPixelRgb(x, y).B);
            }
        }
    }

    [TestMethod]
    public void OtherValueTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 220, 200, 220 },
            { 200, 255, 200 },
            { 220, 200, 220 }
        });

        var result = openCvService?.MedianBlur(imageData, 3);

        for (var x = 0; x < 3; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                Assert.AreEqual(220, result.GetPixelRgb(x, y).R);
                Assert.AreEqual(220, result.GetPixelRgb(x, y).G);
                Assert.AreEqual(220, result.GetPixelRgb(x, y).B);
            }
        }
    }

    [TestMethod]
    public void IdentityFilterTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 0, 255, 255 },
            { 255, 0, 255 },
            { 255, 255, 0 }
        });

        var kernel = new double[,]
        {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        };

        var result = openCvService?.Filter(imageData, kernel);

        Assert.AreEqual(0, result.GetPixelRgb(0, 0).R);
        Assert.AreEqual(255, result.GetPixelRgb(1, 0).R);
        Assert.AreEqual(255, result.GetPixelRgb(2, 0).R);

        Assert.AreEqual(255, result.GetPixelRgb(0, 1).R);
        Assert.AreEqual(0, result.GetPixelRgb(1, 1).R);
        Assert.AreEqual(255, result.GetPixelRgb(2, 1).R);

        Assert.AreEqual(255, result.GetPixelRgb(0, 2).R);
        Assert.AreEqual(255, result.GetPixelRgb(1, 2).R);
        Assert.AreEqual(0, result.GetPixelRgb(2, 2).R);
    }

    [TestMethod]
    public void GaussianBlurTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 200, 100, 100 },
            { 100, 200, 100 },
            { 200, 200, 200 }
        });

        var kernel = new[,]
        {
            { .1, .1, .1 },
            { .1, .1, .1 },
            { .1, .1, .1 }
        };

        var result = openCvService?.Filter(imageData, kernel);

        Assert.AreEqual(140, result.GetPixelRgb(0, 0).R);
        Assert.AreEqual(120, result.GetPixelRgb(1, 0).R);
        Assert.AreEqual(130, result.GetPixelRgb(2, 0).R);

        Assert.AreEqual(150, result.GetPixelRgb(0, 1).R);
        Assert.AreEqual(140, result.GetPixelRgb(1, 1).R);
        Assert.AreEqual(140, result.GetPixelRgb(2, 1).R);

        Assert.AreEqual(160, result.GetPixelRgb(0, 2).R);
        Assert.AreEqual(140, result.GetPixelRgb(1, 2).R);
        Assert.AreEqual(160, result.GetPixelRgb(2, 2).R);
    }

    [TestMethod]
    public void BorderTest()
    {
        var imageData = new ImageData(new byte[,]
        {
            { 9, 9, 9 },
            { 9, 9, 9 },
            { 9, 9, 9 }
        });

        var kernel = new double[,]
        {
            { 0, 0, 0 },
            { 0, 1, 0 },
            { 0, 0, 0 }
        };

        var result = openCvService?.Filter(imageData, kernel, BorderTypes.Constant, 1);

        Assert.AreEqual(5, result.Width);
        Assert.AreEqual(5, result.Height);

        Assert.AreEqual(0, result.GetPixelRgb(0, 0).R);
        Assert.AreEqual(0, result.GetPixelRgb(1, 0).R);
        Assert.AreEqual(0, result.GetPixelRgb(2, 0).R);
        Assert.AreEqual(0, result.GetPixelRgb(3, 0).R);
        Assert.AreEqual(0, result.GetPixelRgb(4, 0).R);

        Assert.AreEqual(0, result.GetPixelRgb(0, 1).R);
        Assert.AreEqual(9, result.GetPixelRgb(1, 1).R);
        Assert.AreEqual(9, result.GetPixelRgb(2, 1).R);
        Assert.AreEqual(9, result.GetPixelRgb(3, 1).R);
        Assert.AreEqual(0, result.GetPixelRgb(4, 1).R);

        Assert.AreEqual(0, result.GetPixelRgb(0, 2).R);
        Assert.AreEqual(9, result.GetPixelRgb(1, 2).R);
        Assert.AreEqual(9, result.GetPixelRgb(2, 2).R);
        Assert.AreEqual(9, result.GetPixelRgb(3, 2).R);
        Assert.AreEqual(0, result.GetPixelRgb(4, 2).R);

        Assert.AreEqual(0, result.GetPixelRgb(0, 3).R);
        Assert.AreEqual(9, result.GetPixelRgb(1, 3).R);
        Assert.AreEqual(9, result.GetPixelRgb(2, 3).R);
        Assert.AreEqual(9, result.GetPixelRgb(3, 3).R);
        Assert.AreEqual(0, result.GetPixelRgb(4, 3).R);

        Assert.AreEqual(0, result.GetPixelRgb(0, 4).R);
        Assert.AreEqual(0, result.GetPixelRgb(1, 4).R);
        Assert.AreEqual(0, result.GetPixelRgb(2, 4).R);
        Assert.AreEqual(0, result.GetPixelRgb(3, 4).R);
        Assert.AreEqual(0, result.GetPixelRgb(4, 4).R);
    }
}