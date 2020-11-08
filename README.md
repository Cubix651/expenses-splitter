# Expenses Splitter

Expenses Splitter to aplikacja umożliwiająca rozliczenia grupowych wydatków. Użytkownicy mogą dodawać wydatki, za które płacili oraz oznaczać, kto ma oddać pieniądze za poniesione koszty. Aplikacja sama rozliczy, kto komu powinien przekazać pieniądze, aby wyrównać rachunki oraz zminimalizuje liczbę potrzebnych transakcji.

Aplikacja będzie wykonana w technologii webowej. Frontend zostanie wykonany z wykorzsytaniem frameworka Angular, zaś backend w języku C#.

Aplikacja jest inspirowana istniejącymi rozwiązaniami:
* https://www.tricount.com
* https://www.kittysplit.com

## Funkcjonalność

Aplikacja będzie rozwijana w sposób iteracyjny. Poniżej znajduje się lista zaplanowanych funkcjonalności, które będą implementowane w kolejnych fazach rozwoju.

- [ ] Stworzenie nowego rozliczenia
- [ ] Użytkownicy mają dostęp do rozliczenia za pomocą linku
    * Każdy może coś zmienić w rozliczeniu
- [ ] Konfiguracja osób w rozliczeniu
  - [ ] Liczba osób i imiona/nicki
  - [ ] Dodatkowe informacje o osobach (nr konta, nr telefonu...)
  - [ ] Podgrupy w ramach głównej grupy (np. podróżowanie w 2 samochodach)
- [ ] Dodawanie wydatków do rozliczenia
  - [ ] Równomierny podział pomiędzy wszystkich w grupie
  - [ ] Równomierny podział pomiędzy zaznaczone osoby
  - [ ] Podział względem podanych wag przy osobach
- [ ] Opłacanie wydatku w rozliczeniu
  - [ ] Oznaczenie, że wydatek został zapłacony
  - [ ] Automatyzacja płatności przez aplikację
- [ ] Widok podsumowania rozliczenia
  - [ ] Kto zapłacił
  - [ ] Kto ma jaki dług i względem kogo
- [ ] Powiadomienia dotyczące rozliczenia
  - [ ] Mailowe
  - [ ] SMS
- [ ] Logowanie do aplikacji
  - [ ] Lista wszystkich rozliczeń dla zalogowanego użytkownika
  - [ ] Role/uprawnienia użytkowników
    - [ ] Administrator oraz użytkownicy z uprawnieniem tylko do odczytu
    - [ ] Użytkownicy z uprawnieniami do oznaczenia uregulowania długu
    - [ ] Użytkownicy z uprawnieniami do modyfikacji wydatków
- [ ] Zaawansowana konfiguracja osób
  - [ ] Małżeństwa/pary/większe grupy - jedna osoba płaci za pozostałe

## How-to

Aby szybko rozpocząć pracę nad projektem wystarczy skorzystać z dockera.

### Zbudowanie obrazów

```bash
> docker-compose build
```

### Uruchomienie

```bash
> docker-compose up
```

Wszystkie komponenty składające się na projekt zostaną uruchomione w trybie developmentu - zmiany w plikach źródłowych będą monitorowane, a odpowiednie komponenty rekompilowane: zarówno po stronie web-api jak i frontendu.

### Migracje EF

Po zmianie modeli dla bazy danych, wystarczy wydać następujące polecenia:

```bash
> docker-compose stop web-api
> docker-compose run --rm web-api dotnet ef migrations add MigrationName
> docker-compose restart web-api
```

Podczas uruchomiania web-api automatycznie aplikowane są wszystkie migracje.
