namespace ImageProcessorLibrary.DataStructures;

/// <summary>
///     Klasa reprezentująca wektor cech.
/// </summary>
public class FeatureVector
{
    public double M00 { get; set; }
    public double M10 { get; set; }
    public double M01 { get; set; }
    public double M20 { get; set; }
    public double M11 { get; set; }
    public double M02 { get; set; }
    public double SurfaceArea { get; set; }
    public double Circumference { get; set; }
    public double Solidity { get; set; }
    public double EquivalentDiameter { get; set; }
    public double W1 { get; set; }
    public double W2 { get; set; }
    public double W3 { get; set; }
    public double W9 { get; set; }
}