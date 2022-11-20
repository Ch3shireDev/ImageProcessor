using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ImageProcessorGUI.Models;
using ImageProcessorGUI.Services;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop!.Startup += (sender, args) =>
            {
                var path = args.Args[0];

                var dialogService = new SelectImagesDialogService();
                var windowService = new WindowService();

                var fileService = new OpenImageService(dialogService, windowService);

                var saveImageDialogService = new SaveImageDialogService();
                var fileSystemService = new FileSystemService();
                var saveImageService = new SaveImageService(saveImageDialogService, fileSystemService);
                var duplicateImageService = new DuplicateImageService(windowService);
                
                var serviceProvider = new ServiceProvider
                {
                    OpenImageService = fileService,
                    SaveImageService = saveImageService,
                    DuplicateImageService = duplicateImageService,
                    SelectImagesDialogService = dialogService,
                    WindowService = windowService,
                };

                windowService.ServiceProvider = serviceProvider;

                var imageData = new ImageData(path, File.ReadAllBytes(path));
                var mainModel = new ImageModel(imageData, serviceProvider);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(mainModel)
                };
            };

        base.OnFrameworkInitializationCompleted();
    }
}