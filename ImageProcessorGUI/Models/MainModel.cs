using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using ImageProcessorGUI.Services;
using ImageProcessorGUI.ViewModels;
using ImageProcessorGUI.Views;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.ServiceProviders;
using ImageProcessorLibrary.Services;

namespace ImageProcessorGUI.Models;

public class MainModel
{
    private readonly IImageServiceProvider _imageServiceProvider;

    private readonly FilterService _filterService = new();

    public MainModel(IImageData imageData, IImageServiceProvider imageServiceProvider)
    {
        _imageServiceProvider = imageServiceProvider;
        ImageData = imageData;
    }

    public IImageData ImageData { get; set; }
    public double ImageWidth => (double)(ImageData.Width * Scale);
    public double ImageHeight => (double)(ImageData.Height * Scale);

    public decimal Scale { get; set; } = 1;
    public IImage AvaloniaImage => GetImage();

    private IImage GetImage()
    {
        return new Bitmap(new MemoryStream(ImageData.Filebytes));
    }

    public event EventHandler<EventArgs> ImageChanged
    {
        add => ImageData.ImageChanged += value;
        remove => ImageData.ImageChanged -= value;
    }

    public void ShowScaledUp200Percent()
    {
        Scale *= 2;
    }

    public void ShowScaledUp150Percent()
    {
        Scale *= 1.5m;
    }

    public void ShowScaledDown50Percent()
    {
        Scale *= 0.5m;
    }

    public void ShowScaledDown25Percent()
    {
        Scale *= 0.25m;
    }

    public void ShowScaledDown20Percent()
    {
        Scale *= 0.2m;
    }

    public void ShowScaledDown10Percent()
    {
        Scale *= 0.1m;
    }

    public void OpenImage()
    {
        _imageServiceProvider.OpenImageService.OpenImage();
    }

    public async Task SaveImage()
    {
        await _imageServiceProvider.SaveImageService.SaveImageAsync(ImageData);
    }

    public void DuplicateImage()
    {
        _imageServiceProvider.DuplicateImageService.DuplicateImage(ImageData);
    }

    public void CreateGrayscale()
    {
        var imageData = new ProcessService().ToGrayscale(ImageData);
        ImageData.Update(imageData);
    }

    public void SwapHorizontal()
    {
        var imageData = new ProcessService().SwapHorizontal(ImageData);
        ImageData.Update(imageData);
    }

