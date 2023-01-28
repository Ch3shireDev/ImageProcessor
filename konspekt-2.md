# Program do usuwania szumów periodycznych poprzez manipulację widmem fourierowskim

## Wstęp
Moim zadaniem było napisać program do usuwania szumów periodycznych z obrazów przez manipulację widmem fourierowskim.

## Opis problemu
Szumy periodyczne to szczególny rodzaj zakłócenia, które powtarza się w regularnych odstępach. Bierze się on m. in. ze sprzężeń elektrycznych w urządzeniach rejestrujących obraz, np. w skanerach. Problem usuwania szumów periodycznych pojawia się często przy digitalizacji starych zdjęć lub dokumentacji medycznej.

(obrazy zaszumionego starego zdjęcia i zaszumionego zdjęcia rentgenowskiego)

## Przedstawienie ogólnej metody

Do usuwania szumów periodycznych wykorzystuje się obróbkę widma fourierowskiego. W tym celu należy wykonać transformację fourierowską obrazu, a następnie wyzerować część widma odpowiadającą częstotliwościom szumu. Następnie wykonuje się odwrotną transformację fourierowską, aby uzyskać obraz bez szumu.

(zaszumiony obraz)

(widmo fourierowskie zaszumionego obrazu)

(widmo fourierowskie zaszumionego obrazu po wyzerowaniu części odpowiadającej częstotliwościom szumu)

(obraz po odwrotnej transformacji fourierowskiej, bez szumu)

Idea tworzenia widma fourierowskiego obrazu opiera się na fakcie, że obraz jest funkcją dyskretną dwóch zmiennych. Każdą funkcję dyskretną można przedstawić jako sumę sinusoid o różnych częstotliwościach i amplitudach.

(animacja pokazująca przybliżanie skomplikowanej funkcji za pomocą sinusów i cosinusów o coraz wyższych częstotliwościach).

Po przekształceniu obrazu, czyli funkcji dwuwymiarowej, do dziedziny częstotliwości, uzyskujemy tak naprawdę funkcję czterowymiarową, to znaczy funkcję zespoloną dwóch zmiennych. Możemy zatem myśleć o tej funkcji jak o dwóch obrazach, jednym przedstawiającym amplitudy obrazu dla różnych częstotliwości, a drugim jego fazę. Nas bedzie głównie interesowało widmo amplitudowe.

(przedstawienie obrazu i jego widma)

Na tym obrazie mamy obraz i jego widmo amplitudowe. Im bliżej środka, tym niższe częstotliwości, im dalej, tym większe. Na podstawie tego widma możemy wyczytać, że obraz nie ma wielu powtarzalnych fragmentów, a większość jego informacji znajduje się w środku, tam gdzie częstotliwości są niewielkie. Dodatkowo białe osie pomagają w wyznaczaniu osi symetrii całego obrazu.

Zobaczmy teraz co się dzieje z widmem obrazu po dodaniu szumu periodycznego.

Widzimy dodatkowy detal na widmie obrazu. Dodatkowo, ten detal jest zazwyczaj separowalny od reszty widma. Możemy więc wyzerować część widma odpowiadającą częstotliwościom szumu, a następnie odwrócić transformację fourierowską, aby uzyskać obraz bez szumu.

## Opis algorytmu

Algorytm transformacji Fouriera na dwuwymiarowej macierzy wartości (czyli obrazie) polega na dokonaniu transformacji na każdym wierszu, a następnie na każdej kolumnie. W ten sposób uzyskujemy macierz, która jest widmem obrazu. W celu odwrócenia transformacji fourierowskiej należy dokonać odwrotnej transformacji na każdym wierszu, a następnie na każdej kolumnie.

Jednowymiarowa transformacja Fouriera na liście wartości polega na znalezieniu listy liczb zespolonych, dla których będzie spełnione następujące równanie:

$$ f(k) = \sum_{k=0}^{N-1} F(k) \text{exp}(-2\pi i k x/N),$$

gdzie $f(k)$ jest wartością funkcji $f$ dla $k$-tego elementu listy, $F(k)$ jest wartością funkcji $F$ dla $k$-tego elementu listy, $x$ jest wartością funkcji $f$ dla $k$-tego elementu listy, $N$ jest długością listy.

Wartość $\text{exp}(-2\pi i k x/N)$ jest sposobem na skrócony zapis $\cos(2\pi k x/N) - i\sin(2\pi k x/N)$. Zatem proces znajdywania wartości $F(k)$ sprowadza się do znajdywania takich amplitud i przesunięć sinusów i cosinusów, które będą dawały wartości $f(k)$.

(animacja rozkładu wartości funkcji na sinusy i cosinusy)

## Przedstawienie działania programu

bzium bzium bzium

## Przedstawienie programu

Program graficzny jest oparty o architekturę MVVM - model - widok - model widoku - gdzie jest separacja pomiędzy widokiem - czyli kodem odpowiedzialnym za wizualne rozmieszczenie interfejsu graficznego - modelem widoku - czyli kodem odpowiedzialnym za logikę interfejsu graficznego - a modelem - czyli kodem odpowiedzialnym za przetwarzanie obrazów.

Kod przetwarzania obrazów umieszczony jest w bibliotece ImageProcessorLibrary, jako separowalny od kodu interfejsu graficznego. Struktura biblioteki była tworzona w doktrynie TDD z wykorzystaniem zasad SOLID, by zapewnić jak najmniejsze sprzęganie między klasami i stabilność wykonywanego kodu.

## Przedstawienie dokumentacji

- [x] Zrozumienie stosowalności i założeń projektu
- [x] Merytoryczna kompetncja - przez podanie podstawy rozwiązania, zastosowanej metody implementacji funkcjonalności
- [x] Umiejętność prezentowania prezentacji
- [x] Umiejętność organizacji prezentacji
- [x] Struktura oprogramowania
- [x] Poziom komentowania kodu
- [x] Opis zmiennych, parametrów globalnych i lokalnych
- [x] Interface graficzny - funkcjonalność
- [x] interface graficzny - walory graficzne
- [x] ocena zgodności rezultatów z założeniami
- [ ] organizacja dokumentacji
- [ ] kompletność dokumentacji
- [ ] szata graficzna
- [ ] dobór obrazów demonstracyjnych
- [ ] dobór zrzutów z ekranów
- [ ] spis treści i inne narzędzia wyszukiwania informacji