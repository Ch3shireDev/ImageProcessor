using System.Numerics;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.Enums;

namespace ImageProcessorLibrary.Services.FourierServices;

public class FftService
{
    private readonly IFourierService fourierService = new FourierService();

    public ComplexData ForwardFFT(ComplexData complexData)
    {
        var fourier = complexData.Data;
        fourier = ChangeSizeToClosestPowerOfTwo(fourier);

        var red = fourierService.FFT2D(fourier[0]);
        var green = fourierService.FFT2D(fourier[1]);
        var blue = fourierService.FFT2D(fourier[2]);

        return new ComplexData(complexData)
        {
            Data = new[] { red, green, blue }
        };
    }

    public ComplexData InverseFFT(ComplexData complexData)
    {
        var red = fourierService.FFT2D(complexData.Data[0], -1);
        var green = fourierService.FFT2D(complexData.Data[1], -1);
        var blue = fourierService.FFT2D(complexData.Data[2], -1);

        var complexData2 = new ComplexData(complexData)
        {
            Data = new[] { red, green, blue },
            Height = complexData.Height,
            Width = complexData.Width
        };

        var result = Resize(complexData2, complexData.Height, complexData.Width);

        return result;
    }

    public ComplexData LogN(ComplexData complexData)
    {
        var red = LogN(complexData.Data[0]);
        var green = LogN(complexData.Data[1]);
        var blue = LogN(complexData.Data[2]);
        var data = new[] { red, green, blue };

        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    public static Complex[,] LogN(Complex[,] fourier)
    {
        var result = new Complex[fourier.GetLength(0), fourier.GetLength(1)];

        for (var x = 0; x < fourier.GetLength(0); x++)
        {
            for (var y = 0; y < fourier.GetLength(1); y++)
            {
                var value = Math.Log(fourier[x, y].Magnitude);
                result[x, y] = new Complex(value, 0);
            }
        }

        return result;
    }

    public Complex[,] Resize(Complex[,] input, int height, int width)
    {
        var output = new Complex[height, width];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (i >= input.GetLength(0) || j >= input.GetLength(1))
                {
                    output[i, j] = new Complex(0, 0);
                }
                else
                {
                    output[i, j] = input[i, j];
                }
            }
        }

