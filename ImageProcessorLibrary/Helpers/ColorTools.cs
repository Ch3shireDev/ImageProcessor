using System.Drawing;
using ImageProcessorLibrary.DataStructures;

namespace ImageProcessorLibrary.Helpers;

/// <summary>
///     Narzędzia do operacji na kolorach.
/// </summary>
public static class ColorTools
{
    /// <summary>
    ///     Konwersja z modelu RGB do HSL.
    /// </summary>
    /// <param name="rgb">Pixel w formacie RGB.</param>
    /// <returns>Piksel w formacie HSL.</returns>
    public static HSL RGBToHSL(Color rgb)
    {
        var r = rgb.R / 255.0f;
        var g = rgb.G / 255.0f;
        var b = rgb.B / 255.0f;

        var min = Math.Min(Math.Min(r, g), b);
        var max = Math.Max(Math.Max(r, g), b);
        var delta = max - min;

        var hsl = new HSL();

        hsl.L = (max + min) / 2;

        if (delta == 0)
        {
            hsl.H = 0;
            hsl.S = 0.0f;
        }
        else
        {
            hsl.S = hsl.L <= 0.5 ? delta / (max + min) : delta / (2 - max - min);

            double hue;

            if (r == max)
            {
                hue = (g - b) / 6 / delta;
            }
            else if (g == max)
            {
                hue = 1.0f / 3 + (b - r) / 6 / delta;
            }
            else
            {
                hue = 2.0f / 3 + (r - g) / 6 / delta;
            }

            if (hue < 0)
            {
                hue += 1;
            }

            if (hue > 1)
            {
                hue -= 1;
            }

            hsl.H = (int)(hue * 360);
        }

        return hsl;
    }

    /// <summary>
    ///     Konwersja z modelu HSL do RGB.
    /// </summary>
    /// <param name="hsl"></param>
    /// <returns></returns>
    public static Color HSLToRGB(HSL hsl)
    {
        if (hsl.S == 0)
        {
            var r = (byte)(hsl.L * 255);
            var g = (byte)(hsl.L * 255);
            var b = (byte)(hsl.L * 255);
            return Color.FromArgb(r, g, b);
        }
        else
        {
            var hue = (double)hsl.H / 360;

            var v2 = hsl.L < 0.5 ? hsl.L * (1 + hsl.S) : hsl.L + hsl.S - hsl.L * hsl.S;
            var v1 = 2 * hsl.L - v2;

            var r = (byte)(255 * HueToRGB(v1, v2, hue + 1.0f / 3));
            var g = (byte)(255 * HueToRGB(v1, v2, hue));
            var b = (byte)(255 * HueToRGB(v1, v2, hue - 1.0f / 3));
            return Color.FromArgb(r, g, b);
        }
    }

    /// <summary>
    ///     Konwersja wartości barwy na wartość RGB.
    /// </summary>
    /// <param name="v1"></param>
    /// <param name="v2"></param>
    /// <param name="vH"></param>
    /// <returns></returns>
    private static double HueToRGB(double v1, double v2, double vH)
    {
        if (vH < 0)
        {
            vH += 1;
        }

        if (vH > 1)
        {
            vH -= 1;
        }

        if (6 * vH < 1)
        {
            return v1 + (v2 - v1) * 6 * vH;
        }

        if (2 * vH < 1)
        {
            return v2;
        }

        if (3 * vH < 2)
        {
            return v1 + (v2 - v1) * (2.0f / 3 - vH) * 6;
        }

        return v1;
    }
}