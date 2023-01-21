using System.Numerics;

namespace ImageProcessorLibrary.Services;

public class FourierService : IFourierService
{
    public Complex[,] FFT2D(Complex[,] c, int nx, int ny, int dir)
    {
        var data = new Complex[nx];

        for (var j = 0; j < ny; j++)
        {
            for (var i = 0; i < nx; i++)
            {
                data[i] = c[i, j];
            }

            var m = (int)Math.Log(nx, 2);
            
            var res = FFT1D(dir, m, data);

            for (var i = 0; i < nx; i++)
            {
                c[i, j] = res[i];
            }
        }
        
        var data2 = new Complex[ny];

        for (var i = 0; i < nx; i++)
        {
            for (var j = 0; j < ny; j++)
            {
                data2[j] = c[i, j];
            }

            var m = (int)Math.Log(ny, 2);


            var res = FFT1D(dir, m, data2);

            for (var j = 0; j < ny; j++)
            {
                c[i, j] = res[j];
            }
        }

        return c;
    }
    

    private static Complex[] FFT1D(int dir, int m, Complex[] z)
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
                (z[i], z[j]) = (z[j], z[i]);
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
                    var t1 = u1 * z[i1].Real - u2 * z[i1].Imaginary;
                    var t2 = u1 * z[i1].Imaginary + u2 * z[i1].Real;
                    z[i1] = z[i] - new Complex(t1, t2);
                    z[i] += new Complex(t1, t2);
                }

                var z2 = u1 * c1 - u2 * c2;
                u2 = u1 * c2 + u2 * c1;
                u1 = z2;
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
                z[i] /= nn;
            }
        }

        return z;
    }
}