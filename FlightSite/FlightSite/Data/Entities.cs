using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSite.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace FlightSite.Data
{
    public class Entities : DbContext
    {
        public DbSet<Passenger> Passengers => Set<Passenger>();

        public DbSet<Flight> Flights => Set<Flight>();

        public Entities(DbContextOptions<Entities> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Passenger>().HasKey(p=>p.Email);

            modelBuilder.Entity<Flight>().Property(p => p.RemainingNumberOfSeats)
                .IsConcurrencyToken();

            modelBuilder.Entity<Flight>().OwnsOne(f => f.Departure);
            modelBuilder.Entity<Flight>().OwnsOne(f => f.Arrival);

        }
    }
}
