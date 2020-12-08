using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace frontend.Pages
{
    public class IndexModel : PageModel
    {
        public List<WeatherForecast> Forecasts { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            Forecasts = new List<WeatherForecast>();
        }

        public async System.Threading.Tasks.Task OnGetAsync([FromServices]WeatherClient client)
        {
            await foreach (var item in client.GetWeatherAsync()) 
            {
                Forecasts.Add(item);
            }
        }
    }
}
