namespace ImageProcessorTests;

public struct Complex
{
    public double Real { get; set; }
    public double Imag { get; set; }

    public Complex(double x, double y=0)
    {
        Real = x;
        Imag = y;
    }

    public double Magnitude => GetMagnitude();

    public double GetMagnitude()
    {
        return Math.Sqrt(Real * Real + Imag * Imag);
    }

    public double Phase()
    {
        return Math.Atan(Imag / Real);
    }
}