using System.Drawing;
using System.Numerics;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class FftService
{
    private readonly IFourierService fourierService = new FourierService();

    public Complex[,] InverseFFT(Complex[,] Fourier)
    {
        var nx = Fourier.GetLength(0);
        var ny = Fourier.GetLength(1);

        return fourierService.FFT2D(Fourier, nx, ny, -1);
    }

    public ComplexData ForwardFFT(ComplexData complexData)
    {
        var forward = ForwardFFT(complexData.Data);

        return new ComplexData(complexData)
        {
            Data = forward
        };
    }

    public Complex[,] ForwardFFT(Complex[,] Fourier)
    {
        var nx = Fourier.GetLength(0);
        var ny = Fourier.GetLength(1);

        return fourierService.FFT2D(Fourier, nx, ny, 1);
    }

    private Complex[][,] InverseFFT(Complex[][,] Fourier)
    {
        var red = InverseFFT(Fourier[0]);
        var green = InverseFFT(Fourier[1]);
        var blue = InverseFFT(Fourier[2]);

        return new[] { red, green, blue };
    }

    public ComplexData InverseFFT(ComplexData complexData)
    {
        var inverse = InverseFFT(complexData.Data);


        var complexData2 = new ComplexData(complexData)
        {
            Data = inverse,
            Height = complexData.Height,
            Width = complexData.Width
        };

        var result = Resize(complexData2, complexData.Height, complexData.Width);

        return result;
    }

    public Complex[][,] ForwardFFT(Complex[][,] Fourier)
    {
        Fourier = ChangeSizeToClosestPowerOfTwo(Fourier);

        var red = ForwardFFT(Fourier[0]);
        var green = ForwardFFT(Fourier[1]);
        var blue = ForwardFFT(Fourier[2]);

        return new[] { red, green, blue };
    }


    public double GetMinReal(ComplexData complexData)
    {
        var min = double.MaxValue;

        foreach (var complex in complexData.Data)
            for (var x = 0; x < complex.GetLength(0); x++)
            {
                for (var y = 0; y < complex.GetLength(1); y++)
                {
                    var value = complex[x, y].Real;

                    if (value < min)
                    {
                        min = value;
                    }
                }
            }

        return min;
    }

    public ComplexData LogN(ComplexData complexData)
    {
        var data = LogN(complexData.Data);
        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    private Complex[][,] LogN(Complex[][,] fourier)
    {
        var red = LogN(fourier[0]);
        var green = LogN(fourier[1]);
        var blue = LogN(fourier[2]);

        return new[] { red, green, blue };
    }

    private Complex[,] LogN(Complex[,] fourier)
    {
        var result = new Complex[fourier.GetLength(0), fourier.GetLength(1)];

        for (var x = 0; x < fourier.GetLength(0); x++)
        {
            for (var y = 0; y < fourier.GetLength(1); y++)
            {
                var value = Math.Log(fourier[x, y].Magnitude);

                //if (Math.Abs(value) > 99999) value = 0;
                if (value < 0)
                {
                }

                result[x, y] = new Complex(value, 0);
            }
        }

        return result;
    }

    public ImageData ToImageData(ComplexData complexData)
    {
        return ToImageData(complexData.Data);
    }

    public ImageData ToImageData(Complex[,] input)
    {
        var array = new byte[input.GetLength(0), input.GetLength(1)];

        for (var y = 0; y < array.GetLength(0); y++)
        {
            for (var x = 0; x < array.GetLength(1); x++)
            {
                var value = input[y, x].Magnitude;
                if (value > 255) value = 255;
                array[y, x] = (byte)value;
            }
        }

        var image2 = new ImageData(array);
        return image2;
    }


    public ImageData ToImageData(Complex[][,] input)
    {
        var red = input[0];
        var green = input[1];
        var blue = input[2];

        var colors = new Color[red.GetLength(0), red.GetLength(1)];

        for (var y = 0; y < red.GetLength(0); y++)
        {
            for (var x = 0; x < red.GetLength(1); x++)
            {
                colors[y, x] = Color.FromArgb((byte)red[y, x].Magnitude, (byte)green[y, x].Magnitude, (byte)blue[y, x].Magnitude);
            }
        }

        return new ImageData(colors);
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
        var nx = input.GetLength(0);
        var ny = input.GetLength(1);

        var newNx = (int)Math.Pow(2, Math.Ceiling(Math.Log(nx, 2)));
        var newNy = (int)Math.Pow(2, Math.Ceiling(Math.Log(ny, 2)));

        return Resize(input, newNx, newNy);
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

                    if (magnitude0 == double.NaN || magnitude0 == double.PositiveInfinity || magnitude0 == double.NegativeInfinity)
                    {
                        continue;
                    }

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

                    if (value == double.NaN || value == double.PositiveInfinity || value == double.NegativeInfinity)
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
}