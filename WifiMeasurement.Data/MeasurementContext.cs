using WifiMeasurement.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace WifiMeasurement.Data
{
    internal class MeasurementContext : DbContext
    {
        public MeasurementContext(DbContextOptions<MeasurementContext> options) : base(options)
        {
        }

        public DbSet<Device> Devices { get; set; }

        public DbSet<Measurement> Measurements { get; set; }

        public DbSet<TestSeries> TestSeries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Device>()
                .HasKey(u => u.DeviceId);
            modelBuilder
                 .Entity<Device>()
                 .HasMany(u => u.TestSeries)
                 .WithOne(x => x.Device)
                 .HasForeignKey(x => x.DeviceId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<TestSeries>()
                .HasKey(u => u.TestSeriesId);
            modelBuilder
                 .Entity<TestSeries>()
                 .HasMany(u => u.Measurements)
                 .WithOne(x => x.TestSeries)
                 .HasForeignKey(x => x.TestSeriesId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder
                .Entity<Measurement>()
                .HasKey(u => u.MeasurementId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
