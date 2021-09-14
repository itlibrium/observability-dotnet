using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Observability.ServiceA.Controllers
{
    public class ExternalService
    {
        private readonly HttpClient _httpClient;

        public ExternalService(HttpClient httpClient) => _httpClient = httpClient;

        public Task<string> GetWeatherForecast(DateTime? date = null)
        {
            return _httpClient.GetStringAsync(date is null
                ? "weather-forecast"
                : $"weather-forecast?date={date.Value.ToShortDateString()}");
        }
    }
}