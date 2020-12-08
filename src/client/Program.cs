using System;
using Grpc.Net.Client;
using GrpcBackend;
using Newtonsoft.Json;

namespace client
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:34300");
            var client = new Forecast.ForecastClient(channel);

            var items = client.Predict(new WeatherRequest { Location = "Berlin" });

            while (await items.ResponseStream.MoveNext(default))
            {
                var item = items.ResponseStream.Current;

                Console.WriteLine(JsonConvert.SerializeObject(item));
            }
        }
    }
}
