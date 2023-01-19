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

    //private T[,] GetSlice<T>(T[,,] array, int index)
    //{
    //    var result = new T[array.GetLength(1), array.GetLength(2)];
    //    for (var x = 0; x < array.GetLength(1); x++)
    //    {
    //        for (var y = 0; y < array.GetLength(2); y++)
    //        {
    //            result[x, y] = array[index, x, y];
    //        }
    //    }

    //    return result;
    //}

    //private T[,,] Combine<T>(params T[][,] array)
    //{
    //    var n0 = array.GetLength(0);
    //    var n1 = array[0].GetLength(0);
    //    var n2 = array[0].GetLength(1);

    //    var result = new T[n0, n1, n2];

    //    for (var x = 0; x < n0; x++)
    //    {
    //        for (var y = 0; y < n1; y++)
    //        {
    //            for (var z = 0; z < n2; z++)
    //            {
    //                result[x, y, z] = array[x][y, z];
    //            }
    //        }
    //    }
    //}

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
                if (value < 0) value = 0;
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
                colors[y, x] = Color.FromArgb((byte)red[y, x].Real, (byte)green[y, x].Real, (byte)blue[y, x].Real);
            }
        }

        return new ImageData(colors);
    }


    public Complex[,] ToComplexData(byte[,] GreyImage)
    {
        var width = GreyImage.GetLength(0);
        var height = GreyImage.GetLength(1);
        var Fourier = new Complex [width, height];

        //Copy Image Data to the Complex Array
        for (var i = 0; i <= width - 1; i++)
        for (var j = 0; j <= height - 1; j++)
        {
            Fourier[i, j].Real = GreyImage[i, j];
            Fourier[i, j].Imag = 0;
        }

        return Fourier;
    }

    public Complex[][,] ToComplexData(IImageData GreyImage)
    {
        var width = GreyImage.Width;
        var height = GreyImage.Height;
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
                //  c[i,j].Real = Real[i];
                //  c[i,j].Imag = Imag[i];
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
        long nn, i, i1, j, k, i2, l, l1, l2;
        double c1, c2, tx, ty, t1, t2, u1, u2, z;
        /* Calculate the number of points */
        nn = 1;

        for (i = 0; i < m; i++)
        {
            nn *= 2;
        }

        /* Do the bit reversal */
        i2 = nn >> 1;
        j = 0;

        for (i = 0; i < nn - 1; i++)
        {
            if (i < j)
            {
                tx = x[i];
                ty = y[i];
                x[i] = x[j];
                y[i] = y[j];
                x[j] = tx;
                y[j] = ty;
            }

            k = i2;

            while (k <= j)
            {
                j -= k;
                k >>= 1;
            }

            j += k;
        }

        /* Compute the FFT */
        c1 = -1.0;
        c2 = 0.0;
        l2 = 1;

        for (l = 0; l < m; l++)
        {
            l1 = l2;
            l2 <<= 1;
            u1 = 1.0;
            u2 = 0.0;

            for (j = 0; j < l1; j++)
            {
                for (i = j; i < nn; i += l2)
                {
                    i1 = i + l1;
                    t1 = u1 * x[i1] - u2 * y[i1];
                    t2 = u1 * y[i1] + u2 * x[i1];
                    x[i1] = x[i] - t1;
                    y[i1] = y[i] - t2;
                    x[i] += t1;
                    y[i] += t2;
                }

                z = u1 * c1 - u2 * c2;
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
            for (i = 0; i < nn; i++)
            {
                x[i] /= nn;
                y[i] /= nn;
            }
        }


        //  return(true) ;
    }
}