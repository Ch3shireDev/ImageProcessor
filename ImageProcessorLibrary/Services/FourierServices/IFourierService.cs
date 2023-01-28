using System.Numerics;

namespace ImageProcessorLibrary.Services.FourierServices;

/// <summary>
///     Interfejs do obsługi transformacji Fouriera.
/// </summary>
public interface IFourierService
{
    Complex[,] FFT2D(Complex[,] c, FourierDirection dir = FourierDirection.Forward);
    Complex[] FFT1D(Complex[] x, FourierDirection dir = FourierDirection.Forward);
}