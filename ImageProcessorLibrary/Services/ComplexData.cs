using System.Numerics;

namespace ImageProcessorLibrary.Services;

public class ComplexData
{
    public ComplexData()
    {
    }

    public ComplexData(ComplexData complexData)
    {
        Data = complexData.Data;
        Width = complexData.Width;
        Height = complexData.Height;
    }

    public ComplexData(byte[,] GreyImage)
    {
        var width = GreyImage.GetLength(1);
        var height = GreyImage.GetLength(0);
        var Fourier = new Complex [height, width];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                Fourier[i, j] = GreyImage[i, j];
            }
        }

        Data = new[] { Fourier, Fourier, Fourier };
        Width = width;
        Height = height;
    }

    public Complex[][,] Data { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Complex[,] this[int i] => Data[i];
}