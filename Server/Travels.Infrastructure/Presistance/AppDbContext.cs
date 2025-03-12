using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Travels.Domain.Entities;

namespace Travels.Infrastructure.Presistance
{
    public class AppDbContext : DbContext
    {
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<TravelOffer> TravelOffers { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder
                .UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relacja między TravelOffer a Destination (1:N)
            modelBuilder.Entity<TravelOffer>()
                .HasOne(to => to.Destination)
                .WithMany(d => d.TravelOffers)
                .HasForeignKey(to => to.DestinationId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja między TravelOffer a Hotel (M:N)
            modelBuilder.Entity<TravelOffer>()
                .HasMany(to => to.Hotels)
                .WithMany(h => h.TravelOffers)
                .UsingEntity(j => j.ToTable("TravelOfferHotel"));

            // Relacja między TravelOffer a Transport (M:N)
            modelBuilder.Entity<TravelOffer>()
                .HasMany(to => to.Transports)
                .WithMany(t => t.TravelOffers)
                .UsingEntity(j => j.ToTable("TravelOfferTransport"));

            // Relacja między Reservation a User (N:1)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja między Reservation a TravelOffer (N:1)
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.TravelOffer)
                .WithMany(to => to.Reservations)
                .HasForeignKey(r => r.TravelOfferId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacja między Review a TravelOffer (N:1)
            modelBuilder.Entity<Review>()
                .HasOne(r => r.TravelOffer)
                .WithMany(to => to.Reviews)
                .HasForeignKey(r => r.TravelOfferId)
                .OnDelete(DeleteBehavior.Cascade);
/*
            // Relacja między User a Review (N:1)
            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(r => r.UserIdentifier)
                .OnDelete(DeleteBehavior.Restrict);*/

        }
    }
}

