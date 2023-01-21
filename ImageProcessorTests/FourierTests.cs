using System.Drawing;
using System.Numerics;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class FourierTests
{
    private FftService fftService;

    [TestInitialize]
    public void Initialize()
    {
        fftService = new FftService();
    }

    [TestMethod]
    public void SimpleFourierTransform()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
        });

        var complex = fftService.ToComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);

        Assert.AreEqual(127, fourier[0][0, 0].Magnitude);
        Assert.AreEqual(127, fourier[0][0, 2].Magnitude);
        Assert.AreEqual(127, fourier[1][0, 0].Magnitude);
        Assert.AreEqual(127, fourier[1][0, 2].Magnitude);
        Assert.AreEqual(127, fourier[2][0, 0].Magnitude);
        Assert.AreEqual(127, fourier[2][0, 2].Magnitude);
    }

    [TestMethod]
    public void SimpleShiftTest()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
            { 0, 254, 0, 254 },
        });

        var complex = fftService.ToComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);
        var shift = fftService.FFTShift(fourier);

        Assert.AreEqual(127, shift[0][2, 0].Magnitude);
        Assert.AreEqual(127, shift[0][2, 2].Magnitude);
        Assert.AreEqual(127, shift[1][2, 0].Magnitude);
        Assert.AreEqual(127, shift[1][2, 2].Magnitude);
        Assert.AreEqual(127, shift[2][2, 0].Magnitude);
        Assert.AreEqual(127, shift[2][2, 2].Magnitude);

        Assert.AreEqual(4, shift[0].GetLength(0));
        Assert.AreEqual(4, shift[0].GetLength(1));
    }

    [TestMethod]
    public void SimpleShiftTest2()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
            { 0, 127, 254, 127, 0, 127, 254, 127 },
        });

        inputImage.Save("a.png");

        var complex = fftService.ToComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);
        var shift = fftService.FFTShift(fourier);

        var shift2= fftService.LogN(shift);
        shift2 = fftService.Normalize(shift2); 

        Assert.AreNotEqual(0, shift2[0][4, 2].Magnitude);
        Assert.AreNotEqual(0, shift2[0][4, 4].Magnitude);
        Assert.AreNotEqual(0, shift2[0][4, 6].Magnitude);
    }


    [TestMethod]
    public void GrayscaleImageFourierTransformTest()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 100, 200, 180, 100 },
            { 100, 120, 180, 100 },
            { 150, 100, 190, 130 }
        });

        var inputImageGray = inputImage.GetGrayArray();

        var complex = fftService.ToComplexData(inputImageGray);

        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);

        var resultImage = fftService.ToImageData(result);
        var resultImageGray = resultImage.GetGrayArray();

        for (var x = 0; x < inputImage.Width; x++)
        {
            for (var y = 0; y < inputImage.Height; y++)
            {
                Assert.AreEqual(inputImageGray[y, x], resultImageGray[y, x]);
            }
        }

        Assert.AreEqual(4, resultImage.Width);
        Assert.AreEqual(3, resultImage.Height);
    }

    [TestMethod]
    public void GrayscaleColorImageFourierTransformTest()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 100, 200, 180, 100 },
            { 100, 120, 180, 100 },
            { 150, 100, 190, 130 }
        });

        var inputImageGray = inputImage.GetGrayArray();

        var complex = fftService.ToComplexData(inputImage);

        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);

        result = fftService.ChangeSize(result, inputImage.Height, inputImage.Width);

        var resultImage = fftService.ToImageData(result);
        var resultImageGray = resultImage.GetGrayArray();

        

        for (var x = 0; x < inputImage.Width; x++)
        {
            for (var y = 0; y < inputImage.Height; y++)
            {
                Assert.AreEqual(inputImageGray[y, x], resultImageGray[y, x]);
            }
        }

        Assert.AreEqual(4, resultImage.Width);
        Assert.AreEqual(3, resultImage.Height);
    }

    [TestMethod]
    public void ColorImageFourierTransformTest()
    {
        var inputImage = new ImageData(new[,]
        {
            { Color.FromArgb(110, 190, 200), Color.FromArgb(200, 100, 144), Color.FromArgb(100, 150, 200), Color.FromArgb(210, 100, 250), },
            { Color.FromArgb(150, 150, 220), Color.FromArgb(200, 120, 159), Color.FromArgb(100, 170, 255), Color.FromArgb(205, 210, 250), },
            { Color.FromArgb(190, 110, 230), Color.FromArgb(200, 150, 192), Color.FromArgb(100, 180, 127), Color.FromArgb(190, 115, 250), },
        });

        var complex = fftService.ToComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);
        result = fftService.ChangeSize(result, inputImage.Height, inputImage.Width);


        var resultImage = fftService.ToImageData(result);
        for (var x = 0; x < inputImage.Width; x++)
        {
            for (var y = 0; y < inputImage.Height; y++)
            {
                Assert.AreEqual(inputImage.Pixels[y, x], resultImage.Pixels[y, x]);
            }
        }

        Assert.AreEqual(4, resultImage.Width);
        Assert.AreEqual(3, resultImage.Height);
    }

    [TestMethod]
    public void NormalizationTest()
    {
        var complex = new Complex[,]
        {
            { new Complex(0, 0), new Complex(1, 0), new Complex(2, 0) }
        };

        var result = fftService.Normalize(complex);

        Assert.AreEqual(0, result[0, 0].Magnitude);
        Assert.AreEqual(127.5, result[0, 1].Magnitude);
        Assert.AreEqual(255, result[0, 2].Magnitude);
    }
    
    [TestMethod]
    public void NormalizationTest2()
    {
        var complex = new Complex[,]
        {
            { new Complex(0, 0), new Complex(1, 0), new Complex(2, 0) }
        };

        var result = fftService.Normalize(new[] { complex, complex, complex });

        Assert.AreEqual(0, result[0][0, 0].Magnitude);
        Assert.AreEqual(127.5, result[0][0, 1].Magnitude);
        Assert.AreEqual(255, result[0][0, 2].Magnitude);
    }

    [TestMethod]
    public void NormalizationInfinityTest()
    {

        var complex = new Complex[,]
        {
            { new Complex(0, 0), new Complex(1, 0), new Complex(2, 0), new Complex(double.PositiveInfinity,0) }
        };

        var result = fftService.Normalize(new[] { complex, complex, complex });

        Assert.AreEqual(0, result[0][0, 0].Magnitude);
        Assert.AreEqual(127.5, result[0][0, 1].Magnitude);
        Assert.AreEqual(255, result[0][0, 2].Magnitude);
        Assert.AreEqual(255, result[0][0, 3].Magnitude);
    }

    [TestMethod]
    public void AutomaticSizeChange()
    {
        var image = new ImageData(
            new byte[,]
            {
                {100,100,100, },
                {100,100,100, },
                {100,100,100, }
            }
        );

        var complex = fftService.ToComplexData(image);
        var fourier = fftService.ForwardFFT(complex);

        Assert.AreEqual(4, fourier[0].GetLength(0));
        Assert.AreEqual(4, fourier[0].GetLength(1));
    }
    
    [TestMethod]
    public void Test()
    {
        var image = new ImageData("lion.jpg", File.ReadAllBytes("../../../Resources/lion.jpg"));


        var complex = fftService.ToComplexData(image);
        var complex2 = fftService.ChangeSizeToClosestPowerOfTwo(complex);

        var fourier = fftService.ForwardFFT(complex2);

        int n = 50;

        var I = fourier[0].GetLength(0);
        var J = fourier[0].GetLength(1);

        for (var i = 0; i < I; i++)
        {
            for (var j = 0; j <J; j++)
            {
                if ((i < n || i > I - n) || (j < n || j < J - n)) continue;
                fourier[0][i, j] = new Complex(0, 0);
                fourier[1][i, j] = new Complex(0, 0);
                fourier[2][i, j] = new Complex(0, 0);
            }
        }

        var fourier1 = fourier;
        var fourier2 = fftService.LogN(fourier1);
        var fourier3 = fftService.Normalize(fourier2);
        var fourier4 = fftService.FFTShift(fourier3);

        fftService.ToImageData(fourier2).Save("fourier1.jpg");
        fftService.ToImageData(fourier2).Save("fourier2.jpg");
        fftService.ToImageData(fourier3).Save("fourier3.jpg");
        fftService.ToImageData(fourier4).Save("fourier4.jpg");

        var resultComplex = fftService.InverseFFT(fourier);

        resultComplex = fftService.ChangeSize(resultComplex, image.Height, image.Width);

        var resultImage = fftService.ToImageData(resultComplex);

        image.Save("lion-input.jpg");
        resultImage.Save("lion-result.jpg");
    }

    [TestMethod]
    public void ChangeSizeTest()
    {
        var tab = new[,]
        {
            { new Complex(1, 2), new Complex(1, 2), new Complex(1, 2), },
            { new Complex(1, 2), new Complex(1, 2), new Complex(1, 2), }
        };

        var t1 = fftService.ChangeSize(tab, 4, 3);
        var t2 = fftService.ChangeSize(tab, 2, 3);

        Assert.AreEqual(4, t1.GetLength(0));
        Assert.AreEqual(3, t1.GetLength(1));

        Assert.AreEqual(0, t1[3, 2].Real);
        Assert.AreEqual(0, t1[3, 2].Imaginary);

        for (var x = 0; x < 2; x++)
        {
            for (var y = 0; y < 3; y++)
            {
                Assert.AreEqual(tab[x, y], t2[x, y]);
            }
        }
    }

    [TestMethod]
    public void FindClosestPowerOf2()
    {
        Assert.AreEqual(8, fftService.FindPowerOf2(5));
        Assert.AreEqual(8, fftService.FindPowerOf2(8));
        Assert.AreEqual(16, fftService.FindPowerOf2(9));
        Assert.AreEqual(32, fftService.FindPowerOf2(17));
        Assert.AreEqual(32, fftService.FindPowerOf2(31));
        Assert.AreEqual(32, fftService.FindPowerOf2(32));
    }
}