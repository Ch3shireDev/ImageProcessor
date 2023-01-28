namespace ImageProcessorLibrary.DataStructures;

/// <summary>
///     Struktura reprezentująca kolor w przestrzeni HSL.
/// </summary>
public class HSL
{
    public HSL()
    {
    }

    public HSL(int h, double s, double l)
    {
        H = h;
        S = s;
        L = l;
    }

    public int H { get; set; }

    public double S { get; set; }

    public double L { get; set; }
}