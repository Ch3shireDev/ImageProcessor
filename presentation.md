---
marp: true
theme: gaia
_class: lead
paginate: true
backgroundColor: #fff
backgroundImage: url('https://marp.app/assets/hero-background.svg')
---

# Program do usuwania szumów periodycznych poprzez manipulację widmem fourierowskim

Igor Nowicki

Wyższa Szkoła Informatyki Stosowanej i Zarządzania
pod auspicjami Polskiej Akademii Nauk

----------

## Szumy periodyczne

Artefakty pochodzące z okresowych zakłóceń w procesie akwizycji obrazu.

##### Przykłady
<center>

<img src="images/brain-mri.jpg" height="250px">
<img src="images/old-photo.jpg" height="250px">
<img src="images/aerial-periodic.png" height="250px">

</center>

<div style="font-size:0.8rem">
Źródła:
<ul>
<li>https://ietresearch.onlinelibrary.wiley.com/doi/10.1049/iet-ipr.2018.5707</li>
<li>https://craftofcoding.wordpress.com/2017/02/18/image-processing-fun-with-fft-ii/</li>
</ul>
</div>

---------

## Idea

Dowolnie skomplikowaną funkcję ciągłą okresową możemy przybliżać za pomocą sinusów i cosinusów o coraz wyższych częstotliwościach.

<br>

<center>
<!-- <img src="images/animation-1.gif" width="45%"> -->
<!-- <img src="images/animation-2.gif" width="45%"> -->
<img src="images/fourier.png" width="50%">
</center>


---------

## Podstawa matematyczna

Obraz zostaje przeniesiony do dziedziny częstotliwości:

$$Im(x,y) \to F(x,y),$$

gdzie $F(x,y)$ jest widmem fourierowskim obrazu $Im(x,y)$.

$$Im(x,y) = \frac{1}{N^2} \sum_{u=0}^{N-1} \sum_{v=0}^{N-1} F(u,v) e^{-2\pi i \frac{ux+vy}{N}}$$

---------

## Zastosowanie

Widmo fourierowskie obrazu z szumem periodycznym ma łatwe do oddzielenia cechy w porównaniu do widma oryginalnego obrazu.

<br>
<center>
<img src="images/lena.png" width="200px">
<img src="images/lena-periodic.png" width="200px">
<img src="images/arrow.png" width="200px">
<img src="images/lena-fft.png" width="200px">
<img src="images/lena-periodic-fft.png" width="200px">
</center>

---------

## Algorytm usuwania szumów periodycznych

1. Przekształcenie obrazu do widma fourierowskiego
2. Zerowanie wartości z widma dla ustalonych zakresów wyższych częstotliwości
3. Odwrotne przekształcenie zmienionego widma do obrazu


<!-- ---------

## Metoda postępowania

Obróbka obrazu w dziedzinie częstotliwości

<style>.i1{height: 120px;}</style>

<center>
<img class="i1" src="images/aerial-periodic.png" >
<img class="i1" src="images/arrow.png" >
<img class="i1" src="images/aerial-fourier.png" >
<img class="i1" src="images/arrow.png" >
<img class="i1" src="images/aerial-fourier-removed.png" >
<img class="i1" src="images/arrow.png" >
<img class="i1" src="images/aerial-clean.png" >
</center>

<br>


<style>.i2{height: 250px;}</style>

<center>
<img class="i2" src="images/aerial-periodic.png" >
<img class="i2" src="images/arrow.png" >
<img class="i2" src="images/aerial-clean.png" >
</center> -->

---------

## Prezentacja

Użyty zostanie program przygotowany w ramach zajęć z przedmiotu Algorytmy przetwarzania obrazów: ImageProcessor.

<br>

<center>
<img src="images/ip-3.png" height="350px">
</center>

---------

## Opis środowiska programistycznego

Program został przygotowany w języku C#, w środowisku Visual Studio 2022, z użyciem frameworka .NET 6.
<br>
<center>
<img src="images/vs.png" height="350px">
</center>

---------

## Powłoka graficzna

Interfejs użytkownika został przygotowany z użyciem frameworka Avalonia, w architekturze model-widok-model widoku (MVVM).

<br>

<center>
<img src="images/avalonia.png" height="350px">
</center>

---------

## Architektura MVVM

- Widok odpowiada za wygląd zewnętrzny aplikacji,
- Model widoku odpowiada za interakcję widoku z modelem,
- Model odpowiada za przechowywanie i transformację danych.

<center>
<img src="images/mvvm.png" height="350px">
</center>

## Biblioteka programistyczna

Biblioteka programistyczna została przygotowana z użyciem modułów OpenCV oraz FFTW.NET.

<br>

<center>
<img src="images/opencv.png" height="200px">
</center>

---------

## Moduł testów jednostkowych

Testy jednostkowe zostały przygotowane z użyciem biblioteki MSTest. Przeprowadzane testy gwarantują poprawność działania algorytmów oraz umożliwiają ich sprawną refaktoryzację.
<br>
<center>
<img src="images/mstest.png" height="350px">
</center>

---------

## Dokumentacja

Dokumentacja została wykonana z użyciem biblioteki DocFX, umożliwiającej automatyczne generowanie z kodu źródłowego.

<br>

<center>
<img src="images/docfx.png" width="80%">
</center>

---------

## System kontroli wersji

Do przechowywania kodu źródłowego i zarządzania wersjami zostało wykorzystane repozytorium GitHub.
<br>

<center>
<img src="images/github.png" width="70%">
</center>

Strona projektu: https://github.com/Ch3shireDev/ImageProcessor/

---------

## Zakończenie

Dziękuję za uwagę.

<br>

<center>
<img src="images/xkcd.png" width="50%">
</center>