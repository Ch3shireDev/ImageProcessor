﻿using System.Drawing;
using System.Numerics;
using ImageProcessorLibrary.DataStructures;

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
                Fourier[i, j] = (double)GreyImage[i, j];
            }
        }

        Data = new[] { Fourier, Fourier, Fourier };
        Width = width;
        Height = height;
    }


    public ComplexData(IImageData GreyImage)
    {
        var threeFourier = new Complex[3][,];

        for (var k = 0; k < 3; k++)
        {
            threeFourier[k] = ToComplexData(GetChannel(k, GreyImage.Pixels));
        }

        Data = threeFourier;
        Width = GreyImage.Width;
        Height = GreyImage.Height;
    }

    public Complex[][,] Data { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }

    public Complex[,] this[int i] => Data[i];

    public ImageData ToImageData()
    {
        var red = Data[0];
        var green = Data[1];
        var blue = Data[2];

        var colors = new Color[red.GetLength(0), red.GetLength(1)];

        for (var y = 0; y < red.GetLength(0); y++)
        {
            for (var x = 0; x < red.GetLength(1); x++)
            {
                var redB = ComplexToColorByte(red[y, x]);
                var greenB = ComplexToColorByte(green[y, x]);
                var blueB = ComplexToColorByte(blue[y, x]);

                colors[y, x] = Color.FromArgb(redB, greenB, blueB);
            }
        }

        return new ImageData(colors);
    }

    static byte ComplexToColorByte(Complex value)
    {
        var magnitude = value.Magnitude;
        if (magnitude > 255) return 255;
        if (magnitude < 0) return 0;
        return (byte)magnitude;
    }

    private Complex[,] ToComplexData(byte[,] GreyImage)
    {
        var width = GreyImage.GetLength(1);
        var height = GreyImage.GetLength(0);
        var Fourier = new Complex[height, width];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                Fourier[i, j] = (double)GreyImage[i, j];
            }
        }

        return Fourier;
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
}