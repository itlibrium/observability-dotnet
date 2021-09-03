using System.Net.Http;
using System.Threading.Tasks;

namespace Telemetry.ServiceA
{
    public class ExternalService
    {
        private readonly HttpClient _httpClient;
        
        public ExternalService(HttpClient httpClient) => _httpClient = httpClient;

        public Task<string> GetWeatherForecast() => _httpClient.GetStringAsync("weather-forecast");
    }
}