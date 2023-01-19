using System.Drawing;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

public class FftService
{
    public Complex[,] InverseFFT(Complex[,] Fourier)
    {
        var nx = Fourier.GetLength(0);
        var ny = Fourier.GetLength(1);

        return FFT2D(Fourier, nx, ny, -1);
    }

    public Complex[,] ForwardFFT(Complex[,] Fourier)
    {
        var nx = Fourier.GetLength(0);
        var ny = Fourier.GetLength(1);

        return FFT2D(Fourier, nx, ny, 1);
    }

    public Complex[][,] InverseFFT(Complex[][,] Fourier)
    {
        var red = InverseFFT(Fourier[0]);
        var green = InverseFFT(Fourier[1]);
        var blue = InverseFFT(Fourier[2]);

        return new[] { red, green, blue };
    }

    public Complex[][,] ForwardFFT(Complex[][,] Fourier)
    {
        var red = ForwardFFT(Fourier[0]);
        var green = ForwardFFT(Fourier[1]);
        var blue = ForwardFFT(Fourier[2]);

        return new[] { red, green, blue };
    }


    public ImageData ToImageData(Complex[,] input)
    {
        var array = new byte[input.GetLength(0), input.GetLength(1)];

        for (var y = 0; y < array.GetLength(0); y++)
        {
            for (var x = 0; x < array.GetLength(1); x++)
            {
                var value = input[y, x].Magnitude();
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
                colors[y, x] = Color.FromArgb((byte)red[y, x].Magnitude(), (byte)green[y, x].Magnitude(), (byte)blue[y, x].Magnitude());
            }
        }

        return new ImageData(colors);
    }


