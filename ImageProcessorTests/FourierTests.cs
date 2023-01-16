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
    public void Test()
    {
        var inputImage = new ImageData(new byte[,]
        {
            { 100, 200, 180, 100 },
            { 100, 120, 180, 100 },
            { 150, 100, 190, 130 }
        });

        var inputImageGray = inputImage.GetGrayArray();

        var complex = fftService.ToComplexData(inputImageGray);
        
        var fourier = fftService.FFT(complex);
        var result = fftService.InverseFFT(fourier);
        
        var resultImage = ToImageData(result);
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

    public Complex[,] ToComplex(byte[,] array)
    {
        var result = new Complex[array.GetLength(0), array.GetLength(1)];

        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                result[i, j] = new Complex(array[i, j], 0);
            }
        }

        return result;
    }

    private static ImageData ToImageData(Complex[,] result2)
    {
        var array = new byte[result2.GetLength(0), result2.GetLength(1)];

        for (var y = 0; y < array.GetLength(0); y++)
        {
            for (var x = 0; x < array.GetLength(1); x++)
            {
                var value = result2[y, x].Magnitude();
                if (value < 0) value = 0;
                if (value > 255) value = 255;
                array[y, x] = (byte)value;
            }
        }

        var image2 = new ImageData(array);
        return image2;
    }

    private static AlignedArrayDouble ImageToArray(IImageData imageData)
    {
        var array = new AlignedArrayDouble(256, imageData.Width, imageData.Height);

        for (var i = 0; i < imageData.Width; i++)
        {
            for (var j = 0; j < imageData.Height; j++)
            {
                var pixel = imageData.GetGrayValue(i, j);

                array[i, j] = pixel;
            }
        }

        return array;
    }

    public IImageData GetImageData(AlignedArrayComplex array)
    {
        var width = array.GetSize()[0];
        var height = array.GetSize()[1];
        var imageData = new ImageData(width, height);

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                var pixel = array[i, j].Real;

                pixel /= double.MaxValue;
                imageData.SetGrayValue(i, j, pixel * 255);
            }
        }

        return imageData;
    }

    private static AlignedArrayComplex GetComplexArray(IImageData bitmap)
    {
        using var input = new AlignedArrayComplex(16, bitmap.Width, bitmap.Height);

        for (var row = 0; row < input.GetLength(0); row++)
        {
            for (var col = 0; col < input.GetLength(1); col++)
            {
                input[row, col] = bitmap.GetGrayValue(row, col);
            }
        }

        return input;
    }
}