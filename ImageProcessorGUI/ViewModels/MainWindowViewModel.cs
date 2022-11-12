namespace ImageProcessorGUI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Title { get; set; } = "Image processor";
    public MenuViewModel MenuViewModel { get; set; } = new MenuViewModel();
}