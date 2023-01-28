using System.Numerics;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services.FourierServices;

/// <summary>
///     Klasa do przeprowadzania transformacji Fouriera wraz z funkcjami pomocniczymi.
/// </summary>
public class FftService
{
    private readonly IFourierService fourierService = new FourierService();

    /// <summary>
    ///     Funkcja do przeprowadzania transformacji Fouriera.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do przeprowadzania odwrotnej transformacji Fouriera.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
    public ComplexData InverseFFT(ComplexData complexData)
    {
        var red = fourierService.FFT2D(complexData.Data[0], FourierDirection.Backward);
        var green = fourierService.FFT2D(complexData.Data[1], FourierDirection.Backward);
        var blue = fourierService.FFT2D(complexData.Data[2], FourierDirection.Backward);

        var complexData2 = new ComplexData(complexData)
        {
            Data = new[] { red, green, blue },
            Height = complexData.Height,
            Width = complexData.Width
        };

        var result = Resize(complexData2, complexData.Height, complexData.Width);

        return result;
    }

    /// <summary>
    ///     Funkcja do pobrania logarytmu naturalnego z danych w dziedzinie częstotliwości.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do pobrania logarytmu naturalnego z danych w dziedzinie częstotliwości.
    /// </summary>
    /// <param name="fourier"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na zadaną wysokość i szerokość.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na zadaną wysokość i szerokość.
    /// </summary>
    /// <param name="input"></param>
    /// <param name="height"></param>
    /// <param name="width"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na najbliższą potęgę dwójki.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    private Complex[,] ChangeSizeToClosestPowerOfTwo(Complex[,] input)
    {
        var newWidth = FindWidthForPowerOfTwo(input);
        return Resize(input, newWidth, newWidth);
    }

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na najbliższą potęgę dwójki.
    /// </summary>
    /// <param name="imageData"></param>
    /// <returns></returns>
    public int FindWidthForPowerOfTwo(ImageData imageData)
    {
        var complexData = new ComplexData(imageData);
        return FindWidthForPowerOfTwo(complexData.Data[0]);
    }

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na najbliższą potęgę dwójki.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public int FindWidthForPowerOfTwo(Complex[,] input)
    {
        var nx = input.GetLength(0);
        var ny = input.GetLength(1);

        var newNx = (int)Math.Pow(2, Math.Ceiling(Math.Log(nx, 2)));
        var newNy = (int)Math.Pow(2, Math.Ceiling(Math.Log(ny, 2)));

        var newWidth = Math.Max(newNx, newNy);
        return newWidth;
    }

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na najbliższą potęgę dwójki.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public Complex[][,] ChangeSizeToClosestPowerOfTwo(Complex[][,] input)
    {
        var red = ChangeSizeToClosestPowerOfTwo(input[0]);
        var green = ChangeSizeToClosestPowerOfTwo(input[1]);
        var blue = ChangeSizeToClosestPowerOfTwo(input[2]);

        return new[] { red, green, blue };
    }

    /// <summary>
    ///     Funkcja do zmiany rozmiaru obrazu na najbliższą potęgę dwójki.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public int FindPowerOf2(int n)
    {
        var x = (int)Math.Log2(n);
        var y = (int)Math.Pow(2, x);
        if (y == n) return n;
        return (int)Math.Pow(2, x + 1);
    }

