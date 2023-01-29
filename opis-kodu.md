# Opis kodu

# `ImageProcessorGUI/Views/MainWindow.xaml.cs`

Główne okno programu zawarte jest w klasie MainWindow. Widok jest połączony z modelem widoku, MainWindowViewModel. Wszystkie wywoływane komendy są umieszczone w modelu widoku. Widok komunikuje się z modelem widoku za pomocą wiązania danych, tj. data binding.

# `ImageProcessorGUI/Views/MainWindowViewModel.cs`

Klasa MainWindowViewModel jest odpowiedzialna za przekazywanie żądań do MainModel, w którym przechowywana jest komunikacja z serwisami API. W klasie MainWindowViewModel są głównie wywołania funkcji modelu i ewentualne żądania odświeżenia wartości do których odwołuje się widok. Na przykład, przy operacjach skalowania podnoszona jest flaga aktualizacji parametrów obserwowanej wysokości i szerokości obrazu.

# `ImageProcessorGUI/Models/MainModel.cs`

Klasa MainModel przechowuje kod wywoływania kolejnych komenad API. Wszystkie komendy odwołują się do zewnętrznych serwisów, działając na zasadzie przekazywania odpowiedzialności kolejnym podzespołom. W ten sposób program jest rozszerzalny, a jedna z głównych klas projektu nie jest skomplikowana bardziej niż to konieczne.

Przyjrzymy się teraz działaniu funkcji usuwania szumów periodycznych.

Tworzony jest duplikat obiektu ImageData - obiektu przechowującego dane na temat obrazu. Duplikat będzie finalnym obrazem po zmianach. Zarówno obraz oryginalny jak i duplikat są przekazywane jako argumenty do konstruktora klasy `RemovePeriodicNoiseViewModel`, modelu widoku, w której jest zawarta logika operacji odszumiania obrazu. Model widoku jest osadzany jako `DataContext` dla obiektu klasy `RemovePeriodicNoiseView`, widoku okna opcji. Obok okna opcji wyświetlane jest także okno z finalnym obrazem, na razie niezmienionym, które jest automatycznie aktualizowane przy podnoszeniu zdarzenia aktualizacji obrazu.

# `ImageProcessorGUI/ViewModels/AddPeriodicNoiseViewModel.cs`

Ponieważ logika usuwania szumów periodycznych nie jest skomplikowana, postanowiłem zawrzeć całość w modelu widoku, bez tworzenia osobnego modelu. Metoda `Refresh` odświeża obraz końcowy po przetworzeniu przez algorytm usuwania szumów periodycznych, jak również wyświetla widma fourierowskie przed i po usunięciu cech szumowych.

# `ImageProcessorLibrary/Services/FourierServices/FftService.cs`

Klasa `FftService` zawiera metody pozwalające na uzyskiwanie widm fourierowskich z obrazów i obrazów z widm fourierowskich, jak również obróbkę widm w celu usuwania cech. Dane na temat obrazu w postaci widma są przechowywane w obiektach klasy `ComplexData`, jako że widma zawierają macierze liczb zespolonych dla wszystkich kanałów barw.

Aby być w stanie wyświetlać widma w formie przyjaznej dla użytkownika, obraz musi być przesunięty tak, by wszystkie rogi znalazły się w środku, jak również jego amplituda musi zostać zlogarytmowana i znormalizowana. Dopiero w takiej formie widmo jest przedstawiane użytkownikowi.

# `ImageProcessorLibrary/Services/FourierServices/FourierService.cs`

Klasa `FftService` korzysta z podmienialnego serwisu `FourierService`, który dostarcza jedynie transformacje Fouriera jedno- i dwuwymiarowe w obydwu kierunkach. Ponieważ na etapie pisania była zmieniana biblioteka dostarczająca metody transformacji Fouriera, było to krytycznie ważne, by podmiana nastąpiła bardzo prosto, ze zmianą minimalnej liczby innych fragmentów kodu. Kontrola czy podmiana nie spowodowała zmiany działania programu zachodziła poprzez wywoływanie tetów jednostowych.

# `ImageProcessorTests/FftTests.cs`

W klasie `FftTests` zawarte są testy jednostkowe dla klasy `FftService`. Testy sprawdzają poprawność działania transformacji Fouriera dla obrazów o różnych rozmiarach, a także dla obrazów z szumem gaussowskim. Testy sprawdzają również poprawność działania funkcji usuwającej cechy szumowe z widm.

Testy były pisane od najprostszych - na przykład, czy po transformacji i transformacji odwrotnej obraz jest ten sam - do bardziej skomplikowanych, na przykład sprawdzania, czy dla małego obrazu uzyskiwane są te same wartości natężenia widm, czy nie występują wartości nieskończone w niewłaściwych miejscach, etc. W praktyce zdane testy zapewniają, że dana funkcjonalność działa poprawnie. Jeśli na którymś etapie programu zostanie zmieniony fragment kodu, który powodowałby nieoczekiwane błędy wskutek sprzęgnięć z klasą `FftService`, testy jednostkowe powinny to wykryć.

Wszystkie testy są pisane według kolejności:

1. Przygotowanie danych wejściowych
2. Wywołanie metody testowanej
3. Sprawdzenie poprawności wyników