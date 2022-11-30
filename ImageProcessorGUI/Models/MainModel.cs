﻿using System;
using System.Threading.Tasks;
using ImageProcessorGUI.ViewModels;
using ImageProcessorLibrary.DataStructures;
using ImageProcessorLibrary.Services;
using IServiceProvider = ImageProcessorLibrary.ServiceProviders.IServiceProvider;

namespace ImageProcessorGUI.Models;

public class MainModel
{
    private readonly IServiceProvider _serviceProvider;

    public MainModel(ImageData imageData, IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        ImageData = imageData;
    }

    public ImageData ImageData { get; set; }
    public double ImageWidth => (double)((decimal)ImageData.Width * Scale);
    public double ImageHeight => (double)((decimal)ImageData.Height * Scale);

    public decimal Scale { get; set; } = 1;

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
        _serviceProvider.OpenImageService.OpenImage();
    }

    public async Task SaveImage()
    {
        await _serviceProvider.SaveImageService.SaveImageAsync(ImageData);
    }

    public void DuplicateImage()
    {
        _serviceProvider.DuplicateImageService.DuplicateImage(ImageData);
    }

    public void ShowValueHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetValueHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRgbHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetRgbHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowRHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetRedHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowGHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetGreenHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
    }

    public void ShowBHistogram()
    {
        var valueHistogram = _serviceProvider.HistogramService.GetBlueHistogram(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(valueHistogram);
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

        _serviceProvider.WindowService.ShowOptionsWindowTwoValues(viewModel);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
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

        _serviceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     wyrównywanie przez eqalizację histogramu.
    /// </summary>
    public void OpenEqualizeHistogramWindow()
    {
        var imageData = new ImageData(ImageData);
        imageData = new StretchingService().EqualizeStretching(imageData);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     Operacja punktowa jednoargumentowe: negacja.
    /// </summary>
    public void NegateImage()
    {
        var imageData = _serviceProvider.ProcessService.NegateImage(ImageData);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
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

        _serviceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
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

        _serviceProvider.WindowService.ShowOptionsWindowOneValue(viewModel);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
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

        _serviceProvider.WindowService.ShowOptionsWindowTwoValues(viewModel);
        _serviceProvider.WindowService.ShowImageWindow(imageData);
    }

    /// <summary>
    ///     operacje punktowe wieloargumentowe:
    ///     dodawania obrazów z wysyceniem
    /// </summary>
    public void AddImage()
    {
    }

    /// <summary>
    ///     operacje punktowe wieloargumentowe:
    ///     dodawania obrazów bez wysycenia.
    /// </summary>
    public void AddImageWithoutSaturate()
    {
    }

    /// <summary>
    ///     liczenia różnicy bezwzględnej obrazów.
    /// </summary>
    public void ImagesDifference()
    {
    }

    /// <summary>
    ///     dodawanie, dzielenie i mnożenie obrazów przez liczbę całkowitą z wysyceniem i bez wysycenia
    /// </summary>
    public void AddNumberToImage()
    {
    }

    /// <summary>
    ///     dodawanie, dzielenie i mnożenie obrazów przez liczbę całkowitą z wysyceniem i bez wysycenia
    /// </summary>
    public void SubtractNumberFromImage()
    {
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
    public void BinaryAnd()
    {
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
    public void BinaryOr()
    {
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
    public void BinaryXor()
    {
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
    public void BinaryNot()
    {
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
    public void GetBinaryMask()
    {
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
    public void Get8BitMask()
    {
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
    public void EdgeSobelEast()
    {
    }

    public void EdgeSobelNorthEast()
    {
    }

    public void EdgeSobelNorth()
    {
    }

    public void EdgeSobelNorthWest()
    {
    }

    public void EdgeSobelWest()
    {
    }

    public void EdgeSobelSouthWest()
    {
    }

    public void EdgeSobelSouth()
    {
    }

    public void EdgeSobelSouthEast()
    {
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
    public void FillBorderConstant()
    {
    }

    public void FillResultBorderConstant()
    {
    }

    public void FillBorderReflect()
    {
    }

    public void FillBorderWrap()
    {
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
    public void CalculateMedian3x3()
    {
    }

    public void CalculateMedian5x5()
    {
    }

    public void CalculateMedian7x7()
    {
    }

    public void CalculateMedian9x9()
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

    public void MedianBlur()
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
    
}