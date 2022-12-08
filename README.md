# ImageProcessor

Projekt na zaliczenie zajęć z Algorytmów Przetwarzania Obrazów.

## 1. Odczyt i zapis obrazów

Aplikacja powinna:

- [x] pracować z obrazami zapisanymi w formatach: `.bmp; .tif; .png; .jpg`)
- [x] zapewniać opcje:
  - [x] wczytywania obrazu,
  - [x] zapisywania obrazu,
  - [x] duplikacji obrazu,
  - [x] jednoczesnego wyświetlania wielu obrazów.
- [x] wyświetlać histogram wczytanych obrazów monochromatycznych.
- [x] wyświetlać histogram wczytanych obrazów kolorowych.

## 2. Wyświetlanie obrazów

Aplikacja powinna:

- [x] pokazywać obrazy w powiększeniach:
  - [x] dopasowany do szerokości ekranu,
  - [x] 100% czyli piksel w piksel,
  - [x] pomniejszony (50%, 25%, 20% i 10%) i
  - [x] powiększony (150%, 200%)
- [x] dostarczać prowadnice do przesuwania obrazu w pionie i poziomie.

## 3. Operacje na histogramach

- [x] opracować algorytmy (metodami przedstawionymi na wykładzie): 
  - [x] rozciągania liniowego
  - [x] rozciągania nieliniowego
  - [x] wyrównywania przez eqalizację histogramu.

Rozciąganie histogramu: w zakresach `[min; max]` i `[a; b]` zarówno gdy a i b są większe lub mniejsze od zakresu poziomów jasności występujących w obrazie w wersji liniowej i nielinowej.

Rozciąganie nieliniowe zaimplementować przez funkcję korekcji gamma ze współczynnikiem wyznaczanym przez użytkownika.

## 4. Operacje punktowe jednoargumentowe

- [x] opracować algorytm i uruchomić aplikację realizującą typowe operacje punktowe jednoargumentowe takie jak:
  - [x] negacja,
  - [x] progowanie binarne z zamianą liczby poziomów szarości z jednym lub dwoma progami wskazywanymi suwakiem i wpisanymi jako parametr,
  - [x] progowanie bez zamiany liczby poziomów szarości z dwoma progami wskazanymi przez wskazywanym suwakiem i wpisanym jako parametr.

- [ ] przygotować własne monochromatyczne obrazy testowe.

Powyższe funkcjonalności proszę przetestować na obrazach, których histogramy pokazują rozdzielność zakres poziomów szarości obiektów i tła (obrazy o histogramie dwumodalnym o głębokiej dolince) i obrazach o zawężonym zakresie tonów (brak wypełnienia tonacji skrajnie ciemnych i skrajnie jasnych) oraz o pełnym wykorzystaniu całego zakresu tonów.

## 5. Operacje punktowe wieloargumentowe

Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje punktowe wieloargumentowe:

- [x] dodawania obrazów z wysyceniem
- [x] dodawanie obrazów bez wysycenia
- [x] dodawanie, dzielenie i mnożenie obrazów przez liczbę całkowitą z wysyceniem i bez wysycenia
- [x] liczenia różnicy bezwzględnej obrazów.

## 6. Operacje logiczne

Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje logiczne na obrazach monochromatycznych i binarnych:

- [x] not,
- [x] and,
- [x] or,
- [x] xor,
- [x] zamiana obrazów z maski binarnej na maskę zapisaną na 8 bitach i na odwrót

Należy pamiętać o sprawdzeniu zgodności typów i rozmiarów obrazów stanowiących operandy.

Proszę pamiętać: w operacjach jednopunktowych dwuargumentowych logicznych na obrazach działania prowadzone są na odpowiednich pikselach obrazów stanowiących argumenty danej operacji. W szczególności działania prowadzone są na bitach o tej samej wadze.

Proszę o przygotowanie własnych monochromatycznych i binarnych obrazów testowych.

## 7. OpenCV

