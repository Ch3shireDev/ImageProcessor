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
using ImageProcessorLibrary.Services.DuplicateImageServices;
using ImageProcessorLibrary.Services.FileSystemServices;
using ImageProcessorLibrary.Services.HistogramServices;
using ImageProcessorLibrary.Services.ImageServices;
using ImageProcessorLibrary.Services.OpenImageServices;
using ImageProcessorLibrary.Services.SaveImageServices;

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
        {
            desktop!.Startup += (sender, args) =>
            {
                var dialogService = new SelectImagesDialogService();
                var windowService = new WindowService();

                var fileService = new OpenImageService(dialogService, windowService);

                var saveImageDialogService = new SaveImageDialogService();
                var fileSystemService = new FileSystemService();
                var saveImageService = new SaveImageService(saveImageDialogService, fileSystemService);
                var duplicateImageService = new DuplicateImageService(windowService);
                var histogramService = new HistogramService();
                //var stretchingOptionsService = new StretchingOptionsService();
                var negateImageService = new ImageProcessor();

                var serviceProvider = new ImageServiceProvider
                {
                    OpenImageService = fileService,
                    SaveImageService = saveImageService,
                    DuplicateImageService = duplicateImageService,
                    SelectImagesDialogService = dialogService,
                    WindowService = windowService,
                    HistogramService = histogramService,
                    //StretchingOptionsService = stretchingOptionsService,
                    ImageProcessor = negateImageService,
                    SelectImagesService = dialogService
                };

                windowService.imageServiceProvider = serviceProvider;


                var path = GetPath(args);

                var mainModel = new MainModel(path, serviceProvider);
                var mainViewModel = new MainWindowViewModel(mainModel);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private static ImageData GetPath(ControlledApplicationLifetimeStartupEventArgs args)
    {
        if (args.Args.Length == 0)
        {
            return new ImageData(new byte[,] { { 0 } });
        }

        var path = args.Args[0];
        var imageData = new ImageData(path, File.ReadAllBytes(path));
        return imageData;
    }
}