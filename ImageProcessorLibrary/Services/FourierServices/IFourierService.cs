using System.Numerics;

namespace ImageProcessorLibrary.Services.FourierServices;

public interface IFourierService
{
    Complex[,] FFT2D(Complex[,] c, int dir = 1);
    Complex[] FFT1D(Complex[] x, int dir = 1);
}