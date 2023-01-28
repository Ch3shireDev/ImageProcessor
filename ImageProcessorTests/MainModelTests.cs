using System.Drawing;
using ImageProcessorGUI.Models;
using ImageProcessorGUI.ViewModels;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services.Enums;
using ImageProcessorTests.Mockups;

namespace ImageProcessorTests;

[TestClass]
public class MainModelTests
{
    private MockDuplicateImageService _duplicateImageService;
    private MockHistogramService _histogramService;
    private MockNegateImageService _negateImageService;
    private MockOpenImageService _openImageService;
    private MockSaveImageService _saveImageService;
    private MockWindowService _windowService;
    private MainModel model;

    private ImageServiceProvider serviceProvider;
    private MockStretchingOptionsWindowService stretchingOptionsWindowService;

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
        _negateImageService = new MockNegateImageService();

        serviceProvider = new ImageServiceProvider
        {
            OpenImageService = _openImageService,
            SaveImageService = _saveImageService,
            DuplicateImageService = _duplicateImageService,
            WindowService = _windowService,
            HistogramService = _histogramService,
            StretchingOptionsService = stretchingOptionsWindowService,
            NegateImageService = _negateImageService
        };

        model = new MainModel(imageData, serviceProvider);
    }

    [TestMethod]
    public void BinaryOperationsViewModelTest()
    {
        var imageData = new ImageData(new[,]
        {
            { Color.DarkRed, Color.White, Color.Red },
            { Color.Blue, Color.White, Color.Black },
            { Color.Black, Color.Purple, Color.Green }
        });

        Assert.AreEqual(Color.DarkRed.ToArgb(), imageData.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.White.ToArgb(), imageData.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.Red.ToArgb(), imageData.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Blue.ToArgb(), imageData.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.White.ToArgb(), imageData.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), imageData.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), imageData.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Purple.ToArgb(), imageData.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), imageData.GetPixelRgb(2, 2).ToArgb());

        var mainModel = new MainModel(imageData, serviceProvider);

        mainModel.BinaryOperation();
        var viewModel = _windowService.BinaryOperationViewModel as BinaryOperationViewModel;
        Assert.IsNotNull(viewModel);

        viewModel.SelectedOperation = BinaryOperationType.BINARY_AND;
        viewModel.SelectedImage = new ImageData(new[,]
        {
            { true, true, true },
            { false, false, false },
            { false, true, true }
        });

        viewModel.ShowCommand.Execute(null);

        var image = _windowService.ImageData;

        Assert.IsNotNull(image);

        Assert.AreEqual(Color.DarkRed.ToArgb(), image.GetPixelRgb(0, 0).ToArgb());
        Assert.AreEqual(Color.White.ToArgb(), image.GetPixelRgb(1, 0).ToArgb());
        Assert.AreEqual(Color.Red.ToArgb(), image.GetPixelRgb(2, 0).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(0, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(1, 1).ToArgb());
        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(2, 1).ToArgb());

        Assert.AreEqual(Color.Black.ToArgb(), image.GetPixelRgb(0, 2).ToArgb());
        Assert.AreEqual(Color.Purple.ToArgb(), image.GetPixelRgb(1, 2).ToArgb());
        Assert.AreEqual(Color.Green.ToArgb(), image.GetPixelRgb(2, 2).ToArgb());
    }

    /// <summary>
    ///     Po kliknięciu w menu "File" -> "Open" wywoływana jest metoda OpenImage(). To powinno poskutkować otwarciem okna
    ///     wyboru pliku, po czym powinno pojawić się nowe okno z wybranym obrazem.
    /// </summary>
    [TestMethod]
    public async Task OpenImageTest()
    {
        Assert.IsFalse(_openImageService.IsOpen);
        await model.OpenImage();
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
        Assert.AreEqual(false, _windowService.IsOptionWindowCalled);
        model.OpenLinearStretchingWindow();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsOptionWindowCalled);
    }

    [TestMethod]
    public void GammaStretchingTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsOptionWindowCalled);
        model.OpenGammaStretchingWindow();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsOptionWindowCalled);
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
        Assert.AreEqual(false, _negateImageService.IsNegated);
        model.NegateImage();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _negateImageService.IsNegated);
    }

    [TestMethod]
    public void BinaryThresholdTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsOptionWindowCalled);
        model.BinaryThreshold();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsOptionWindowCalled);
    }

    [TestMethod]
    public void GreyscaleThresholdTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsOptionWindowCalled);
        model.GreyscaleThresholdOneSlider();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsOptionWindowCalled);
    }

    [TestMethod]
    public void GreyscaleThresholdTwoSlidersTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsOptionWindowCalled);
        model.GreyscaleThresholdTwoSliders();
        Assert.AreEqual(true, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsOptionWindowCalled);
    }

    [TestMethod]
    public void AddImageTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsAddImagesWindowCalled);
        model.AddImages();
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsAddImagesWindowCalled);
    }

    //[OtsuSegmentationBasicTest]
    //public void ImagesDifferenceTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void AddNumberToImageTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void SubtractNumberFromImageTest()
    //{
    //    Assert.Fail();
    //}

    [TestMethod]
    public void BinaryOperationsTest()
    {
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(false, _windowService.IsBinaryOperationsWindowCalled);
        model.BinaryOperation();
        Assert.AreEqual(false, _windowService.IsShowImageWindowCalled);
        Assert.AreEqual(true, _windowService.IsBinaryOperationsWindowCalled);
    }

    //[OtsuSegmentationBasicTest]
    //public void GetBinaryMaskTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void Get8BitMaskTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void GaussianBlurTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelEastTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelNorthEastTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelNorthTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelNorthWestTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelWestTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelSouthWestTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelSouthTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void EdgeSobelSouthEastTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void FillBorderConstantTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void FillResultBorderConstantTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void FillBorderReflectTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void FillBorderWrapTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CalculateMedian3x3Test()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CalculateMedian5x5Test()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CalculateMedian7x7Test()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CalculateMedian9x9Test()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void SobelEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void PrewittEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CannyOperatorEdgeDetectionTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void OtsuSegmentationTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void AdaptativeThresholdSegmentationTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void MorphologyErosionTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void MorphologyDilationTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void MorphologyOpeningTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void MorphologyClosingTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void CalculateFeatureVectorTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void ModifyAmplitudeSpectrumTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void MedianBlurTest()
    //{
    //    Assert.Fail();
    //}

    //[OtsuSegmentationBasicTest]
    //public void SharpeningTest()
    //{
    //    Assert.Fail();
    //}
}