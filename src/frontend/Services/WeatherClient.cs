using System.Collections.Generic;
using GrpcBackend;

namespace frontend
{
    public class WeatherClient
    {
        private readonly Forecast.ForecastClient _client;

        public WeatherClient(Forecast.ForecastClient client)
        {
            _client = client;
        }

        public async IAsyncEnumerable<WeatherForecast> GetWeatherAsync()
        {
            var request = new WeatherRequest
            {
                Location = "Berlin"
            };

            var response = _client.Predict(request);
            while (await response.ResponseStream.MoveNext(default))
            {
                var weather = response.ResponseStream.Current;

                yield return new WeatherForecast
                {
                    Date = weather.Date.ToDateTime(),
                    TemperatureC = weather.TemperatureC,
                    Summary = weather.Summary
                };
            }
        }
    }
}
