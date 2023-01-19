using System.Drawing;
using ImageProcessorLibrary.DataStructures;

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
    public void Test()
    {
        var image = new ImageData("lion.jpg", File.ReadAllBytes("../../../Resources/lion.jpg"));


            var complex = fftService.ToComplexData(image);
            complex = fftService.ChangeSizeToClosestPowerOfTwo(complex);

            var fourier = fftService.ForwardFFT(complex);

            //for (var i = 0; i < fourier[0].GetLength(0); i++)
            //{
            //    for (var j = 0; j < fourier[0].GetLength(1); j++)
            //    {
            //        if (i < n ) continue;
            //        fourier[0][i, j] = new Complex(0, 0);
            //        fourier[1][i, j] = new Complex(0, 0);
            //        fourier[2][i, j] = new Complex(0, 0);
            //    }
            //}

            fourier = fftService.FFTShift(fourier);
            fourier = fftService.Normalize(fourier);

            fftService.ToImageData(fourier).Save("fourier.jpg");

            var resultComplex = fftService.InverseFFT(fourier);

            resultComplex = fftService.ChangeSize(resultComplex, image.Height, image.Width);

            var resultImage = fftService.ToImageData(resultComplex);

            resultImage.Save($"result.jpg");
        

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
        Assert.AreEqual(0, t1[3, 2].Imag);

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