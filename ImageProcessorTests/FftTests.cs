using System.Drawing;
using System.Numerics;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.FourierServices;

namespace ImageProcessorTests;

[TestClass]
public class FftTests
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
            { 0, 254, 0, 254 }
        });

        inputImage.Write("a-1.jpg");

        var complex = new ComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);

        var f2 = fftService.FFTShift(fourier);
        f2 = fftService.Normalize(f2);
        var f3 = f2.ToImageData();
        f3.Write("a-2.jpg");


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
            { 0, 254, 0, 254 }
        });

        var complex = new ComplexData(inputImage);
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
            { 0, 127, 254, 127, 0, 127, 254, 127 }
        });

        inputImage.Write("a.png");

        var complex = new ComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);
        var shift = fftService.FFTShift(fourier);

        var shift2 = fftService.LogN(shift);
        shift2 = fftService.Normalize(shift2);

        Assert.AreEqual(0, shift2[0][4, 2].Magnitude);
        Assert.AreEqual(255, shift2[0][4, 4].Magnitude);
        Assert.AreEqual(0, shift2[0][4, 6].Magnitude);
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

        var complex = new ComplexData(inputImageGray);

        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);

        //result = fftService.Resize(fourier, inputImage.Height, inputImage.Width);

        var resultImage = result.ToImageData();
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

        var complex = new ComplexData(inputImage);

        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);

        var resultImage = result.ToImageData();
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
            { Color.FromArgb(110, 190, 200), Color.FromArgb(200, 100, 144), Color.FromArgb(100, 150, 200), Color.FromArgb(210, 100, 250) },
            { Color.FromArgb(150, 150, 220), Color.FromArgb(200, 120, 159), Color.FromArgb(100, 170, 255), Color.FromArgb(205, 210, 250) },
            { Color.FromArgb(190, 110, 230), Color.FromArgb(200, 150, 192), Color.FromArgb(100, 180, 127), Color.FromArgb(190, 115, 250) }
        });

        var complex = new ComplexData(inputImage);
        var fourier = fftService.ForwardFFT(complex);
        var result = fftService.InverseFFT(fourier);


        var resultImage = result.ToImageData();

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
        var complex = new[,]
        {
            { new(0, 0), new Complex(1, 0), new Complex(2, 0) }
        };

        var result = fftService.Normalize(complex);

        Assert.AreEqual(0, result[0, 0].Magnitude);
        Assert.AreEqual(127.5, result[0, 1].Magnitude);
        Assert.AreEqual(255, result[0, 2].Magnitude);
    }

    [TestMethod]
    public void NormalizationTest2()
    {
        var complex = new[,]
        {
            { new(0, 0), new Complex(1, 0), new Complex(2, 0) }
        };

        var result = fftService.Normalize(new[] { complex, complex, complex });

        Assert.AreEqual(0, result[0][0, 0].Magnitude);
        Assert.AreEqual(127.5, result[0][0, 1].Magnitude);
        Assert.AreEqual(255, result[0][0, 2].Magnitude);
    }

    [TestMethod]
    public void NormalizationInfinityTest()
    {
        var complex = new[,]
        {
            { new(0, 0), new Complex(1, 0), new Complex(2, 0), new Complex(double.PositiveInfinity, 0) }
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
                { 100, 100, 100 },
                { 100, 100, 100 },
                { 100, 100, 100 }
            }
        );

        var complex = new ComplexData(image);
        var fourier = fftService.ForwardFFT(complex);

        Assert.AreEqual(4, fourier[0].GetLength(0));
        Assert.AreEqual(4, fourier[0].GetLength(1));
    }

    [TestMethod]
    public void ChangeSizeTest()
    {
        var tab = new[,]
        {
            { new Complex(1, 2), new Complex(1, 2), new Complex(1, 2) },
            { new Complex(1, 2), new Complex(1, 2), new Complex(1, 2) }
        };

        var t1 = fftService.Resize(tab, 4, 3);
        var t2 = fftService.Resize(tab, 2, 3);

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

    [TestMethod]
    public void AddZeroPeriodicNoiseTest()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0, 0, 0);

        Assert.AreEqual(input.Width, givenOutput.Width);
        Assert.AreEqual(input.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(input[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void AddPeriodicNoiseX()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 1);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 100, 101, 100, 99, 100 },
            { 100, 101, 100, 99, 100 },
            { 100, 101, 100, 99, 100 },
            { 100, 101, 100, 99, 100 },
            { 100, 101, 100, 99, 100 }
        });

        Assert.AreEqual(5, givenOutput.Width);
        Assert.AreEqual(5, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void AddPeriodicNoiseY()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0, 1, 2);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 },
            { 102, 102, 102, 102, 102 },
            { 100, 100, 100, 100, 100 },
            { 98, 98, 98, 98, 98 },
            { 100, 100, 100, 100, 100 }
        });

        Assert.AreEqual(5, givenOutput.Width);
        Assert.AreEqual(5, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void AddPeriodicNoiseXY()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 },
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 1, 1, 2);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 100, 102, 100, 98, 100 },
            { 102, 100, 98, 100, 102 },
            { 100, 98, 100, 102, 100 },
            { 98, 100, 102, 100, 98 },
            { 100, 102, 100, 98, 100 }
        });

        Assert.AreEqual(5, givenOutput.Width);
        Assert.AreEqual(5, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void DoubleFrequencyX()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0.5, 0, 4);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 100, 102, 104, 102, 100 }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void PhaseXTest()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0.5, 0, 4, 1);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 98, 100, 102, 104, 102 }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void PhaseXTest2()
    {
        var input = new ImageData(new byte[,]
        {
            { 100, 100, 100, 100, 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 1, 0, 5, 1);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 95, 100, 105, 100, 95 }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void FrequencyYTest()
    {
        var input = new ImageData(new byte[,]
        {
            { 100 },
            { 100 },
            { 100 },
            { 100 },
            { 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0, 0.5, 4);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 100 },
            { 102 },
            { 104 },
            { 102 },
            { 100 }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void PhaseYTest()
    {
        var input = new ImageData(new byte[,]
        {
            { 100 },
            { 100 },
            { 100 },
            { 100 },
            { 100 }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 0, 0.5, 4, 1);

        var expectedOutput = new ImageData(new byte[,]
        {
            { 98 },
            { 100 },
            { 102 },
            { 104 },
            { 102 }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void OverflowTest()
    {
        var input = new ImageData(new[,]
        {
            { Color.FromArgb(250, 120, 120), Color.FromArgb(250, 120, 120), Color.FromArgb(250, 120, 120) }
        });

        var givenOutput = fftService.AddPeriodicNoise(input, 1, 0, 100);

        var expectedOutput = new ImageData(new[,]
        {
            { Color.FromArgb(250, 120, 120), Color.FromArgb(255, 220, 220), Color.FromArgb(250, 120, 120) }
        });

        Assert.AreEqual(expectedOutput.Width, givenOutput.Width);
        Assert.AreEqual(expectedOutput.Height, givenOutput.Height);

        for (var y = 0; y < input.Height; y++)
        {
            for (var x = 0; x < input.Width; x++)
            {
                Assert.AreEqual(expectedOutput[y, x], givenOutput[y, x], $"Error for x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void RemoveRectanglesDoubleTest()
    {
        var complexData = new ComplexData(new byte[,]
        {
            { 2, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 2, 2, 0, 0, 0 },
            { 0, 0, 0, 2, 2, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 2, 0, 0, 0, 0, 0, 0, 2 }
        });

        var givenOutput = fftService.RemoveRectangles(complexData, 1, 1, 3, 3, RemoveRecanglesModeEnum.DOUBLE);

        var expectedResult = new ComplexData(new byte[,]
        {
            { 2, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 2, 2, 0, 0, 0 },
            { 0, 0, 0, 2, 2, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 0, 0, 0 },
            { 2, 0, 0, 0, 0, 0, 0, 2 }
        });

        Assert.AreEqual(8, givenOutput.Width);
        Assert.AreEqual(8, givenOutput.Height);

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                Assert.AreEqual(expectedResult[0][y, x], givenOutput[0][y, x], $"x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void RemoveRectanglesNoMirrorTest()
    {
        var complexData = new ComplexData(new byte[,]
        {
            { 6, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 7, 7, 0, 0, 0 },
            { 0, 0, 0, 7, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 6, 0, 0, 0, 0, 0, 0, 6 }
        });

        var givenOutput = fftService.RemoveRectangles(complexData, 1, 1, 3, 3, RemoveRecanglesModeEnum.SINGLE);

        var expectedResult = new ComplexData(new byte[,]
        {
            { 6, 0, 0, 0, 0, 0, 0, 6 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 7, 7, 0, 0, 0 },
            { 0, 0, 0, 7, 7, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 0, 0, 1, 1, 0 },
            { 6, 0, 0, 0, 0, 0, 0, 6 }
        });

        Assert.AreEqual(8, givenOutput.Width);
        Assert.AreEqual(8, givenOutput.Height);

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                Assert.AreEqual(expectedResult[0][y, x], givenOutput[0][y, x], $"x:{x}, y:{y}");
            }
        }
    }

    [TestMethod]
    public void RemoveRectanglesQuadTest()
    {
        var complexData = new ComplexData(new byte[,]
        {
            { 8, 0, 0, 0, 0, 0, 0, 8 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 0, 0, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 0, 0, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 0, 1, 1, 0, 0, 1, 1, 0 },
            { 8, 0, 0, 0, 0, 0, 0, 8 }
        });

        var givenOutput = fftService.RemoveRectangles(complexData, 1, 1, 3, 3, RemoveRecanglesModeEnum.QUAD);

        var expectedResult = new ComplexData(new byte[,]
        {
            { 8, 0, 0, 0, 0, 0, 0, 8 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 9, 9, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 8, 0, 0, 0, 0, 0, 0, 8 }
        });

        Assert.AreEqual(8, givenOutput.Width);
        Assert.AreEqual(8, givenOutput.Height);

        for (var y = 0; y < 8; y++)
        {
            for (var x = 0; x < 8; x++)
            {
                Assert.AreEqual(expectedResult[0][y, x], givenOutput[0][y, x]);
            }
        }
    }

    [TestMethod]
    public void DoubleFftShift()
    {
        var complexData = new ComplexData(new byte[,]
        {
            { 1, 2, 3, 4 },
            { 5, 6, 7, 8 },
            { 9, 10, 11, 12 },
            { 13, 14, 15, 16 }
        });

        var result = fftService.FFTShift(complexData);
        var result2 = fftService.FFTShift(result);

        Assert.AreEqual(4, result.Width);
        Assert.AreEqual(4, result.Height);

        for (var y = 0; y < 4; y++)
        {
            for (var x = 0; x < 4; x++)
            {
                Assert.AreEqual(complexData[0][y, x], result2[0][y, x]);
            }
        }
    }
}