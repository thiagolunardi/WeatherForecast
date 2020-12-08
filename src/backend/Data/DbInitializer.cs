using System;
using System.Collections.Generic;
using System.Linq;

namespace backend
{
    public static class DbInitializer
    {
        public static void Initialize(WeatherForecastDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Forecasts.Any())
            {
                return;   // DB has been seeded
            }

            var cities = new[] {"Berlin", "Amsterdan", "Sao Paulo", "Dublin", "Madrid", "Lisboa", "Copenhagen", "Paris", "Roma" };
            var clime = new[] { "Cool", "Warm", "Hot", "Freezing", "Unhuman" };

            var rnd = new Random();
            var forecasts = new List<WeatherForecast>();
            foreach (var city in cities)
            {
                var date = DateTime.Today;
                while (date - DateTime.Today < TimeSpan.FromDays(7))
                {                    
                    forecasts.Add(new WeatherForecast
                    {
                        Location = city,
                        Date = date,
                        TemperatureC = rnd.Next(-15, 49),
                        Summary = clime[rnd.Next(0,4)]
                    });
                    date = date.AddDays(1);
                }
            }

            context.Forecasts.AddRange(forecasts);
            context.SaveChanges();
        }
    }
}