using ImageProcessorGUI.Models;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class MenuModelTests
{
    private MockFileService FileService;
    private MenuModel model;

    [TestInitialize]
    public void TestInitialize()
    {
        FileService = new MockFileService();

        var serviceProvider = new ServiceProvider
        {
            FileService = FileService
        };

        model = new MenuModel(serviceProvider);
    }

    /// <summary>
    ///     Po kliknięciu w menu "File" -> "Open" wywoływana jest metoda OpenImage(). To powinno poskutkować otwarciem okna
    ///     wyboru pliku, po czym powinno pojawić się nowe okno z wybranym obrazem.
    /// </summary>
    [TestMethod]
    public void OpenImageTest()
    {
        Assert.IsFalse(FileService.IsOpen);
        model.OpenImage();
        Assert.IsTrue(FileService.IsOpen);
    }
    
    /// <summary>
    ///     Po kliknięciu w menu "File -> "Save" wywoływana jest metoda SaveImage(). Skutkuje otwarciem okna wyboru ścieżki i zapisu pod daną ścieżką pliku.
    /// </summary>
    [TestMethod]
    public void SaveImageTest()
    {
        Assert.IsFalse(FileService.IsSaved);
        model.SaveImage();
        Assert.IsTrue(FileService.IsSaved);
    }

    [TestMethod]
    public void DuplicateImageTest()
    {
        Assert.IsFalse(FileService.IsDuplicated);
        model.DuplicateImage();
        Assert.IsTrue(FileService.IsDuplicated);
    }

    [TestMethod]
    public void ShowValueHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowRgbHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowRHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowGHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowBHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledUp200PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledUp150PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledDown50PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledDown25PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledDown20PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ShowScaledDown10PercentTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void LinearStretchingTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GammaStretchingTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EqualizeHistogramTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void NegateImageTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void BinaryThresholdTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GreyscaleThresholdTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void AddImageTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void AddImageWithoutSaturateTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ImagesDifferenceTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void AddNumberToImageTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void SubtractNumberFromImageTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void BinaryAndTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void BinaryOrTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void BinaryXorTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void BinaryNotTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GetBinaryMaskTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void Get8BitMaskTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void GaussianBlurTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelEastTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelNorthEastTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelNorthTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelNorthWestTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelWestTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelSouthWestTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelSouthTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void EdgeSobelSouthEastTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FillBorderConstantTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FillResultBorderConstantTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FillBorderReflectTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void FillBorderWrapTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CalculateMedian3x3Test()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CalculateMedian5x5Test()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CalculateMedian7x7Test()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CalculateMedian9x9Test()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void SobelEdgeDetectionTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void PrewittEdgeDetectionTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CannyOperatorEdgeDetectionTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void OtsuSegmentationTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void AdaptativeThresholdSegmentationTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void MorphologyErosionTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void MorphologyDilationTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void MorphologyOpeningTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void MorphologyClosingTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void CalculateFeatureVectorTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void ModifyAmplitudeSpectrumTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void MedianBlurTest()
    {
        Assert.Fail();
    }

    [TestMethod]
    public void SharpeningTest()
    {
        Assert.Fail();
    }
}