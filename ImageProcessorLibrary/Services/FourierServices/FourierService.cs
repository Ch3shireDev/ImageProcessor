using System.Numerics;
using FFTW.NET;

namespace ImageProcessorLibrary.Services.FourierServices;

public class FourierService : IFourierService
{
    public Complex[] FFT1D(Complex[] x, int dir = 1)
    {
        var y = new Complex[x.Length];

        using var pinIn = new PinnedArray<Complex>(x);
        using var pinOut = new PinnedArray<Complex>(y);

        if (dir == 1)
        {
            DFT.FFT(pinIn, pinOut);
            y = Normalize(y);
        }
        else
        {
            DFT.IFFT(pinIn, pinOut);
        }

        return y;
    }


    public Complex[,] FFT2D(Complex[,] c, int dir = 1)
    {
        var y = new Complex[c.GetLength(0), c.GetLength(1)];

        using var pinIn = new PinnedArray<Complex>(c);
        using var pinOut = new PinnedArray<Complex>(y);

        if (dir == 1)
        {
            DFT.FFT(pinIn, pinOut);
            y = Normalize(y);
        }
        else
        {
            DFT.IFFT(pinIn, pinOut);
        }

        return y;
    }

    private static Complex[] Normalize(Complex[] y)
    {
        for (var i = 0; i < y.Length; i++)
        {
            y[i] /= y.Length;
        }

        return y;
    }

    private static Complex[,] Normalize(Complex[,] y)
    {
        for (var i = 0; i < y.GetLength(0); i++)
        {
            for (var j = 0; j < y.GetLength(1); j++)
            {
                y[i, j] /= y.GetLength(0) * y.GetLength(1);
            }
        }

        return y;
    }
}