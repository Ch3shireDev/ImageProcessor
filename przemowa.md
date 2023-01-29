# Początek

Dzień dobry, nazywam się Igor Nowicki i na dzisiejszej prezentacji przedstawię projekt Image Processor wraz z funkcjonalnością usuwania szumów periodycznych poprzez manipulację widmem fourierowskim.

## Slajd 1 - Szumy periodyczne

W procesie akwizycji obrazu mogą występować artefakty pochodzące z okresowych zakłóceń. Na przykład nierównomiernie dostarczany prąd może powodować jasne i ciemne prążki na zeskanowanych zdjęciach. Problem szumów periodycznych pojawia się często przy digitalizacji starych zdjęć lub dokumentacji medycznej.

Poniżej przedstawiam przykłady szumów - skan zdjęcia z rezonansu magnetycznego mózgu, skan zdjęcia ulicy z lat 30. XX wieku oraz zdjęcie z satelity pokazujące miasto Pompeje.

## Slajd 2 - Idea

Do rozwiązania problemu usuwania szumów periodycznych wykorzystamy fakt, że dowolnie skomplikowaną funkcję ciągłą okresową możemy rozbić na sumę sinusoid o coraz wyższych częstotliwościach.

## Slajd 3 - Podstawa matematyczna

W przypadku sygnału próbkowanego, jakim jest obraz cyfrowy, możemy przeprowadzić transformację Fouriera poprzez znalezienie kolejnych wartości funkcji $F(u,v)$ odpowiadających amplitudom sinusoid o coraz wyższych częstotliwościach. Przeprowadzenie ponownej transformacji Fouriera na funkcji $F(u,v)$ z przeciwnym znakiem prowadzi do odzyskania pierwotnego sygnału.

## Slajd 4 - Zastosowanie

Mamy zatem sposób, który pozwala nam na rozkład skomplikowanego sygnału na fale proste o różnych częstotliwościach. Możemy więc wykorzystać tę metodę do usuwania szumów periodycznych.

Jak widzimy poniżej, widmo obrazu z nałożonym całościowo szumem periodycznym ma łatwą do uchwycenia cechę odpowiadającą za szum.

## Slajd 5 - Działanie algorytmu

Algorytm usuwania szumów periodycznych:

1. Przekształcić obraz do postaci widma fourierowskiego
2. Znaleźć i usunąć jasne fragmenty widma spoza środka
3. Przekształcić tak zmienione widmo z powrotem do postaci obrazu.

## Slajd 7 - Prezentacja

Zaprezentuję teraz działanie programu. Najpierw wczytam obraz z nałożonymi szumami periodycznymi. Następnie wybiorę z menu opcję "Usuń szumy periodyczne". Za pomocą suwaków określę pozycję i rozmiar prostokątów służących do usuwania szumów. Na koniec wybiorę opcję "Zastosuj" i otrzymam oczyszczony obraz.

## Slajd 8 - Opis środowiska

Projekt został przygotowany w środowisku Visual Studio 2022, z użyciem języka C# i frameworka .NET 6. Biblioteka programistyczna została całkowicie odseparowana od powłoki graficznej, możliwe jest jej ponowne wykorzystanie przy innych projektach, niekoniecznie wykorzystujących ten sam interfejs graficzny.

## Slajd 9 - Powłoka graficzna

Interfejs użytkownika został przygotowany z użyciem frameworka Avalonia, reklamowanego jako całkowicie multiplatformowy.

## Slajd - Architektura MVVM

Architektura interfejsu graficznego została oparta o wzorzec MVVM - model, widok, model widoku.

W widoku przechowywane są wszystkie informacje na temat wyglądu aplikacji, model widoku zajmuje się przechowywaniem kodu interakcji interfejsu graficznego z narzędziami programistycznymi, a model przechowuje informacje na których operuje program i wywołuje narzędzia programistyczne. Aplikacja projektowana w oparciu o wzorzec MVVM ma mniej sprzężone ze sobą komponenty, jest również prostsza w testowaniu.

## Slajd 10 - Biblioteka programistyczna

Biblioteka programistyczna korzysta z pakietu OpenCV do zaawansowanych operacji na obrazach, jak również z modułu FFTW.NET, implementującego algorytm szybkiej transformacji Fouriera.

## Slajd 11 - Moduł testowy

Projekt była przygotowany w większości z wykorzystaniem doktryny programowania sterowanego testami (TDD). Takie podejście pozwala na zwiększenie stabilności projektu, obniża sprzęgnięcia między modułami, a także ułatwia refaktoryzację kodu.

## Slajd 12 - Dokumentacja

Do stworzenia dokumentacji został wykorzystany generator DocFX. Pozwala on na jednoznaczne związanie komentarzy w kodzie z dokumentacją, a także na generowanie dokumentacji w formacie HTML.

## Slajd 13 - System kontroli wersji

Projekt został przygotowany w systemie kontroli wersji Git, z użyciem platformy GitHub. Wszystkie zmiany w kodzie są zapisywane w repozytorium, co pozwala na łatwe zarządznie zmianami w projekcie i szybkie odzyskiwanie poprzednich wersji.

## Slajd 14 - Zakończenie

Dziękuję za uwagę.