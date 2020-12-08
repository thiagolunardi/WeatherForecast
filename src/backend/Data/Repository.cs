using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace backend
{
    public interface IRepository 
    {
        IAsyncEnumerable<WeatherForecast> GetForecastBy(string location);
    }

    public class Repository : IRepository
    {
        private readonly WeatherForecastDbContext _dbContext;

        public Repository(WeatherForecastDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAsyncEnumerable<WeatherForecast> GetForecastBy(string location)
        {
            return _dbContext.Forecasts
                .Where(f => f.Location == location)
                .AsNoTracking()
                .AsAsyncEnumerable();
        }
    }
}
