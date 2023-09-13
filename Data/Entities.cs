using Flights.Domain.Entities;
using System;
using Microsoft.EntityFrameworkCore;

namespace Flights.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Plane> Flights => Set<Plane>();

        public Entities(DbContextOptions<Entities>options) : base(options)
        {
            

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(f => f.Email);

            modelBuilder.Entity<Plane>().Property(f => f.RemainingNumberOfSeats)
                .IsConcurrencyToken();

            modelBuilder.Entity<Plane>().OwnsOne(f => f.Departure);

            modelBuilder.Entity<Plane>().OwnsOne(f => f.Arrival);

            modelBuilder.Entity<Plane>().OwnsMany(f => f.Bookings);
        }

    }
}