        return output;
    }

    public ComplexData Resize(ComplexData input, int height, int width)
    {
        var red = Resize(input.Data[0], height, width);
        var green = Resize(input.Data[1], height, width);
        var blue = Resize(input.Data[2], height, width);
        var outputData = new[] { red, green, blue };
        return new ComplexData
        {
            Data = outputData,
            Width = width,
            Height = height
        };
    }

    private Complex[,] ChangeSizeToClosestPowerOfTwo(Complex[,] input)
    {
        var newWidth = FindWidthForPowerOfTwo(input);
        return Resize(input, newWidth, newWidth);
    }

    public int FindWidthForPowerOfTwo(ImageData imageData)
    {
        var complexData = new ComplexData(imageData);
        return FindWidthForPowerOfTwo(complexData.Data[0]);
    }

    public int FindWidthForPowerOfTwo(Complex[,] input)
    {
        var nx = input.GetLength(0);
        var ny = input.GetLength(1);

        var newNx = (int)Math.Pow(2, Math.Ceiling(Math.Log(nx, 2)));
        var newNy = (int)Math.Pow(2, Math.Ceiling(Math.Log(ny, 2)));

        var newWidth = Math.Max(newNx, newNy);
        return newWidth;
    }

    public Complex[][,] ChangeSizeToClosestPowerOfTwo(Complex[][,] input)
    {
        var red = ChangeSizeToClosestPowerOfTwo(input[0]);
        var green = ChangeSizeToClosestPowerOfTwo(input[1]);
        var blue = ChangeSizeToClosestPowerOfTwo(input[2]);

        return new[] { red, green, blue };
    }

    public int FindPowerOf2(int n)
    {
        var x = (int)Math.Log2(n);
        var y = (int)Math.Pow(2, x);
        if (y == n) return n;
        return (int)Math.Pow(2, x + 1);
    }


    public ComplexData FFTShift(ComplexData complexData)
    {
        var data = FFTShift(complexData.Data);
        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    public Complex[,] FFTShift(Complex[,] Output)
    {
        var nx = Output.GetLength(0);
        var ny = Output.GetLength(1);

        int i, j;
        var FFTShifted = new Complex[nx, ny];

        for (i = 0; i <= nx / 2 - 1; i++)
            for (j = 0; j <= ny / 2 - 1; j++)
            {
                FFTShifted[i + nx / 2, j + ny / 2] = Output[i, j];
                FFTShifted[i, j] = Output[i + nx / 2, j + ny / 2];
                FFTShifted[i + nx / 2, j] = Output[i, j + ny / 2];
                FFTShifted[i, j + nx / 2] = Output[i + nx / 2, j];
            }

        return FFTShifted;
    }

    public Complex[][,] FFTShift(Complex[][,] Output)
    {
        var red = FFTShift(Output[0]);
        var green = FFTShift(Output[1]);
        var blue = FFTShift(Output[2]);

        return new[] { red, green, blue };
    }

    public ComplexData Normalize(ComplexData complexData)
    {
        var data = Normalize(complexData.Data);
        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    public Complex[,] Normalize(Complex[,] input)
    {
        double max = 0;
        double min = 255;

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                var magnitude = input[x, y].Real;
                if (magnitude > max) max = magnitude;
                if (magnitude < min) min = magnitude;
            }
        }

        var param = 255 / (max - min);

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                var value = input[x, y].Real - min;

                input[x, y] = new Complex(value * param, 0);
            }
        }

        return input;
    }

    public Complex[][,] Normalize(Complex[][,] input)
    {
        double max = 0;
        double min = 255;

        for (var z = 0; z < 3; z++)
        {
            for (var x = 0; x < input[z].GetLength(0); x++)
            {
                for (var y = 0; y < input[z].GetLength(1); y++)
                {
                    var magnitude0 = input[z][x, y].Real;

                    if (IsNotFiniteNumber(magnitude0)) continue;

                    if (magnitude0 > max) max = magnitude0;
                    if (magnitude0 < min) min = magnitude0;
                }
            }
        }

        var param = 255 / (max - min);

        var output = new Complex[3][,];

        for (var z = 0; z < 3; z++)
        {
            var outputz = new Complex[input[0].GetLength(0), input[0].GetLength(1)];

            for (var x = 0; x < input[0].GetLength(0); x++)
            {
                for (var y = 0; y < input[0].GetLength(1); y++)
                {
                    var value = input[z][x, y].Real - min;
                    if (value > 9999) value = max;
                    if (value < -9999) value = 0;

                    if (IsNotFiniteNumber(value))
                    {
                        value = 0;
                    }

                    outputz[x, y] = new Complex(value * param, 0);
                }
            }

            output[z] = outputz;
        }

        return output;
    }

    private static bool IsNotFiniteNumber(double value)
    {
        return double.IsNaN(value) || double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value);
    }

    public ComplexData AddPeriodicNoise(ComplexData complexData, double frequencyX, double frequencyY, double amplitude, double phase)
    {
        for (var y = 0; y < complexData.Height; y++)
        {
            for (var x = 0; x < complexData.Width; x++)
            {
                var value = amplitude * Math.Sin(frequencyX * x * Math.PI / 2 + frequencyY * y * Math.PI / 2 - phase * Math.PI * frequencyX / 2 - phase * Math.PI * frequencyY / 2);
                value = (int)value;
                complexData.Data[0][y, x] += value;
                complexData.Data[1][y, x] += value;
                complexData.Data[2][y, x] += value;
            }
        }

        return complexData;
    }

    public ImageData AddPeriodicNoise(ImageData imageData, double frequencyX = 0, double frequencyY = 0, double amplitude = 1, double phase = 0)
    {
        var complexData = new ComplexData(imageData);
        complexData = AddPeriodicNoise(complexData, frequencyX, frequencyY, amplitude, phase);
        return complexData.ToImageData();
    }

    public ComplexData RemoveRectangles(ComplexData complexData, int x1, int y1, int x2, int y2, RemoveRecanglesModeEnum mode)
    {
        var height = complexData.Data[0].GetLength(0);
        var width = complexData.Data[0].GetLength(1);

        for (var x = x1; x < x2; x++)
        {
            for (var y = y1; y < y2; y++)
            {
                switch (mode)
                {
                    case RemoveRecanglesModeEnum.NONE:
                        break;
                    case RemoveRecanglesModeEnum.SINGLE:
                        complexData = RemovePixelIfIsInRange(complexData, x, y, width, height);
                        break;
                    case RemoveRecanglesModeEnum.DOUBLE:
                        complexData = RemovePixelIfIsInRange(complexData, x, y, width, height);
                        complexData = RemovePixelIfIsInRange(complexData, width - x - 1, height - y - 1, width, height);
                        break;
                    case RemoveRecanglesModeEnum.QUAD:
                        complexData = RemovePixelIfIsInRange(complexData, x, y, width, height);
                        complexData = RemovePixelIfIsInRange(complexData, width - x - 1, height - y - 1, width, height);
                        complexData = RemovePixelIfIsInRange(complexData, x, height - y - 1, width, height);
                        complexData = RemovePixelIfIsInRange(complexData, width - x - 1, y, width, height);
                        break;
                }
            }
        }

        return complexData;
    }

    private ComplexData RemovePixelIfIsInRange(ComplexData complexData, int x, int y, int width, int height)
    {
        if (IsInRange(x, y, width, height))
        {
            complexData.Data[0][y, x] = 0;
            complexData.Data[1][y, x] = 0;
            complexData.Data[2][y, x] = 0;
        }

        return complexData;
    }

    private bool IsInRange(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}