using ImageProcessorGUI.Models;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class ImageModelTests
{
    private MockDuplicateImageService _duplicateImageService;
    private MockHistogramService _histogramService;
    private MockOpenImageService _openImageService;
    private MockSaveImageService _saveImageService;
    private MockWindowService _windowService;
    private ImageModel model;

    [TestInitialize]
    public void TestInitialize()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        var imageData = new ImageData("lion.jpg", bytes);

        _openImageService = new MockOpenImageService();
        _saveImageService = new MockSaveImageService();
        _duplicateImageService = new MockDuplicateImageService();
        _windowService = new MockWindowService();
        _histogramService = new MockHistogramService();

        var serviceProvider = new ServiceProvider
        {
            OpenImageService = _openImageService,
            SaveImageService = _saveImageService,
            DuplicateImageService = _duplicateImageService,
            WindowService = _windowService,
            HistogramService = _histogramService
        };

        model = new ImageModel(imageData, serviceProvider);
    }

    /// <summary>
    ///     Po kliknięciu w menu "File" -> "Open" wywoływana jest metoda OpenImage(). To powinno poskutkować otwarciem okna
    ///     wyboru pliku, po czym powinno pojawić się nowe okno z wybranym obrazem.
    /// </summary>
    [TestMethod]
    public void OpenImageTest()
    {
        Assert.IsFalse(_openImageService.IsOpen);
        model.OpenImage();
        Assert.IsTrue(_openImageService.IsOpen);
    }

    /// <summary>
    ///     Po kliknięciu w menu "File -> Save" wywoływana jest metoda SaveImageAsync(). Skutkuje otwarciem okna wyboru
    ///     ścieżki i
    ///     zapisu pod daną ścieżką pliku.
    /// </summary>
    [TestMethod]
    public async Task SaveImageTest()
    {
        Assert.IsNull(_saveImageService.ImageData);
        Assert.IsFalse(_saveImageService.IsSaved);
        await model.SaveImage();
        Assert.IsTrue(_saveImageService.IsSaved);
        Assert.IsNotNull(_saveImageService.ImageData);
    }

    /// <summary>
    ///     Po kliknięciu w menu "File -> Duplicate" wywoływana jest metoda DuplicateImage(). Powinna ona skutkować duplikacją
    ///     obrazu do kolejnego okna.
    /// </summary>
    [TestMethod]
    public void DuplicateImageTest()
    {
        Assert.IsFalse(_duplicateImageService.IsDuplicated);
        Assert.IsNull(_duplicateImageService.ImageData);
        model.DuplicateImage();
        Assert.IsTrue(_duplicateImageService.IsDuplicated);
        Assert.IsNotNull(_duplicateImageService.ImageData);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Histogram -> Value" wywoływana jest metoda ShowValueHistogram(). Powinna ona skutkować
    ///     wyświetleniem histogramu wartości.
    /// </summary>
    [TestMethod]
    public void ShowValueHistogramTest()
    {
        Assert.IsFalse(_windowService.IsShowImageWindowCalled);
        Assert.IsFalse(_histogramService.IsShowValueHistogramCalled);
        model.ShowValueHistogram();
        Assert.IsTrue(_windowService.IsShowImageWindowCalled);
        Assert.IsTrue(_histogramService.IsShowValueHistogramCalled);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Histogram -> RGB" wywoływana jest metoda ShowRgbHistogram(). Powinna ona skutkować
    ///     wyświetleniem histogramu RGB.
    /// </summary>
    [TestMethod]
    public void ShowRgbHistogramTest()
    {
        Assert.IsFalse(_windowService.IsShowImageWindowCalled);
        Assert.IsFalse(_histogramService.IsShowRgbHistogramCalled);
        model.ShowRgbHistogram();
        Assert.IsTrue(_windowService.IsShowImageWindowCalled);
        Assert.IsTrue(_histogramService.IsShowRgbHistogramCalled);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Histogram -> R" wywoływana jest metoda ShowRHistogram(). Powinna ona skutkować wyświetleniem
    ///     histogramu R.
    /// </summary>
    [TestMethod]
    public void ShowRHistogramTest()
    {
        Assert.IsFalse(_windowService.IsShowImageWindowCalled);
        Assert.IsFalse(_histogramService.IsShowRHistogramCalled);
        model.ShowRHistogram();
        Assert.IsTrue(_windowService.IsShowImageWindowCalled);
        Assert.IsTrue(_histogramService.IsShowRHistogramCalled);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Histogram -> G" wywoływana jest metoda ShowGHistogram(). Powinna ona skutkować wyświetleniem
    ///     histogramu G.
    /// </summary>
    [TestMethod]
    public void ShowGHistogramTest()
    {
        Assert.IsFalse(_windowService.IsShowImageWindowCalled);
        Assert.IsFalse(_histogramService.IsShowGHistogramCalled);
        model.ShowGHistogram();
        Assert.IsTrue(_windowService.IsShowImageWindowCalled);
        Assert.IsTrue(_histogramService.IsShowGHistogramCalled);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Histogram -> B" wywoływana jest metoda ShowBHistogram(). Powinna ona skutkować wyświetleniem
    ///     histogramu B.
    /// </summary>
    [TestMethod]
    public void ShowBHistogramTest()
    {
        Assert.IsFalse(_windowService.IsShowImageWindowCalled);
        Assert.IsFalse(_histogramService.IsShowBHistogramCalled);
        model.ShowBHistogram();
        Assert.IsTrue(_windowService.IsShowImageWindowCalled);
        Assert.IsTrue(_histogramService.IsShowBHistogramCalled);
    }

    //[TestMethod]
    //public void ShowScaledUp200PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ShowScaledUp150PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ShowScaledDown50PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ShowScaledDown25PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ShowScaledDown20PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ShowScaledDown10PercentTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void LinearStretchingTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void GammaStretchingTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EqualizeHistogramTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void NegateImageTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void BinaryThresholdTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void GreyscaleThresholdTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void AddImageTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void AddImageWithoutSaturateTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ImagesDifferenceTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void AddNumberToImageTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void SubtractNumberFromImageTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void BinaryAndTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void BinaryOrTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void BinaryXorTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void BinaryNotTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void GetBinaryMaskTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void Get8BitMaskTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void GaussianBlurTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelEastTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelNorthEastTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelNorthTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelNorthWestTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelWestTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelSouthWestTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelSouthTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void EdgeSobelSouthEastTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void FillBorderConstantTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void FillResultBorderConstantTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void FillBorderReflectTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void FillBorderWrapTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CalculateMedian3x3Test()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CalculateMedian5x5Test()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CalculateMedian7x7Test()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CalculateMedian9x9Test()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void SobelEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void PrewittEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CannyOperatorEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void OtsuSegmentationTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void AdaptativeThresholdSegmentationTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void MorphologyErosionTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void MorphologyDilationTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void MorphologyOpeningTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void MorphologyClosingTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void CalculateFeatureVectorTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void ModifyAmplitudeSpectrumTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void MedianBlurTest()
    //{
    //    Assert.Fail();
    //}

    //[TestMethod]
    //public void SharpeningTest()
    //{
    //    Assert.Fail();
    //}
}