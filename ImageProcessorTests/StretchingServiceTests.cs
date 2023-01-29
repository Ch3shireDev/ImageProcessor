//using ImageProcessorLibrary.DataStructures;
//using ImageProcessorLibrary.Services.StretchingServices;

//namespace ImageProcessorTests;

//[TestClass]
//public class StretchingServiceTests
//{
//    private StretchingOptionsService _stretchingOptionsService;

//    [TestInitialize]
//    public void TestInitialize()
//    {
//        _stretchingOptionsService = new StretchingOptionsService();
//    }

//    [TestMethod]
//    public void EqualizeHistogramTest()
//    {
//        var filebytes = File.ReadAllBytes("Resources/lion.jpg");
//        var imageData = new ImageData("lion.jpg", filebytes);
//        var result = _stretchingOptionsService.GetEqualizedImage(imageData);
//        Assert.AreEqual(imageData.Width, result.Width);
//        Assert.AreEqual(imageData.Height, result.Height);
//    }
//}