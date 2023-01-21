using System.Drawing;
using System.Numerics;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Services;

public class FftService
{
    public ComplexData ToComplexData(byte[,] GreyImage)
    {
        return new ComplexData(GreyImage);
    }

    public Complex[,] InverseFFT(Complex[,] Fourier)
    {
        var nx = Fourier.GetLength(0);
        var ny = Fourier.GetLength(1);

        return FFT2D(Fourier, nx, ny, -1);
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

        return FFT2D(Fourier, nx, ny, 1);
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

        inverse = Resize(inverse, complexData.Height, complexData.Width);

        return new ComplexData(complexData)
        {
            Data = inverse,
            Height = complexData.Height,
            Width = complexData.Width
        };
    }

    public Complex[][,] ForwardFFT(Complex[][,] Fourier)
    {
        Fourier = ChangeSizeToClosestPowerOfTwo(Fourier);

        var red = ForwardFFT(Fourier[0]);
        var green = ForwardFFT(Fourier[1]);
        var blue = ForwardFFT(Fourier[2]);

        return new[] { red, green, blue };
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
                if (Math.Abs(value) > 99999) value = 0;
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


    public ComplexData ToComplexData(IImageData GreyImage)
    {
        var threeFourier = new Complex[3][,];

        for (var k = 0; k < 3; k++)
        {
            threeFourier[k] = ToComplexData(GetChannel(k, GreyImage.Pixels)).Data[k];
        }

        return new ComplexData
        {
            Data = threeFourier,
            Width = GreyImage.Width,
            Height = GreyImage.Height
        };
    }

    private static byte GetChannel(int k, Color color)
    {
        var value = k switch
        {
            0 => color.R,
            1 => color.G,
            _ => color.B
        };
        return value;
    }

    private static byte[,] GetChannel(int k, Color[,] colors)
    {
        var result = new byte[colors.GetLength(0), colors.GetLength(1)];

        for (var i = 0; i < colors.GetLength(0); i++)
        {
            for (var j = 0; j < colors.GetLength(1); j++)
            {
                result[i, j] = GetChannel(k, colors[i, j]);
            }
        }

        return result;
    }


    /*-------------------------------------------------------------------------
        Perform a 2D FFT inplace given a complex 2D array
        The direction dir, 1 for forward, -1 for reverse
        The size of the array (height,width)
        Return false if there are memory problems or
        the dimensions are not powers of 2
    */

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
        var outputData = Resize(input.Data, height, width);
        return new ComplexData
        {
            Data = outputData,
            Width = width,
            Height = height
        };
    }

