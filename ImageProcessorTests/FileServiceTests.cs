using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;

namespace ImageProcessorTests;

[TestClass]
public class FileServiceTests
{
    private MockDialogService dialogService;
    private FileService fileService;
    private MockWindowService windowService;

    [TestInitialize]
    public void TestInitialize()
    {
        var bytes = File.ReadAllBytes("Resources/lion.jpg");
        var imageData = new ImageData("lion.jpg", bytes);
        dialogService = new MockDialogService(imageData);
        windowService = new MockWindowService();
        fileService = new FileService(dialogService, windowService);
    }

    [TestMethod]
    public async Task OpenImageTest()
    {
        Assert.IsFalse(windowService.IsShowImageWindowCalled);
        await fileService.OpenImage();
        Assert.IsTrue(windowService.IsShowImageWindowCalled);
    }
}