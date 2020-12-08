using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GrpcBackend;
using Grpc.Net.Client;

namespace frontend
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            ConfigureServices(builder.Services);

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<GrpcChannel>(provider =>
            {
                var backendAddress = "http://localhost:34300";
                return GrpcChannel.ForAddress(backendAddress);
            });

            services.AddScoped<Forecast.ForecastClient>(provider =>
            {
                var channel = provider.GetService<GrpcChannel>();
                return new Forecast.ForecastClient(channel);
            });

            services.AddScoped<WeatherClient>();
        }
    }
}