    public Complex[,] ToComplexData(byte[,] GreyImage)
    {
        var width = GreyImage.GetLength(0);
        var height = GreyImage.GetLength(1);
        var Fourier = new Complex [width, height];

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                Fourier[i, j].Real = GreyImage[i, j];
                Fourier[i, j].Imag = 0;
            }
        }

        return Fourier;
    }

    public Complex[][,] ToComplexData(IImageData GreyImage)
    {
        var threeFourier = new Complex[3][,];

        for (var k = 0; k < 3; k++)
        {
            threeFourier[k] = ToComplexData(GetChannel(k, GreyImage.Pixels));
        }

        return threeFourier;
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
        The size of the array (nx,ny)
        Return false if there are memory problems or
        the dimensions are not powers of 2
    */

    public Complex[,] ChangeSize(Complex[,] input, int nx, int ny)
    {
        var output = new Complex[nx, ny];

        for (var i = 0; i < nx; i++)
        {
            for (var j = 0; j < ny; j++)
            {
                if (i >= input.GetLength(0) || j >= input.GetLength(1)) output[i, j] = new Complex(0, 0);
                else output[i, j] = input[i, j];
            }
        }

        return output;
    }

    public Complex[][,] ChangeSize(Complex[][,] input, int nx, int ny)
    {
        var red = ChangeSize(input[0], nx, ny);
        var green = ChangeSize(input[1], nx, ny);
        var blue = ChangeSize(input[2], nx, ny);

        return new[] { red, green, blue };
    }

    public Complex[,] ChangeSizeToClosestPowerOfTwo(Complex[,] input)
    {
        var nx = input.GetLength(0);
        var ny = input.GetLength(1);

        var newNx = (int)Math.Pow(2, Math.Ceiling(Math.Log(nx, 2)));
        var newNy = (int)Math.Pow(2, Math.Ceiling(Math.Log(ny, 2)));

        return ChangeSize(input, newNx, newNy);
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
        int y = (int)Math.Pow(2, x);
        if (y == n) return n;
        return (int)Math.Pow(2, x + 1);
    }


    public Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
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
                imag[i] = c[i, j].Imag;
            }

            // Calling 1D FFT Function for Rows
            m = (int)Math.Log(nx, 2); //Finding power of 2 for current number of points e.g. for nx=512 m=9
            FFT1D(dir, m, ref real, ref imag);

            for (i = 0; i < nx; i++)
            {
                output[i, j].Real = real[i];
                output[i, j].Imag = imag[i];
            }
        }

        // Transform the columns  
        real = new double[ny];
        imag = new double[ny];

        for (i = 0; i < nx; i++)
        {
            for (j = 0; j < ny; j++)
            {
                //Real[j] = c[i,j].Real;
                //Imag[j] = c[i,j].Imag;
                real[j] = output[i, j].Real;
                imag[j] = output[i, j].Imag;
            }

            // Calling 1D FFT Function for Columns


            m = (int)Math.Log(ny, 2); //Finding power of 2 for current number of points e.g. for nx=512 m=9


            FFT1D(dir, m, ref real, ref imag);

            for (j = 0; j < ny; j++)
            {
                //c[i,j].Real = Real[j];
                //c[i,j].Imag = Imag[j];
                output[i, j].Real = real[j];
                output[i, j].Imag = imag[j];
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
    public void FFT1D(int dir, int m, ref double[] x, ref double[] y)
    {
        /* Calculate the number of points */
        long nn = 1;

        for (var i = 0; i < m; i++)
        {
            nn *= 2;
        }

        /* Do the bit reversal */
        long i2 = nn >> 1;
        long j = 0;

        for (var i = 0; i < nn - 1; i++)
        {
            if (i < j)
            {
                double tx = x[i];
                double ty = y[i];
                x[i] = x[j];
                y[i] = y[j];
                x[j] = tx;
                y[j] = ty;
            }

            long k = i2;

            while (k <= j)
            {
                j -= k;
                k >>= 1;
            }

            j += k;
        }

        /* Compute the FFT */
        double c1 = -1.0;
        double c2 = 0.0;
        long l2 = 1;

        for (var l = 0; l < m; l++)
        {
            long l1 = l2;
            l2 <<= 1;
            double u1 = 1.0;
            double u2 = 0.0;

            for (j = 0; j < l1; j++)
            {
                for (var i = j; i < nn; i += l2)
                {
                    long i1 = i + l1;
                    double t1 = u1 * x[i1] - u2 * y[i1];
                    double t2 = u1 * y[i1] + u2 * x[i1];
                    x[i1] = x[i] - t1;
                    y[i1] = y[i] - t2;
                    x[i] += t1;
                    y[i] += t2;
                }

                double z = u1 * c1 - u2 * c2;
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

    public Complex[,] Normalize(Complex[,] input)
    {
        double max = 0;

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                var magnitude = input[x, y].Magnitude();
                if (magnitude > max) max = magnitude;
            }
        }

        var param = 255 / Math.Sqrt(2) / max;

        for (var x = 0; x < input.GetLength(0); x++)
        {
            for (var y = 0; y < input.GetLength(1); y++)
            {
                input[x, y] = new Complex(input[x, y].Real * param, input[x, y].Imag * param);
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
                    var magnitude0 = input[z][x, y].Magnitude();
                    if (magnitude0 > max) max = magnitude0;
                }
            }
        }

        var param = 255 / Math.Sqrt(2) / max;

        for (var z = 0; z < 3; z++)
        {
            for (var x = 0; x < input[0].GetLength(0); x++)
            {
                for (var y = 0; y < input[0].GetLength(1); y++)
                {
                    input[z][x, y] = new Complex(input[z][x, y].Real * param, input[z][x, y].Imag * param);
                }
            }
        }

        return input;
    }

    public Complex[,] RemoveFFTShift(Complex[,] FFTShifted)
    {
        var nx = FFTShifted.GetLength(0);
        var ny = FFTShifted.GetLength(1);

        int i, j;
        var FFTNormal = new Complex[nx, ny];

        for (i = 0; i <= nx / 2 - 1; i++)
        for (j = 0; j <= ny / 2 - 1; j++)
        {
            FFTNormal[i + nx / 2, j + ny / 2] = FFTShifted[i, j];
            FFTNormal[i, j] = FFTShifted[i + nx / 2, j + ny / 2];
            FFTNormal[i + nx / 2, j] = FFTShifted[i, j + ny / 2];
            FFTNormal[i, j + nx / 2] = FFTShifted[i + nx / 2, j];
        }

        return FFTNormal;
    }
}