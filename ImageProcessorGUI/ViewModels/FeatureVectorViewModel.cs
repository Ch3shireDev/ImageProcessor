using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using ImageProcessorLibrary.Services;
using ReactiveUI;

namespace ImageProcessorGUI.ViewModels;

public class FeatureVectorViewModel : ReactiveObject
{
    private string? errorMessage;

    public IImageData ImageData;

    private string result;

    public FeatureVectorViewModel(IImageData imageData)
    {
        FeatureVectorService = new FeatureVectorService();
        CsvService = new CsvService();
        ImageData = imageData;
    }

    public string Result
    {
        get => result;
        set
        {
            result = value;
            this.RaisePropertyChanged();
        }
    }


    public string? ErrorMessage
    {
        get => errorMessage;
        set
        {
            errorMessage = value;
            this.RaisePropertyChanged();
        }
    }

    public FeatureVectorService FeatureVectorService { get; set; }
    public CsvService CsvService { get; set; }

    public string FeatureVectorText { get; set; }

    public ICommand SaveCommand => ReactiveCommand.CreateFromTask(Save);
    
    public void Run()
    {
        Result = GetFeatureVectorCsv();
    }

    private string GetFeatureVectorCsv()
    {
        var featureVectors = FeatureVectorService.GetFeatureVectors(ImageData).ToList();

        return CsvService.ToText(featureVectors);
    }

    private async Task Save()
    {
        var result = GetFeatureVectorCsv();
        var dialog = new SaveFileDialog();
        dialog.Filters.Add(new FileDialogFilter
        {
            Name = "CSV", Extensions = { "csv" },

        });

        dialog.InitialFileName = "result.csv";
        var path = await dialog.ShowAsync(MainWindow);

        await File.WriteAllTextAsync(path, result);
    }
    private Window? MainWindow =>
        Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;
}