Proszę dołączyć bibliotekę OpenCV i korzystać z niej przygotowując poszczególne funkcjonalności.

- [x] Opracowanie algorytmu i uruchomienie funkcjonalności realizującej operacje:
    - [x] wygładzania liniowego oparte na typowych maskach wygładzania:
        - [x] uśrednienie, 
        - [x] uśrednienie z wagami, 
        - [x] filtr gaussowski
        przestawionych użytkownikowi jako maski do wyboru,
    - [x] wyostrzania liniowego oparte na 3 maskach laplasjanowych (podanych w wykładzie) przestawionych użytkownikowi maski do wyboru,
    - [x] kierunkowej detekcji krawędzi w oparciu o maski 8 kierunkowych masek Sobela (podstawowe 8 kierunków) przestawionych użytkownikowi do wyboru,

- [x] Zaimplementować wybór sposobu uzupełnienie marginesów/brzegów w operacjach sąsiedztwa według zasady wybranej spośród następujących zasad:
    - [x] wypełnienie ramki wybraną wartością stałą `n` narzuconą przez użytkownika: `BORDER_CONSTANT`
    - [x] wypełnienie wyniku wybraną wartością stałą `n` narzuconą przez użytkownika
    - [x] wyliczenie ramki według `BORDER_REFLECT`
    - [x] wyliczenie ramki według `BORDER_WRAP`

- [ ] Opracowanie algorytmu i uruchomienie aplikacji realizującej uniwersalną operację medianową opartą na:
    - [ ] otoczeniu 3x3, 
    - [ ] 5x5, 
    - [ ] 7x7, 
    - [ ] 9x9 
 zadawanym w sposób interaktywny: wybór z list, przesuwanie baru lub wpisanie w przygotowane pole). 
 
Zastosować powyższych metod uzupełniania brzegowych pikselach obrazu, dając użytkownikowi możliwość wyboru, jak w zadaniu 1.

## 8. Detekcja krawędzi

- [ ] Implementacja detekcji krawędzi operatorami opartymi na maskach Sobela i Prewitta oraz operatorem Cannyego.
- [ ] Opracować algorytm i uruchomić funkcjonalność realizującą segmentację obrazów następującymi metodami:
    - [ ] Dostosowanie obsługi do wykonywania prostego interaktywnego progowania z jednym i dwoma progami (zad 2 lab 2) tak, aby prezentować wyniki w chwili zmiany progu związanego z przesunięciem wskaźnika lub wpisania nowej wartości.

- [ ] Ponadto jako część "interface" operacji występuje wykonanie pozostałych dwóch poniżej wymienionych wersji progowania:
    - [ ] Progowanie metodą Otsu,
    - [ ] Progowanie adaptacyjne (adaptive threshold).

## 9. Algorytmy morfologii

- [ ] Opracować algorytm i uruchomić funkcjonalność wykonywania podstawowych operacji morfologii matematycznej: 
    - [ ] erozji, 
    - [ ] dylatacji, 
    - [ ] otwarcia,
    - [ ] zamknięcia 
    wykorzystując podstawowy element strukturalny dysk 3x3.
    
## 10. Składowe wektora cech obiektu binarnego

- [ ] Opracować algorytm i uruchomić funkcjonalność realizującą wyznaczanie następujących składowych wektora cech obiektu binarnego:
    - [ ] Momenty
    - [ ] Pole powierzchni i obwód
    - [ ] Współczynniki kształtu: aspectRatio, extent, solidity, equivalentDiameter

- [ ] Przygotować zapis wyników w postaci pliku tekstowego do wczytania do oprogramowania Excel.

Program przetestować na podstawowych figurach znakach graficznych (gwiazdka, wykrzyknik, dwukropek, przecinek, średnik, itp.).

## 11. Implementacja narzędzia do manipulacji widmem amplitudowym obrazu w celu likwidacji zakłóceń periodycznych

- [ ] Zaimplementować wskazany algorytm.