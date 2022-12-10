using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class FeatureVectorServiceTests
{
    [TestInitialize]
    public void TestInitialize()
    {
        service = new FeatureVectorService();
    }

    FeatureVectorService service;

    [TestMethod]
    public void Test()
    {
        var imageData = new ImageData(3, 5);
        var result = service.GetFeatureVectors(imageData);
        Assert.IsNotNull(result);
    }
}