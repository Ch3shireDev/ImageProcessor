using System.Numerics;

namespace ImageProcessorLibrary.Services;

public class FourierService : IFourierService
{
    public Complex[,] FFT2D(Complex[,] c, int dir)
    {
        c = TransformColumns(c, dir);
        c = TransformRows(c, dir);

        return c;
    }

    private static Complex[,] TransformRows(Complex[,] c, int dir)
    {
        for (var i = 0; i < c.GetLength(1); i++)
        {
            var dataY = SliceRow(c, i);
            var res = FFT1D(dataY, dir);
            c = AssignRow(c, res, i);
        }

        return c;
    }

    private static Complex[,] TransformColumns(Complex[,] c, int dir)
    {
        for (var j = 0; j < c.GetLength(0); j++)
        {
            var dataX = SliceColumn(c, j);
            var res = FFT1D(dataX, dir);
            c = AssignColumn(c, res, j);
        }

        return c;
    }

    private static Complex[,] AssignRow(Complex[,] c, Complex[] res, int i)
    {
        for (var j = 0; j < c.GetLength(0); j++)
        {
            c[i, j] = res[j];
        }

        return c;
    }

    private static Complex[,] AssignColumn(Complex[,] c, Complex[] res, int j)
    {
        for (var i = 0; i < c.GetLength(1); i++)
        {
            c[i, j] = res[i];
        }

        return c;
    }

    private static Complex[] SliceRow(Complex[,] c, int i)
    {
        var dataY = new Complex[c.GetLength(0)];

        for (var j = 0; j < c.GetLength(0); j++)
        {
            dataY[j] = c[i, j];
        }

        return dataY;
    }

    private static Complex[] SliceColumn(Complex[,] c, int j)
    {
        var dataX = new Complex[c.GetLength(1)];

        for (var i = 0; i < c.GetLength(1); i++)
        {
            dataX[i] = c[i, j];
        }

        return dataX;
    }


    private static Complex[] FFT1D(Complex[] c, int dir)
    {

        /* Do the bit reversal */
        var i2 = c.Length >> 1;
        long j = 0;

        for (var i = 0; i < c.Length - 1; i++)
        {
            if (i < j)
            {
                (c[i], c[j]) = (c[j], c[i]);
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
        
        var m = (int)Math.Log(c.Length, 2);
        for (var l = 0; l < m; l++)
        {
            var l1 = l2;
            l2 <<= 1;
            var u1 = 1.0;
            var u2 = 0.0;

            for (j = 0; j < l1; j++)
            {
                for (var i = j; i < c.Length; i += l2)
                {
                    var i1 = i + l1;
                    var t1 = u1 * c[i1].Real - u2 * c[i1].Imaginary;
                    var t2 = u1 * c[i1].Imaginary + u2 * c[i1].Real;
                    c[i1] = c[i] - new Complex(t1, t2);
                    c[i] += new Complex(t1, t2);
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
            for (var i = 0; i < (long)c.Length; i++)
            {
                c[i] /= c.Length;
            }
        }

        return c;
    }
}