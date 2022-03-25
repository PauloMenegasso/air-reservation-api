using AirReservationApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace AirReservationApi.Infra.Database
{
    public class AirReservationDBContext : DbContext
    {
        public DbSet<AirReservation> AirReservations { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public AirReservationDBContext(DbContextOptions<AirReservationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Airport>()
                .HasIndex(a => a.Code)
                .IsUnique();
        }
    }
}