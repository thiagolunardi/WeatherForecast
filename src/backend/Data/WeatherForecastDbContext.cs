using Microsoft.EntityFrameworkCore;

namespace backend
{
    public class WeatherForecastDbContext : DbContext
    {
        public WeatherForecastDbContext (DbContextOptions<WeatherForecastDbContext> options)
            : base(options)
        {
        }

        public DbSet<WeatherForecast> Forecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>().ToTable("Forecasts");
        }
    }
}