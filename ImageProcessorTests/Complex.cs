namespace ImageProcessorTests;

public struct Complex
{
    public double Real { get; set; }
    public double Imag { get; set; }

    public Complex(double x, double y)
    {
        Real = x;
        Imag = y;
    }

    public float Magnitude()
    {
        return (float)Math.Sqrt(Real * Real + Imag * Imag);
    }

    public float Phase()
    {
        return (float)Math.Atan(Imag / Real);
    }
}