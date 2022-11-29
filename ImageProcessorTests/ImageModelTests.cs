using ImageProcessorGUI.Models;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

public class MockProcessService:IProcessService
{
    public bool IsNegated{ get; set; }
    public ImageData NegateImage(ImageData imageData)
    {
        IsNegated = true;
        return imageData;
    }

    public void OpenBinaryThresholdWindow(ImageData imageData)
    {
        IsBinaryThresholdWindowOpen = true;
    }

    public bool IsBinaryThresholdWindowOpen { get; set; }

    public void OpenGreyscaleThresholdOneSliderWindow(ImageData imageData)
    {
        IsGreyscaleThresholdWindowOpen = true;
    }

    public void OpenGreyscaleThresholdTwoSlidersWindow(ImageData imageData)
    {
        IsGreyscaleThresholdTwoSlidersWindowOpen = true;
    }

    public bool IsGreyscaleThresholdTwoSlidersWindowOpen { get; set; }

    public bool IsGreyscaleThresholdWindowOpen { get; set; }
}
public class MockStretchingOptionsWindowService:IStretchingOptionsService
{
    public bool IsLinearStretchingWindowShown { get; set; }
    public bool IsGammaStretchingWindowShown{ get; set; }
    public bool IsEqualizeHistogramWindowShown{ get; set; }
    public void ShowGammaStretchingWindow(ImageData imageData)
    {
        IsGammaStretchingWindowShown = true;
    }

    public ImageData GetEqualizedImage(ImageData imageData)
    {
        return imageData;
    }

    public void ShowLinearStretchingWindow(ImageData imageData)
    {
        IsLinearStretchingWindowShown = true;
    }
}

[TestClass]
public class ImageModelTests
{
    private MockDuplicateImageService _duplicateImageService;
    private MockHistogramService _histogramService;
    private MockOpenImageService _openImageService;
    private MockSaveImageService _saveImageService;
    private MockWindowService _windowService;
    private ImageModel model;
    private MockStretchingOptionsWindowService stretchingOptionsWindowService;
    private MockProcessService processService;
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
        stretchingOptionsWindowService = new MockStretchingOptionsWindowService();
        processService = new MockProcessService();
        
        var serviceProvider = new ServiceProvider
        {
            OpenImageService = _openImageService,
            SaveImageService = _saveImageService,
            DuplicateImageService = _duplicateImageService,
            WindowService = _windowService,
            HistogramService = _histogramService,
            StretchingOptionsService = stretchingOptionsWindowService,
            ProcessService = processService
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
    ///     Po kliknięciu w menu "Histogram -> LMin" wywoływana jest metoda ShowValueHistogram(). Powinna ona skutkować
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

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleUp200" wywoływana jest metoda ScaleUp200Percent(). Powinna ona skutkować zmianą
    ///     skali obrazka o 200%.
    /// </summary>
    [TestMethod]
    public void ShowScaledUp200PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledUp200Percent();
        Assert.AreEqual(770, model.ImageWidth);
        Assert.AreEqual(1000, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleDown50" wywoływana jest metoda ScaleUp150Percent(). Powinna ona skutkować
    ///     zmianą skali obrazka o 150%.
    /// </summary>
    [TestMethod]
    public void ShowScaledUp150PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledUp150Percent();
        Assert.AreEqual(577.5, model.ImageWidth);
        Assert.AreEqual(750, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleDown50" wywoływana jest metoda ScaleDown50Percent(). Powinna ona skutkować
    ///     zmianą skali obrazka do 50%.
    /// </summary>
    [TestMethod]
    public void ShowScaledDown50PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledDown50Percent();
        Assert.AreEqual(192.5, model.ImageWidth);
        Assert.AreEqual(250, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleDown25" wywoływana jest metoda ScaleDown25Percent(). Powinna ona skutkować
    ///     zmianą skali obrazka do 25%.
    /// </summary>
    [TestMethod]
    public void ShowScaledDown25PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledDown25Percent();
        Assert.AreEqual(96.25, model.ImageWidth);
        Assert.AreEqual(125, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleDown20Percent" wywoływana jest metoda ScaleDown20Percent(). Powinna ona
    ///     skutkować zmianą skali obrazka do 20%.
    /// </summary>
    [TestMethod]
    public void ShowScaledDown20PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledDown20Percent();
        Assert.AreEqual(77, model.ImageWidth);
        Assert.AreEqual(100, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu "Scale -> ScaleDown10Percent" wywoływana jest metoda ScaleDown10Percent(). Powinna ona
    ///     skutkować zmianą skali obrazka do 10%.
    /// </summary>
    [TestMethod]
    public void ShowScaledDown10PercentTest()
    {
        Assert.AreEqual(385, model.ImageWidth);
        Assert.AreEqual(500, model.ImageHeight);
        model.ShowScaledDown10Percent();
        Assert.AreEqual(38.5, model.ImageWidth);
        Assert.AreEqual(50, model.ImageHeight);
    }

    /// <summary>
    ///     Po kliknięciu w menu opcji rozciągania liniowego, otwierane są dwa okna - okno z obrazkiem oraz okno z opcjami w
    ///     postaci suwaków. W trakcie przesuwania suwaków powinny być wyświetlane zmieniane obrazki.
    /// </summary>
    [TestMethod]
    public void LinearStretchingTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, stretchingOptionsWindowService.IsLinearStretchingWindowShown);
        model.OpenLinearStretchingWindow();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, stretchingOptionsWindowService.IsLinearStretchingWindowShown);
    }

    [TestMethod]
    public void GammaStretchingTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, stretchingOptionsWindowService.IsGammaStretchingWindowShown);
        model.OpenGammaStretchingWindow();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, stretchingOptionsWindowService.IsGammaStretchingWindowShown);
    }

    [TestMethod]
    public void EqualizeHistogramTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        model.OpenEqualizeHistogramWindow();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
    }

    [TestMethod]
    public void NegateImageTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, processService.IsNegated);
        model.NegateImage();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, processService.IsNegated);
    }

    [TestMethod]
    public void BinaryThresholdTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, processService.IsBinaryThresholdWindowOpen);
        model.BinaryThreshold();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, processService.IsBinaryThresholdWindowOpen);
    }

    [TestMethod]
    public void GreyscaleThresholdTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, processService.IsGreyscaleThresholdWindowOpen);
        model.GreyscaleThresholdOneSlider();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, processService.IsGreyscaleThresholdWindowOpen);
    }

    [TestMethod]
    public void GreyscaleThresholdTwoSlidersTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, processService.IsGreyscaleThresholdTwoSlidersWindowOpen);
        model.GreyscaleThresholdTwoSliders();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, processService.IsGreyscaleThresholdTwoSlidersWindowOpen);
    }

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