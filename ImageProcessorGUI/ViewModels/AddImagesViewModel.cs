using System.Threading.Tasks;
using System.Windows.Input;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class AddImagesViewModel : ViewModelBase
{
    private readonly IServiceProvider _serviceProvider;

    public AddImagesViewModel(IServiceProvider serviceProvider, ImageData imageData)
    {
        ImageData = imageData;
        _serviceProvider = serviceProvider;
        OriginalImageData = new ImageData(imageData);
    }
    
    public ImageData ImageData { get; set; }
    public ImageData OriginalImageData { get; set; }
    public ImageData AddedImageData { get; set; }
    public string Filepath { get; set; }
    public ICommand SelectImageCommand => ReactiveCommand.CreateFromTask(SelectFile);
    public ICommand ApplyCommand => ReactiveCommand.Create(Apply);

    public async Task SelectFile()
    {
        var result = await _serviceProvider.SelectImagesService.SelectImages();
        if (result.Length == 0) return;
        AddedImageData = result[0];
        Filepath = AddedImageData.Filepath;
        this.RaisePropertyChanged(nameof(Filepath));
    }

    readonly ImageOperationService imageOperationService = new ImageOperationService();

    public void Apply()
    {
        if (AddedImageData == null) return;
        var result = imageOperationService.AddImages(ImageData, AddedImageData);
        ImageData.Update(result);
    }
}