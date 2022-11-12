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
}