using System.Numerics;

namespace ImageProcessorLibrary.Services;

public interface IFourierService
{
    Complex[,] FFT2D(Complex[,] c, int dir);
}