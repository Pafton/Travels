using Microsoft.AspNetCore.Identity;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Presistance
{
    public class PrepDatabase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPasswordHasher<User> _passwordHasher;

        public PrepDatabase(AppDbContext appDbContext, IPasswordHasher<User> passwordHasher)
        {
            _appDbContext = appDbContext;
            _passwordHasher = passwordHasher;
        }

        public void Seed()
        {
            if (_appDbContext.Database.CanConnect())
            {
                if (!_appDbContext.Users.Any())
                {
                    var users = SeedUser();
                    _appDbContext.Users.AddRange(users);
                    _appDbContext.SaveChanges();
                }

                if (!_appDbContext.Destinations.Any())
                {
                    var destinations = SeedDestinations();
                    _appDbContext.Destinations.AddRange(destinations);
                    _appDbContext.SaveChanges();
                }

                if (!_appDbContext.Hotels.Any())
                {
                    var hotels = SeedHotels();
                    _appDbContext.Hotels.AddRange(hotels);
                    _appDbContext.SaveChanges();
                }

                if (!_appDbContext.Transports.Any())
                {
                    var transports = SeedTransports();
                    _appDbContext.Transports.AddRange(transports);
                    _appDbContext.SaveChanges();
                }

                if (!_appDbContext.TravelOffers.Any())
                {
                    var travelOffers = SeedTravelOffers();
                    _appDbContext.TravelOffers.AddRange(travelOffers);
                    _appDbContext.SaveChanges();
                }

                if (!_appDbContext.Reservations.Any())
                {
                    var reservations = SeedReservations();
                    _appDbContext.Reservations.AddRange(reservations);
                    _appDbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<User> SeedUser()
        {
            var users = new List<User>()
            {
                new User() { Name = "Jan", Surname = "Kowalski", Email = "jan.kowalski@test.com", Password = "jankowalski", Role = Role.Admin,isActivate = true },
                new User() { Name = "Anna", Surname = "Nowak", Email = "anna.nowak@test.com", Password = "annanowak", Role = Role.Customer, isActivate = true },
                new User() { Name = "Paweł", Surname = "Zieliński", Email = "pawel.zielinski@test.com", Password = "pawelzielinski", Role = Role.Customer, isActivate = true },
                new User() { Name = "Ewa", Surname = "Bąk", Email = "ewa.bak@test.com", Password = "ewabak", Role = Role.Customer, isActivate = false}
            };
            foreach (var user in users)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }
            return users;
        }

        private IEnumerable<Destination> SeedDestinations()
        {
            var destination = new List<Destination>()
            {
                new Destination() { Name = "Malediwy", Country = "Malediwy", City = "Male", Description = "Tropikalny raj z białymi plażami i krystalicznie czystą wodą." },
                new Destination() { Name = "Paryż", Country = "Francja", City = "Paryż", Description = "Miasto miłości i romansu, znane z Wieży Eiffla." },
                new Destination() { Name = "Nowy Jork", Country = "USA", City = "Nowy Jork", Description = "Tętniące życiem miasto, które nigdy nie śpi, pełne drapaczy chmur i kultury." }
            };
            return destination;
        }

        private IEnumerable<Hotel> SeedHotels()
        {
            var hotel = new List<Hotel>()
            {
                new Hotel() { Name = "Resort Plażowy", Address = "Ocean Avenue, Malediwy", Rating = 5 },
                new Hotel() { Name = "Luksusowy Eiffel", Address = "Rue de Paris, Francja", Rating = 4 },
                new Hotel() { Name = "Central Park Hotel", Address = "5th Avenue, Nowy Jork", Rating = 5 }
            };
            return hotel;
        }

        private IEnumerable<Transport> SeedTransports()
        {
            var transport = new List<Transport>()
            {
                new Transport() { Type = "Lot", Company = "Air Maldives" },
                new Transport() { Type = "Pociąg", Company = "EuroStar" },
                new Transport() { Type = "Autobus", Company = "New York Transport Co." }
            };

            return transport;
        }

        private IEnumerable<TravelOffer> SeedTravelOffers()
        {
            var transportLot = _appDbContext.Transports.First(t => t.Type == "Lot");
            var transportTrain = _appDbContext.Transports.First(t => t.Type == "Pociąg");
            var transportBus = _appDbContext.Transports.First(t => t.Type == "Autobus");

            var hotelMaldives = _appDbContext.Hotels.First(h => h.Name == "Resort Plażowy");
            var hotelParis = _appDbContext.Hotels.First(h => h.Name == "Luksusowy Eiffel");
            var hotelNY = _appDbContext.Hotels.First(h => h.Name == "Central Park Hotel");

            var destinationMaldives = _appDbContext.Destinations.First(d => d.Name == "Malediwy");
            var destinationParis = _appDbContext.Destinations.First(d => d.Name == "Paryż");
            var destinationNY = _appDbContext.Destinations.First(d => d.Name == "Nowy Jork");

            var travelOffers = new List<TravelOffer>()
            {
                new TravelOffer()
                {
                    Title = "Przygoda w Nowym Jorku",
                    Description = "Poznaj ikoniczne miejsca Nowego Jorku",
                    Price = 2200.0,
                    Begin = new DateOnly(2025, 08, 01),
                    End = new DateOnly(2025, 08, 07),
                    AvailableSpots = 20,
                    DestinationId = destinationNY.Id,
                    Hotels = new List<Hotel> { hotelNY },
                    Transports = new List<Transport> { transportBus }

                },
                new TravelOffer()
                {
                    Title = "Wakacje w Paryżu",
                    Description = "Zwiedzanie Luwru, Wieży Eiffla i rejs po Sekwanie",
                    Price = 1800.0,
                    Begin = new DateOnly(2025, 06, 15),
                    End = new DateOnly(2025, 06, 22),
                    AvailableSpots = 25,
                    DestinationId = destinationParis.Id,
                    Hotels = new List<Hotel> { hotelParis },
                    Transports = new List<Transport> { transportTrain }
                },
                new TravelOffer()
                {
                    Title = "Rajskie Malediwy",
                    Description = "Luksusowy wypoczynek na białych plażach",
                    Price = 5000.0,
                    Begin = new DateOnly(2025, 11, 05),
                    End = new DateOnly(2025, 11, 15),
                    AvailableSpots = 10,
                    DestinationId = destinationMaldives.Id,
                    Hotels = new List<Hotel> { hotelMaldives },
                    Transports = new List<Transport> { transportLot }
                }
            };

            return travelOffers;

        }

        private IEnumerable<Reservation> SeedReservations()
        {
            var reservation = new List<Reservation>()
            {
                new Reservation() { UserId = 1, TravelOfferId = 1,Status = false, ReservationDate = DateTime.Now },
                new Reservation() { UserId = 2, TravelOfferId = 2,Status = false, ReservationDate = DateTime.Now },
                new Reservation() { UserId = 3, TravelOfferId = 3,Status = false, ReservationDate = DateTime.Now }
            };
            return reservation;
        }
    }
}
