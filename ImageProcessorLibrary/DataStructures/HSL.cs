namespace ImageProcessorLibrary.DataStructures;

/// <summary>
///     Struktura reprezentująca kolor w przestrzeni HSL.
/// </summary>
public class HSL
{
    /// <summary>
    ///    Konstruktor domyślny.
    /// </summary>
    public HSL()
    {
    }

    /// <summary>
    ///    Konstruktor.
    /// </summary>
    /// <param name="h">Barwa w postaci kąta (od 0 do 360).</param>
    /// <param name="s">Nasycenie (od 0 do 1).</param>
    /// <param name="l">Oświetlenie (od 0 do 1).</param>
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