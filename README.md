Travels Domain Entities
Projekt przedstawia strukturę encji związanych z systemem podróży, obejmującym miejsca docelowe, hotele, transport, rezerwacje, opinie, użytkowników i oferty podróży. Poniżej znajduje się szczegółowy opis każdej klasy w systemie.

Klasy
1. Destination
Reprezentuje miejsce docelowe podróży.

Id: Unikalny identyfikator miejsca docelowego.

Name: Nazwa miejsca docelowego.

Country: Kraj, w którym znajduje się miejsce docelowe.

City: Miasto, w którym znajduje się miejsce docelowe.

Description: Opis miejsca docelowego.

TravelOffers: Kolekcja ofert podróży związanych z tym miejscem.

2. Hotel
Reprezentuje hotel, w którym mogą zatrzymać się podróżni.

Id: Unikalny identyfikator hotelu.

Name: Nazwa hotelu.

Address: Adres hotelu.

Rating: Ocena hotelu (liczbowo).

TravelOffers: Kolekcja ofert podróży zawierających ten hotel.

3. Reservation
Reprezentuje rezerwację dokonaną przez użytkownika na określoną ofertę podróży.

Id: Unikalny identyfikator rezerwacji.

UserId: Identyfikator użytkownika.

User: Użytkownik, który dokonał rezerwacji.

TravelOfferId: Identyfikator oferty podróży.

TravelOffer: Oferta podróży, której dotyczy rezerwacja.

ReservationDate: Data dokonania rezerwacji.

Status: Status rezerwacji (prawda/fałsz - np. potwierdzona lub anulowana).

4. Review
Reprezentuje opinię użytkownika na temat oferty podróży.

Id: Unikalny identyfikator opinii.

Comment: Komentarz użytkownika.

Rating: Ocena oferty podróży (zazwyczaj w skali 1-5).

Date: Data wystawienia opinii.

User: Powiązany użytkownik (jeśli zalogowany).

UserId: Id użytkownika (może być null).

NotLogginUser: Nazwa niezalogowanego użytkownika (jeśli dotyczy).

TravelOfferId: Identyfikator oferty podróży.

TravelOffer: Oferta, której dotyczy opinia.

IsEditable: Flaga wskazująca, czy opinia może być edytowana przez użytkownika.

5. Role
Enum określający rolę użytkownika w systemie.

Admin: Administrator systemu.

Customer: Klient (zwykły użytkownik).

6. Transport
Reprezentuje środek transportu używany w ofercie podróży.

Id: Unikalny identyfikator środka transportu.

Type: Typ środka transportu (np. samolot, pociąg).

Company: Firma świadcząca usługę transportową.

TravelOffers: Kolekcja ofert podróży korzystających z danego transportu.

7. TravelOffer
Reprezentuje ofertę podróży dostępną w systemie.

Id: Unikalny identyfikator oferty podróży.

Title: Tytuł oferty.

Description: Opis oferty.

Price: Cena oferty.

Begin: Data rozpoczęcia.

End: Data zakończenia.

AvailableSpots: Liczba dostępnych miejsc.

DestinationId: Identyfikator miejsca docelowego.

Destination: Powiązane miejsce docelowe.

Reservations: Lista rezerwacji tej oferty.

Hotels: Lista hoteli powiązanych z tą ofertą.

Transports: Lista środków transportu dla tej oferty.

Reviews: Lista opinii dotyczących tej oferty.

8. User
Reprezentuje użytkownika systemu.

Id: Unikalny identyfikator użytkownika.

Name: Imię użytkownika.

Surname: Nazwisko użytkownika.

Email: Adres e-mail.

Password: Hasło.

Role: Rola w systemie (Admin lub Customer).

Reviews: Lista opinii wystawionych przez użytkownika.

Reservations: Lista rezerwacji użytkownika.

PasswordResetTokens: Lista tokenów resetowania hasła.

isActivate: Flaga aktywacji konta.

Jak działa system?
System pozwala na zarządzanie podróżami, rezerwacjami, ofertami, opiniami oraz transportem. Użytkownicy mogą przeglądać dostępne oferty, rezerwować podróże i wystawiać opinie na temat ofert, z których skorzystali. System wspiera również zarządzanie kontami użytkowników, ich aktywacją oraz przypisaniem ról (administrator/klient).

Projekt: Travels
Data: Marzec 2025
