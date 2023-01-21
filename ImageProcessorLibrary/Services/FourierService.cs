using System.Numerics;

namespace ImageProcessorLibrary.Services;

public class FourierService : IFourierService
{
    public Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
    {
        var real = new double[nx];
        var imag = new double[nx];

        var data = new Complex[nx];

        for (var j = 0; j < ny; j++)
        {
            for (var i = 0; i < nx; i++)
            {
                real[i] = c[i, j].Real;
                imag[i] = c[i, j].Imaginary;
            }

            var m = (int)Math.Log(nx, 2);
           var  res= FFT1D(dir, m, real, imag);
           real = res.Item1;
           imag = res.Item2;
            for (var i = 0; i < nx; i++)
            {
                c[i, j] = new Complex(real[i], imag[i]);
            }
        }

        var real2 = new double[ny];
        var imag2 = new double[ny];

        for (var i = 0; i < nx; i++)
        {
            for (var j = 0; j < ny; j++)
            {
                real2[j] = c[i, j].Real;
                imag2[j] = c[i, j].Imaginary;
            }

            var m = (int)Math.Log(ny, 2);


           var res= FFT1D(dir, m, real2, imag2);
           real2 = res.Item1;
           imag2 = res.Item2;
            for (var j = 0; j < ny; j++)
            {
                c[i, j] = new Complex(real2[j], imag2[j]);
            }
        }

        return c;
    }


    

    private Tuple<double[], double[]> FFT1D(int dir, int m, double[] x, double[] y)
    {
        
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

        if (dir == 1)
        {
            for (var i = 0; i < nn; i++)
            {
                x[i] /= nn;
                y[i] /= nn;
            }
        }

        return new Tuple<double[], double[]>(x, y);
    }
}