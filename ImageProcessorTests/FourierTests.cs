using System.Drawing;
using FFTW.NET;
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

        
        var resultImage = fftService.ToImageData(result); for (var x = 0; x < inputImage.Width; x++)
        {
            for (var y = 0; y < inputImage.Height; y++)
            {
                Assert.AreEqual(inputImage.Pixels[y, x], resultImage.Pixels[y, x]);
            }
        }

        Assert.AreEqual(4, resultImage.Width);
        Assert.AreEqual(3, resultImage.Height);
    }


}