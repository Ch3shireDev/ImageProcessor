using OpenCvSharp;

namespace ImageProcessorCLI;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var src = new Mat(@"lion.jpg", ImreadModes.AnyDepth | ImreadModes.AnyColor))
        using (var dst = new Mat())
        {
            src.CopyTo(dst);

            double[] data = { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            var kernel = new Mat(1, 21, MatType.CV_64FC1, data);

            Cv2.Normalize(kernel, kernel, 1.0, 0, NormTypes.L1);

            double sum = 0;
            foreach (var item in data) sum += Math.Abs(item);
            Console.WriteLine(sum); // => .999999970197678

            Cv2.Filter2D(src, dst, MatType.CV_64FC1, kernel, new Point(0, 0));

            using (new Window("src", src))
            using (new Window("dst", dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}