    /// <summary>
    ///     Funkcja do przesunięcia obrazu w dziedzinie częstotliwości tak, by zero znajdywało się na środku.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
    public ComplexData FFTShift(ComplexData complexData)
    {
        var data = FFTShift(complexData.Data);
        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    /// <summary>
    ///     Funkcja do przesunięcia obrazu w dziedzinie częstotliwości tak, by zero znajdywało się na środku.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
    public Complex[,] FFTShift(Complex[,] complexData)
    {
        var nx = complexData.GetLength(0);
        var ny = complexData.GetLength(1);

        var FFTShifted = new Complex[nx, ny];

        for (var i = 0; i < nx / 2; i++)
        {
            for (var j = 0; j < ny / 2; j++)
            {
                FFTShifted[i + nx / 2, j + ny / 2] = complexData[i, j];
                FFTShifted[i, j] = complexData[i + nx / 2, j + ny / 2];
                FFTShifted[i + nx / 2, j] = complexData[i, j + ny / 2];
                FFTShifted[i, j + nx / 2] = complexData[i + nx / 2, j];
            }
        }

        return FFTShifted;
    }

    /// <summary>
    ///     Funkcja do przesunięcia obrazu w dziedzinie częstotliwości tak, by zero znajdywało się na środku.
    /// </summary>
    /// <param name="Output"></param>
    /// <returns></returns>
    public Complex[][,] FFTShift(Complex[][,] Output)
    {
        var red = FFTShift(Output[0]);
        var green = FFTShift(Output[1]);
        var blue = FFTShift(Output[2]);

        return new[] { red, green, blue };
    }

    /// <summary>
    ///     Funkcja do normalizacji obrazu.
    /// </summary>
    /// <param name="complexData"></param>
    /// <returns></returns>
    public ComplexData Normalize(ComplexData complexData)
    {
        var data = Normalize(complexData.Data);
        return new ComplexData(complexData)
        {
            Data = data
        };
    }

    /// <summary>
    ///     Funkcja do normalizacji obrazu.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do normalizacji obrazu.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja do normalizacji obrazu.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static bool IsNotFiniteNumber(double value)
    {
        return double.IsNaN(value) || double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value);
    }

    /// <summary>
    ///     Funkcja do dodania szumu do obrazu.
    /// </summary>
    /// <param name="complexData"></param>
    /// <param name="frequencyX"></param>
    /// <param name="frequencyY"></param>
    /// <param name="amplitude"></param>
    /// <param name="phase"></param>
    /// <returns></returns>
    public ComplexData AddPeriodicNoise(ComplexData complexData, double frequencyX, double frequencyY, double amplitude, double phase)
    {
        for (var y = 0; y < complexData.Height; y++)
        {
            for (var x = 0; x < complexData.Width; x++)
            {
                var argumentX = frequencyX * x * Math.PI / 2;
                var argumentY = frequencyY * y * Math.PI / 2;
                var phaseX = phase * Math.PI * frequencyX / 2;
                var phaseY = phase * Math.PI * frequencyY / 2;

                var value = amplitude * Math.Sin(argumentX + argumentY - phaseX - phaseY);
                value = (int)value;
                complexData.Data[0][y, x] += value;
                complexData.Data[1][y, x] += value;
                complexData.Data[2][y, x] += value;
            }
        }

        return complexData;
    }

    /// <summary>
    ///     Funkcja do dodania szumu periodycznego.
    /// </summary>
    /// <param name="imageData"></param>
    /// <param name="frequencyX"></param>
    /// <param name="frequencyY"></param>
    /// <param name="amplitude"></param>
    /// <param name="phase"></param>
    /// <returns></returns>
    public ImageData AddPeriodicNoise(ImageData imageData, double frequencyX = 0, double frequencyY = 0, double amplitude = 1, double phase = 0)
    {
        var complexData = new ComplexData(imageData);
        complexData = AddPeriodicNoise(complexData, frequencyX, frequencyY, amplitude, phase);
        return complexData.ToImageData();
    }

    /// <summary>
    ///     Funkcja do usuniecia prostokątów z obrazu.
    /// </summary>
    /// <param name="complexData"></param>
    /// <param name="rectangleStartX"></param>
    /// <param name="rectangleStartY"></param>
    /// <param name="rectangleEndX"></param>
    /// <param name="rectangleEndY"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public ComplexData RemoveRectangles(ComplexData complexData, int rectangleStartX, int rectangleStartY, int rectangleEndX, int rectangleEndY, RemoveRecanglesModeEnum mode)
    {
        var height = complexData.Data[0].GetLength(0);
        var width = complexData.Data[0].GetLength(1);

        for (var x = rectangleStartX; x < rectangleEndX; x++)
        {
            for (var y = rectangleStartY; y < rectangleEndY; y++)
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

    /// <summary>
    ///     Funkcja pomocnicza do kasowania pikseli.
    /// </summary>
    /// <param name="complexData"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
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

    /// <summary>
    ///     Funkcja pomocnicza do sprawdzania czy dane koordynaty znajdują się w ustalonym przedziale.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns></returns>
    private static bool IsInRange(int x, int y, int width, int height)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
}