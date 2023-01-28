using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services.OpenCvServices;

namespace ImageProcessorTests;

[TestClass]
public class FeatureVectorServiceTests
{
    private FeatureVectorService service;

    [TestInitialize]
    public void TestInitialize()
    {
        service = new FeatureVectorService();
    }

    [TestMethod]
    public void Test()
    {
        var imageData = new ImageData(3, 5);
        var result = service.GetFeatureVectors(imageData);
        Assert.IsNotNull(result);
    }
}