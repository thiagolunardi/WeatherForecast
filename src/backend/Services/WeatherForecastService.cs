using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcBackend;
using Microsoft.Extensions.Logging;

namespace backend
{
    public class WeatherForecastService : Forecast.ForecastBase
    {
        private readonly ILogger _logger;
        private readonly IRepository _repository;
        
        public WeatherForecastService(ILogger<WeatherForecastService> logger, IRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public override async Task Predict(WeatherRequest request, IServerStreamWriter<WeatherResponse> responseStream, ServerCallContext context)
        {
            var forecast = _repository.GetForecastBy(request.Location);
            await foreach (var item in forecast)
            {
                await responseStream.WriteAsync(new WeatherResponse
                {
                    Date = Timestamp.FromDateTimeOffset(item.Date),
                    TemperatureC = item.TemperatureC,
                    Summary = item.Summary
                });
            }
        }
    }
}
