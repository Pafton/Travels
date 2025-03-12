using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
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
                }

                if (!_appDbContext.Destinations.Any())
                {
                    var destinations = SeedDestinations();
                    _appDbContext.Destinations.AddRange(destinations);
                }

                if (!_appDbContext.Hotels.Any())
                {
                    var hotels = SeedHotels();
                    _appDbContext.Hotels.AddRange(hotels);
                }

                if (!_appDbContext.Transports.Any())
                {
                    var transports = SeedTransports();
                    _appDbContext.Transports.AddRange(transports);
                }

                if (!_appDbContext.TravelOffers.Any())
                {
                    var travelOffers = SeedTravelOffers();
                    _appDbContext.TravelOffers.AddRange(travelOffers);
                }

                if (!_appDbContext.Reviews.Any())
                {
                    var reviews = SeedReviews();
                    _appDbContext.Reviews.AddRange(reviews);
                }

                if (!_appDbContext.TravelOfferTransports.Any())
                {
                    var travelOfferTransports = SeedTravelOfferTransports();
                    _appDbContext.TravelOfferTransports.AddRange(travelOfferTransports);
                }

                if (!_appDbContext.TravelOfferHotels.Any())
                {
                    var travelOfferHotels = SeedTravelOfferHotels();
                    _appDbContext.TravelOfferHotels.AddRange(travelOfferHotels);
                }

                if (!_appDbContext.Reservations.Any())
                {
                    var reservations = SeedReservations();
                    _appDbContext.Reservations.AddRange(reservations);
                }

                _appDbContext.SaveChanges();
            }
        }

        private IEnumerable<User> SeedUser()
        {
            var users = new List<User>()
            {
                new User() { Name = "Jan", Surname = "Kowalski", Email = "jan.kowalski@test.com", Password = "jankowalski", Role = Role.Admin },
                new User() { Name = "Anna", Surname = "Nowak", Email = "anna.nowak@test.com", Password = "annanowak", Role = Role.Customer },
                new User() { Name = "Paweł", Surname = "Zieliński", Email = "pawel.zielinski@test.com", Password = "pawelzielinski", Role = Role.Customer },
                new User() { Name = "Ewa", Surname = "Bąk", Email = "ewa.bak@test.com", Password = "ewabak", Role = Role.Customer }
            };
            foreach (var user in users)
            {
                user.Password = _passwordHasher.HashPassword(user, user.Password);
            }
            return users;
        }

        private IEnumerable<Destination> SeedDestinations()
        {
            return new List<Destination>()
            {
                new Destination() { Name = "Malediwy", Country = "Malediwy", City = "Male", Description = "Tropikalny raj z białymi plażami i krystalicznie czystą wodą." },
                new Destination() { Name = "Paryż", Country = "Francja", City = "Paryż", Description = "Miasto miłości i romansu, znane z Wieży Eiffla." },
                new Destination() { Name = "Nowy Jork", Country = "USA", City = "Nowy Jork", Description = "Tętniące życiem miasto, które nigdy nie śpi, pełne drapaczy chmur i kultury." }
            };
        }

        private IEnumerable<Hotel> SeedHotels()
        {
            return new List<Hotel>()
            {
                new Hotel() { Name = "Resort Plażowy", Address = "Ocean Avenue, Malediwy", Rating = 5 },
                new Hotel() { Name = "Luksusowy Eiffel", Address = "Rue de Paris, Francja", Rating = 4 },
                new Hotel() { Name = "Central Park Hotel", Address = "5th Avenue, Nowy Jork", Rating = 5 }
            };
        }

        private IEnumerable<Transport> SeedTransports()
        {
            return new List<Transport>()
            {
                new Transport() { Type = "Lot", Company = "Air Maldives" },
                new Transport() { Type = "Pociąg", Company = "EuroStar" },
                new Transport() { Type = "Autobus", Company = "New York Transport Co." }
            };
        }

        private IEnumerable<TravelOffer> SeedTravelOffers()
        {
            var transport = _appDbContext.Transports.First();
            return new List<TravelOffer>()
            {
                new TravelOffer() { Title = "Tropikalna Ucieczka",Description = "Spędź tydzień na Malediwach w luksusowym resorcie nad plażą", Price = 2500.0, Begin = new DateOnly(2025, 06, 01), End = new DateOnly(2025, 06, 07), AvailableSpots = 10, DestinationId = 1 },
                new TravelOffer() { Title = "Romantyzm Paryża", Description = "Doświadcz piękna Paryża z przewodnikiem", Price = 1800.0, Begin = new DateOnly(2025, 07, 15), End = new DateOnly(2025, 07, 22), AvailableSpots = 15, DestinationId = 2 },
                new TravelOffer() { Title = "Przygoda w Nowym Jorku", Description = "Poznaj ikoniczne miejsca Nowego Jorku", Price = 2200.0, Begin = new DateOnly(2025, 08, 01), End = new DateOnly(2025, 08, 07), AvailableSpots = 20, DestinationId = 3 }
            };
        }

        private IEnumerable<Review> SeedReviews()
        {
            return new List<Review>()
            {
                new Review() { Rating = 5, Comment = "Wspaniała podróż, Malediwy to prawdziwy raj!", UserId = 1, TravelOfferId = 1 },
                new Review() { Rating = 4, Comment = "Paryż jest piękny, ale tłumy turystów mogą być męczące.", UserId = 2, TravelOfferId = 2 },
                new Review() { Rating = 5, Comment = "Nowy Jork to miasto pełne energii, świetna przygoda!", UserId = 3, TravelOfferId = 3 }
            };
        }

        private IEnumerable<Reservation> SeedReservations()
        {
            return new List<Reservation>()
            {
                new Reservation() { UserId = 1, TravelOfferId = 1, ReservationDate = DateTime.Now },
                new Reservation() { UserId = 2, TravelOfferId = 2, ReservationDate = DateTime.Now },
                new Reservation() { UserId = 3, TravelOfferId = 3, ReservationDate = DateTime.Now }
            };
        }
    }
}