    private Complex[][,] Resize(Complex[][,] input, int height, int width)
    {
        var red = Resize(input[0], height, width);
        var green = Resize(input[1], height, width);
        var blue = Resize(input[2], height, width);

        return new[] { red, green, blue };
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


    private Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
    {
        int i, j;
        int m; //Power of 2 for current number of points

        var output = c;
        // Transform the Rows 
        var real = new double[nx];
        var imag = new double[nx];

        for (j = 0; j < ny; j++)
        {
            for (i = 0; i < nx; i++)
            {
                real[i] = c[i, j].Real;
                imag[i] = c[i, j].Imaginary;
            }

            // Calling 1D FFT Function for Rows
            m = (int)Math.Log(nx, 2); //Finding power of 2 for current number of points e.g. for height=512 m=9
            FFT1D(dir, m, ref real, ref imag);

            for (i = 0; i < nx; i++)
            {
                output[i, j] = new Complex(real[i], imag[i]);
            }
        }

        // Transform the columns  
        real = new double[ny];
        imag = new double[ny];

        for (i = 0; i < nx; i++)
        {
            for (j = 0; j < ny; j++)
            {
                real[j] = output[i, j].Real;
                imag[j] = output[i, j].Imaginary;
            }

            // Calling 1D FFT Function for Columns


            m = (int)Math.Log(ny, 2); //Finding power of 2 for current number of points e.g. for height=512 m=9


            FFT1D(dir, m, ref real, ref imag);

            for (j = 0; j < ny; j++)
            {
                //c[i,j].Real = Real[j];
                //c[i,j].Imag = Imag[j];
                output[i, j] = new Complex(real[j], imag[j]);
            }
        }

        // return(true);
        return output;
    }

    /*-------------------------------------------------------------------------
        This computes an in-place complex-to-complex FFT
        x and y are the Real and imaginary arrays of 2^m points.
        dir = 1 gives forward transform
        dir = -1 gives reverse transform
        Formula: forward
                 N-1
                  ---
                1 \         - j k 2 pi n / N
        X(K) = --- > x(n) e                  = Forward transform
                N /                            n=0..N-1
                  ---
                 n=0
        Formula: reverse
                 N-1
                 ---
                 \          j k 2 pi n / N
        X(n) =    > x(k) e                  = Inverse transform
                 /                             k=0..N-1
                 ---
                 k=0
        */
    private void FFT1D(int dir, int m, ref double[] x, ref double[] y)
    {
        /* Calculate the number of points */
        long nn = 1;

        for (var i = 0; i < m; i++)
        {
            nn *= 2;
        }

        /* Do the bit reversal */
        var i2 = nn >> 1;
        long j = 0;

        for (var i = 0; i < nn - 1; i++)
        {
            if (i < j)
            {
                var tx = x[i];
                var ty = y[i];
                x[i] = x[j];
                y[i] = y[j];
                x[j] = tx;
                y[j] = ty;
            }

            var k = i2;

            while (k <= j)
            {
                j -= k;
                k >>= 1;
            }

            j += k;
        }

        /* Compute the FFT */
        var c1 = -1.0;
        var c2 = 0.0;
        long l2 = 1;

        for (var l = 0; l < m; l++)
        {
            var l1 = l2;
            l2 <<= 1;
            var u1 = 1.0;
            var u2 = 0.0;

            for (j = 0; j < l1; j++)
            {
                for (var i = j; i < nn; i += l2)
                {
                    var i1 = i + l1;
                    var t1 = u1 * x[i1] - u2 * y[i1];
                    var t2 = u1 * y[i1] + u2 * x[i1];
                    x[i1] = x[i] - t1;
                    y[i1] = y[i] - t2;
                    x[i] += t1;
                    y[i] += t2;
                }

                var z = u1 * c1 - u2 * c2;
                u2 = u1 * c2 + u2 * c1;
                u1 = z;
            }

            c2 = Math.Sqrt((1.0 - c1) / 2.0);

            if (dir == 1)
            {
                c2 = -c2;
            }

            c1 = Math.Sqrt((1.0 + c1) / 2.0);
        }

        /* Scaling for forward transform */
        if (dir == 1)
        {
            for (var i = 0; i < nn; i++)
            {
                x[i] /= nn;
                y[i] /= nn;
            }
        }


        //  return(true) ;
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

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                var magnitude = input[x, y].Magnitude;
                if (magnitude > max) max = magnitude;
            }
        }

        var param = 255 / max;

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                input[x, y] = new Complex(input[x, y].Magnitude * param, 0);
            }
        }

        return input;
    }

    public Complex[][,] Normalize(Complex[][,] input)
    {
        double max = 0;

        for (var z = 0; z < 3; z++)
        {
            for (var x = 0; x < input[z].GetLength(0); x++)
            {
                for (var y = 0; y < input[z].GetLength(1); y++)
                {
                    var magnitude0 = input[z][x, y].Magnitude;
                    if (magnitude0 > 9999) continue;
                    if (magnitude0 > max) max = magnitude0;
                }
            }
        }

        var param = 255 / max;

        var output = new Complex[3][,];

        for (var z = 0; z < 3; z++)
        {
            var outputz = new Complex[input[0].GetLength(0), input[0].GetLength(1)];

            for (var x = 0; x < input[0].GetLength(0); x++)
            {
                for (var y = 0; y < input[0].GetLength(1); y++)
                {
                    var value = input[z][x, y].Magnitude;
                    if (value > 9999) value = max;
                    outputz[x, y] = new Complex(value * param, 0);
                }
            }

            output[z] = outputz;
        }

        return output;
    }

    private Complex[,] RemoveFFTShift(Complex[,] shifted)
    {
        var nx = shifted.GetLength(0);
        var ny = shifted.GetLength(1);

        int i, j;
        var result = new Complex[nx, ny];

        for (i = 0; i <= nx / 2 - 1; i++)
        {
            for (j = 0; j <= ny / 2 - 1; j++)
            {
                result[i + nx / 2, j + ny / 2] = shifted[i, j];
                result[i, j] = shifted[i + nx / 2, j + ny / 2];
                result[i + nx / 2, j] = shifted[i, j + ny / 2];
                result[i, j + nx / 2] = shifted[i + nx / 2, j];
            }
        }

        return result;
    }
}