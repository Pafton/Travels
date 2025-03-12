# Travels Domain Entities

Projekt przedstawia strukturę encji związanych z systemem podróży, obejmującym miejsca docelowe, hotele, transport, rezerwacje, opinie, użytkowników i oferty podróży. Poniżej znajduje się szczegółowy opis każdej klasy w systemie.

## Klasy

### 1. `Destination`
Reprezentuje miejsce docelowe podróży.

- **Id**: Unikalny identyfikator miejsca docelowego.
- **Name**: Nazwa miejsca docelowego.
- **Country**: Kraj, w którym znajduje się miejsce docelowe.
- **City**: Miasto, w którym znajduje się miejsce docelowe.
- **Description**: Opis miejsca docelowego.
- **Trips**: Kolekcja podróży, które odwiedzają to miejsce.

### 2. `Hotel`
Reprezentuje hotel, w którym mogą zatrzymać się podróżni.

- **Id**: Unikalny identyfikator hotelu.
- **Name**: Nazwa hotelu.
- **Address**: Adres hotelu.
- **Rating**: Ocena hotelu.
- **Trips**: Kolekcja podróży, które zawierają ten hotel w swojej ofercie.

### 3. `Reservation`
Reprezentuje rezerwację dokonana przez użytkownika na określoną podróż.

- **Id**: Unikalny identyfikator rezerwacji.
- **UserId**: Identyfikator użytkownika, który dokonał rezerwacji.
- **User**: Użytkownik, który dokonał rezerwacji.
- **TripId**: Identyfikator podróży, której dotyczy rezerwacja.
- **Trip**: Podróż, na którą została dokonana rezerwacja.
- **ReservationDate**: Data dokonania rezerwacji.
- **Status**: Status rezerwacji (np. potwierdzona, anulowana).
- **TotalAmount**: Całkowita kwota rezerwacji.

### 4. `Review`
Reprezentuje opinię użytkownika na temat danej podróży.

- **Id**: Unikalny identyfikator opinii.
- **Comment**: Komentarz użytkownika.
- **Rating**: Ocena podróży (zazwyczaj w skali 1-5).
- **Date**: Data wystawienia opinii.
- **UserIdentifier**: Unikalny identyfikator użytkownika (np. e-mail).
- **TripId**: Identyfikator podróży, do której odnosi się opinia.
- **Trip**: Podróż, której dotyczy opinia.
- **IsEditable**: Flaga wskazująca, czy opinia może być edytowana przez użytkownika.

### 5. `Role`
Enum, który określa rolę użytkownika w systemie.

- **Admin**: Administrator systemu.
- **Customer**: Klient (zwykły użytkownik).

### 6. `Transport`
Reprezentuje środek transportu, który jest częścią podróży.

- **Id**: Unikalny identyfikator środka transportu.
- **Type**: Typ środka transportu (np. samolot, pociąg).
- **Company**: Firma odpowiedzialna za dany środek transportu.
- **Trips**: Kolekcja podróży, które korzystają z tego środka transportu.

### 7. `TravelOffer`
Reprezentuje ofertę podróży dostępną w systemie.

- **Id**: Unikalny identyfikator oferty podróży.
- **Title**: Tytuł oferty.
- **Description**: Opis oferty.
- **Price**: Cena oferty.
- **Begin**: Data rozpoczęcia oferty.
- **End**: Data zakończenia oferty.
- **AvailableSpots**: Liczba dostępnych miejsc w ofercie.

### 8. `Trip`
Reprezentuje pojedynczą podróż, obejmującą miejsce docelowe, transport, hotel i opinie.

- **Id**: Unikalny identyfikator podróży.
- **Name**: Nazwa podróży.
- **Description**: Opis podróży.
- **StartDate**: Data rozpoczęcia podróży.
- **EndDate**: Data zakończenia podróży.
- **Price**: Cena podróży.
- **DestinationId**: Identyfikator miejsca docelowego podróży.
- **Destination**: Miejsce docelowe podróży.
- **Hotels**: Kolekcja hoteli związanych z tą podróżą.
- **Transports**: Kolekcja środków transportu związanych z tą podróżą.
- **Reviews**: Kolekcja opinii na temat tej podróży.

### 9. `User`
Reprezentuje użytkownika systemu.

- **Id**: Unikalny identyfikator użytkownika.
- **Name**: Imię użytkownika.
- **Surname**: Nazwisko użytkownika.
- **Email**: Adres e-mail użytkownika.
- **Password**: Hasło użytkownika.
- **Role**: Rola użytkownika (np. Administrator lub Klient).

## Jak działa system?

System pozwala na zarządzanie podróżami, rezerwacjami, ofertami, opiniami oraz transportem. Użytkownicy mogą przeglądać dostępne oferty, rezerwować podróże i wystawiać opinie na temat podróży, w których uczestniczyli. Dodatkowo, system wspiera zarządzanie różnymi rolami użytkowników, takimi jak administrator i klient.

---

**Projekt: Travels**  
**Data: Marzec 2025**