    public void ShowValueHistogram()
    {
        var valueHistogram = _imageServiceProvider.HistogramService.GetValueHistogram(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRgbHistogram()
    {
        var valueHistogram = _imageServiceProvider.HistogramService.GetRgbHistogram(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRHistogram()
    {
        var valueHistogram = _imageServiceProvider.HistogramService.GetRedHistogram(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowGHistogram()
    {
        var valueHistogram = _imageServiceProvider.HistogramService.GetGreenHistogram(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowBHistogram()
    {
        var valueHistogram = _imageServiceProvider.HistogramService.GetBlueHistogram(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    /// <summary>
    ///     Algorytm rozciągania liniowego.
    ///     Rozciąganie histogramu: w zakresach `[min; max]` i `[a; b]` zarówno gdy a i b są większe lub mniejsze od zakresu
    ///     poziomów jasności występujących w obrazie w wersji liniowej i nielinowej.
    /// </summary>
    public void OpenLinearStretchingWindow()
    {
        var imageData = new ImageData(ImageData);

        var viewModel = new OptionsTwoValuesViewModel<int, int>(imageData, (data, LMin, LMax) =>
        {
            var stretchingService = new StretchingService();
            return stretchingService.LinearStretching(data, LMin, LMax);
        })
        {
            Header = "Linear stretching",
            Value1Label = "LMin",
            Value2Label = "LMax",
            Value1Min = 0,
            Value2Min = 0,
            Value1Max = 500,
            Value2Max = 500
        };

        _imageServiceProvider.WindowService.ShowOptionsWindowTwoValues(viewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Rozciąganie nieliniowe zaimplementować przez funkcję korekcji gamma ze współczynnikiem wyznaczanym przez
    ///     użytkownika.
    /// </summary>
    public void OpenGammaStretchingWindow()
    {
        var imageData = new ImageData(ImageData);
        var viewModel = new OptionsOneValueViewModel<double>(imageData, (data, gamma) =>
        {
            var stretchingService = new StretchingService();
            return stretchingService.GammaStretching(data, gamma);
        })
        {
            Header = "Gamma stretching",
            Value1Label = "Gamma",
            Value1Min = 0.001,
            Value1Max = 10
        };

        _imageServiceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     wyrównywanie przez eqalizację histogramu.
    /// </summary>
    public void OpenEqualizeHistogramWindow()
    {
        var imageData = new ImageData(ImageData);
        imageData = new StretchingService().EqualizeStretching(imageData);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Operacja punktowa jednoargumentowe: negacja.
    /// </summary>
    public void NegateImage()
    {
        var imageData = _imageServiceProvider.ProcessService.NegateImage(ImageData);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Operacja punktowa jednoargumentowe: progowanie binarne z zamianą liczby poziomów szarości z jednym lub dwoma
    ///     progami wskazywanymi suwakiem i wpisanymi jako parametr,.
    /// </summary>
    public void BinaryThreshold()
    {
        var imageData = new ImageData(ImageData);

        var viewModel = new OptionsOneValueViewModel<int>(imageData, (data, threshold) =>
        {
            var thresholdService = new ThresholdService();
            return thresholdService.BinaryThreshold(data, threshold);
        })
        {
            Header = "Binary threshold",
            Value1Label = "Threshold",
            Value1Min = 0,
            Value1Max = 255
        };

        _imageServiceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Operacja punktowa jednoargumentowe: progowanie binarne z zamianą liczby poziomów szarości z jednym lub dwoma
    ///     progami wskazywanymi suwakiem i wpisanymi jako parametr,.
    /// </summary>
    public void GreyscaleThresholdOneSlider()
    {
        var imageData = new ImageData(ImageData);

        var viewModel = new OptionsOneValueViewModel<int>(imageData, (data, threshold) =>
        {
            var thresholdService = new ThresholdService();
            return thresholdService.GreyscaleThresholdOneSlider(data, threshold);
        })
        {
            Header = "Greyscale threshold one slider",
            Value1Label = "Threshold",
            Value1Min = 0,
            Value1Max = 255
        };

        _imageServiceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Operacja punktowa jednoargumentowe: progowanie binarne z zamianą liczby poziomów szarości z jednym lub dwoma
    ///     progami wskazywanymi suwakiem i wpisanymi jako parametr,.
    /// </summary>
    public void GreyscaleThresholdTwoSliders()
    {
        var imageData = new ImageData(ImageData);

        var viewModel = new OptionsTwoValuesViewModel<int, int>(imageData, (data, threshold1, threshold2) =>
        {
            var thresholdService = new ThresholdService();
            return thresholdService.GreyscaleThresholdTwoSliders(data, threshold1, threshold2);
        })
        {
            Header = "Greyscale threshold two sliders",
            Value1Label = "Threshold1",
            Value2Label = "Threshold2",
            Value1Min = 0,
            Value2Min = 0,
            Value1Max = 255,
            Value2Max = 255
        };

        _imageServiceProvider.WindowService.ShowOptionsWindowTwoValues(viewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     operacje punktowe wieloargumentowe:
    ///     dodawania obrazów z wysyceniem, bez wysycenia.
    /// </summary>
    public void AddImages()
    {
        var imageData = new ImageData(ImageData);
        var addImagesViewModel = new AddImagesViewModel(
            _imageServiceProvider,
            imageData,
            id => _imageServiceProvider.WindowService.ShowImageWindow(new ImageData(id))
        );
        _imageServiceProvider.WindowService.ShowAddImagesViewModel(addImagesViewModel);
    }


    /// <summary>
    ///     dodawanie, dzielenie i mnożenie obrazów przez liczbę całkowitą z wysyceniem i bez wysycenia
    /// </summary>
    public void MathOperation()
    {
        var imageData = new ImageData(ImageData);
        var mathOperationViewModel = new MathOperationViewModel(
            imageData
        );

        _imageServiceProvider.WindowService.ShowMathOperationViewModel(mathOperationViewModel);
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje logiczne na obrazach monochromatycznych
    ///     i binarnych:
    ///     - [ ] not,
    ///     - [ ] and,
    ///     - [ ] or,
    ///     - [ ] xor,
    ///     - [ ] zamiana obrazów z maski binarnej na maskę zapisaną na 8 bitach i na odwrót
    ///     Należy pamiętać o sprawdzeniu zgodności typów i rozmiarów obrazów stanowiących operandy.
    ///     Proszę pamiętać: w operacjach jednopunktowych dwuargumentowych logicznych na obrazach działania prowadzone są na
    ///     odpowiednich pikselach obrazów stanowiących argumenty danej operacji. W szczególności działania prowadzone są na
    ///     bitach o tej samej wadze.
    /// </summary>
    public void BinaryOperation()
    {
        var imageData = new ImageData(ImageData);
        var binaryOperationViewModel = new BinaryOperationViewModel(
            _imageServiceProvider,
            imageData
        );

        _imageServiceProvider.WindowService.ShowBinaryOperationViewModel(binaryOperationViewModel);
    }

    public void MedianBlurWithoutWeights()
    {
        var kernel = new double[,]
        {
            { 1, 1, 1 },
            { 1, 1, 1 },
            { 1, 1, 1 }
        };

        Filter(kernel);
    }

    private void Filter(double[,] kernel, string title = "Filtr")
    {
        var imageData = new ImageData(ImageData);
        var viewModel = new FilterBorderViewModel(imageData, _imageServiceProvider.WindowService, kernel, title);

        var window = new FilterBorderWindow
        {
            DataContext = viewModel
        };

        window.Show();
    }

    public void MedianBlurWithWeights()
    {
        var kernel = new double[,]
        {
            { 1, 1, 1 },
            { 1, 8, 1 },
            { 1, 1, 1 }
        };

        Filter(kernel);
    }

    /// <summary>
    ///     Proszę dołączyć bibliotekę OpenCV i korzystać z niej przygotowując poszczególne funkcjonalności.
    ///     - [ ] Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje:
    ///     - [ ] wygładzania liniowego oparte na typowych maskach wygładzania (uśrednienie, uśrednienie z wagami, filtr
    ///     gaussowski – przedstawione na wykładzie) przestawionych użytkownikowi jako maski do wyboru,
    ///     - [ ] wyostrzania liniowego oparte na 3 maskach laplasjanowych (podanych w wykładzie) przestawionych użytkownikowi
    ///     maski do wyboru,
    ///     - [ ] kierunkowej detekcji krawędzi w oparciu o maski 8 kierunkowych masek Sobela (podstawowe 8 kierunków)
    ///     przestawionych użytkownikowi do wyboru,
    ///     - [ ] Zaimplementować wybór sposobu uzupełnienie marginesów/brzegów w operacjach sąsiedztwa według zasady wybranej
    ///     spośród następujących zasad:
    ///     - [ ] wypełnienie ramki wybraną wartością stałą `n` narzuconą przez użytkownika: `BORDER_CONSTANT`
    ///     - [ ] wypełnienie wyniku wybraną wartością stałą `n` narzuconą przez użytkownika
    ///     - [ ] wyliczenie ramki według `BORDER_REFLECT`
    ///     - [ ] wyliczenie ramki według `BORDER_WRAP`
    ///     - [ ] Opracowanie algorytmu i uruchomienie aplikacji realizującej uniwersalną operację medianową opartą na:
    ///     - [ ] otoczeniu 3x3,
    ///     - [ ] 5x5,
    ///     - [ ] 7x7,
    ///     - [ ] 9x9
    ///     zadawanym w sposób interaktywny: wybór z list, przesuwanie baru lub wpisanie w przygotowane pole).
    ///     Zastosować powyższych metod uzupełniania brzegowych pikselach obrazu, dając użytkownikowi możliwość wyboru, jak w
    ///     zadaniu 1.
    /// </summary>
    public void GaussianBlur()
    {
        var kernel = new double[,]
        {
            { 1, 2, 1 },
            { 2, 4, 2 },
            { 1, 2, 1 }
        };

        Filter(kernel);
    }

    public void SharpeningMask1()
    {
        var kernel = new double[,]
        {
            { 0, -1, 0 },
            { -1, 4, -1 },
            { 0, -1, 0 }
        };

        kernel = _filterService.Normalize(kernel);

        Filter(kernel);
    }

    public void SharpeningMask2()
    {
        var kernel = new double[,]
        {
            { -1, -1, -1 },
            { -1, 8, -1 },
            { -1, -1, -1 }
        };

        Filter(kernel);
    }

    public void SharpeningMask3()
    {
        var kernel = new double[,]
        {
            { 1, -2, 1 },
            { -2, 4, -2 },
            { 1, -2, 1 }
        };

        Filter(kernel);
    }

    public void EdgeSobelEast()
    {
        var kernel = new double[,]
        {
            { -1, 0, 1 },
            { -2, 0, 2 },
            { -1, 0, 1 }
        };

        Filter(kernel);
    }

    public void EdgeSobelNorthEast()
    {
        var kernel = new double[,]
        {
            { 0, 1, 2 },
            { -1, 0, 1 },
            { -2, -1, 0 }
        };

        Filter(kernel);
    }

    public void EdgeSobelNorth()
    {
        var kernel = new double[,]
        {
            { 1, 2, 1 },
            { 0, 0, 0 },
            { -1, -2, -1 }
        };

        Filter(kernel);
    }

    public void EdgeSobelNorthWest()
    {
        var kernel = new double[,]
        {
            { 2, 1, 0 },
            { 1, 0, -1 },
            { 0, -1, -2 }
        };

        Filter(kernel);
    }

    public void EdgeSobelWest()
    {
        var kernel = new double[,]
        {
            { 1, 0, -1 },
            { 2, 0, -2 },
            { 1, 0, -1 }
        };

        Filter(kernel);
    }

    public void EdgeSobelSouthWest()
    {
        var kernel = new double[,]
        {
            { 0, -2, -1 },
            { 1, 0, -1 },
            { 1, 2, 0 }
        };

        Filter(kernel);
    }

    public void EdgeSobelSouth()
    {
        var kernel = new double[,]
        {
            { -1, -2, -1 },
            { 0, 0, 0 },
            { 1, 2, 1 }
        };

        Filter(kernel);
    }

    public void EdgeSobelSouthEast()
    {
        var kernel = new double[,]
        {
            { -2, -1, 0 },
            { -1, 0, 1 },
            { 0, 1, 2 }
        };

        Filter(kernel);
    }

    /// <summary>
    ///     Proszę dołączyć bibliotekę OpenCV i korzystać z niej przygotowując poszczególne funkcjonalności.
    ///     - [ ] Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje:
    ///     - [ ] wygładzania liniowego oparte na typowych maskach wygładzania (uśrednienie, uśrednienie z wagami, filtr
    ///     gaussowski – przedstawione na wykładzie) przestawionych użytkownikowi jako maski do wyboru,
    ///     - [ ] wyostrzania liniowego oparte na 3 maskach laplasjanowych (podanych w wykładzie) przestawionych użytkownikowi
    ///     maski do wyboru,
    ///     - [ ] kierunkowej detekcji krawędzi w oparciu o maski 8 kierunkowych masek Sobela (podstawowe 8 kierunków)
    ///     przestawionych użytkownikowi do wyboru,
    ///     - [ ] Zaimplementować wybór sposobu uzupełnienie marginesów/brzegów w operacjach sąsiedztwa według zasady wybranej
    ///     spośród następujących zasad:
    ///     - [ ] wypełnienie ramki wybraną wartością stałą `n` narzuconą przez użytkownika: `BORDER_CONSTANT`
    ///     - [ ] wypełnienie wyniku wybraną wartością stałą `n` narzuconą przez użytkownika
    ///     - [ ] wyliczenie ramki według `BORDER_REFLECT`
    ///     - [ ] wyliczenie ramki według `BORDER_WRAP`
    ///     - [ ] Opracowanie algorytmu i uruchomienie aplikacji realizującej uniwersalną operację medianową opartą na:
    ///     - [ ] otoczeniu 3x3,
    ///     - [ ] 5x5,
    ///     - [ ] 7x7,
    ///     - [ ] 9x9
    ///     zadawanym w sposób interaktywny: wybór z list, przesuwanie baru lub wpisanie w przygotowane pole).
    ///     Zastosować powyższych metod uzupełniania brzegowych pikselach obrazu, dając użytkownikowi możliwość wyboru, jak w
    ///     zadaniu 1.
    /// </summary>
    public void CalculateMedian()
    {
        var imageData = new ImageData(ImageData);
        var viewModel = new UniversalMedianOperationViewModel(imageData);
        var window = new UniversalMedianOperationWindow { DataContext = viewModel };
        _imageServiceProvider.WindowService.ShowImageWindow(imageData);
        window.Show();
    }


    /// <summary>
    ///     - [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    ///     - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2)
    ///     tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.
    ///     - [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji
    ///     progowania:
    ///     - [ ] Progowanie metodą Otsu,
    ///     - [ ] Progowanie adaptacyjne (adaptive threshold).
    /// </summary>
    public void SobelEdgeDetection()
    {
    }


    /// <summary>
    ///     - [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    ///     - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2)
    ///     tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.
    ///     - [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji
    ///     progowania:
    ///     - [ ] Progowanie metodą Otsu,
    ///     - [ ] Progowanie adaptacyjne (adaptive threshold).
    /// </summary>
    public void PrewittEdgeDetection()
    {
    }


    /// <summary>
    ///     - [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    ///     - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2)
    ///     tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.
    ///     - [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji
    ///     progowania:
    ///     - [ ] Progowanie metodą Otsu,
    ///     - [ ] Progowanie adaptacyjne (adaptive threshold).
    /// </summary>
    public void CannyOperatorEdgeDetection()
    {
    }


    /// <summary>
    ///     - [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    ///     - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2)
    ///     tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.
    ///     - [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji
    ///     progowania:
    ///     - [ ] Progowanie metodą Otsu,
    ///     - [ ] Progowanie adaptacyjne (adaptive threshold).
    /// </summary>
    public void OtsuSegmentation()
    {
    }

    /// <summary>
    ///     - [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    ///     - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2)
    ///     tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.
    ///     - [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji
    ///     progowania:
    ///     - [ ] Progowanie metodą Otsu,
    ///     - [ ] Progowanie adaptacyjne (adaptive threshold).
    /// </summary>
    public void AdaptativeThresholdSegmentation()
    {
    }

    /// <summary>
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność wykonywania podstawowych operacji morfologii matematycznej:
    ///     - [ ] erozji,
    ///     - [ ] dylatacji,
    ///     - [ ] otwarcia,
    ///     - [ ] zamknięcia
    ///     wykorzystując podstawowy element strukturalny dysk 3x3.
    /// </summary>
    public void MorphologyErosion()
    {
    }

    public void MorphologyDilation()
    {
    }

    public void MorphologyOpening()
    {
    }

    public void MorphologyClosing()
    {
    }

    /// <summary>
    ///     - [ ] Opracować algorytm i uruchomić funkcjonalność realizującą wyznaczanie następujących składowych wektora cech
    ///     obiektu binarnego:
    ///     - [ ] Momenty
    ///     - [ ] Pole powierzchni i obwód
    ///     - [ ] Współczynniki kształtu: aspectRatio, extent, solidity, equivalentDiameter
    ///     - [ ] Przygotować zapis wyników w postaci pliku tekstowego do wczytania do oprogramowania Excel.
    ///     Program przetestować na podstawowych figurach znakach graficznych (gwiazdka, wykrzyknik, dwukropek, przecinek,
    ///     średnik, itp.).
    /// </summary>
    public void CalculateFeatureVector()
    {
    }

    /// <summary>
    ///     wyostrzania liniowego oparte na 3 maskach laplasjanowych (podanych w wykładzie) przestawionych użytkownikowi maski
    ///     do wyboru,
    /// </summary>
    public void Sharpening()
    {
    }

    /// <summary>
    ///     Implementacja narzędzia do manipulacji widmem amplitudowym obrazu w celu likwidacji zakłóceń periodycznych
    /// </summary>
    public void RemovePeriodicNoice()
    {
    }

    public void ToBinaryImage()
    {
        BinaryThreshold();
    }
}