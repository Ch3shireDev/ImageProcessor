using System.Numerics;
using FFTW.NET;

namespace ImageProcessorLibrary.Services.FourierServices;

/// <summary>
///     Serwis dostarczający transformację Fouriera.
/// </summary>
public class FourierService : IFourierService
{
    /// <summary>
    ///     Transformacja Fouriera w jednym wymiarze.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Complex[] FFT1D(Complex[] x, FourierDirection dir = FourierDirection.Forward)
    {
        var y = new Complex[x.Length];

        using var pinIn = new PinnedArray<Complex>(x);
        using var pinOut = new PinnedArray<Complex>(y);

        if (dir == FourierDirection.Forward)
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

    /// <summary>
    ///     Transformacja Fouriera w dwóch wymiarach.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="dir"></param>
    /// <returns></returns>
    public Complex[,] FFT2D(Complex[,] c, FourierDirection dir = FourierDirection.Forward)
    {
        var y = new Complex[c.GetLength(0), c.GetLength(1)];

        using var pinIn = new PinnedArray<Complex>(c);
        using var pinOut = new PinnedArray<Complex>(y);

        if (dir == FourierDirection.Forward)
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

    /// <summary>
    ///     Normalizacja wyniku transformacji Fouriera w jednym wymiarze.
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    private static Complex[] Normalize(Complex[] y)
    {
        for (var i = 0; i < y.Length; i++)
        {
            y[i] /= y.Length;
        }

        return y;
    }

    /// <summary>
    ///     Normalizacja wyniku transformacji Fouriera w dwóch wymiarach.
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
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