using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Observability.ServiceB.Controllers
{
    [ApiController]
    [Route("weather-forecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private static readonly Random Random = new(DateTime.Now.Millisecond);

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger) => _logger = logger;

        [HttpGet]
        public WeatherForecast Get(DateTime? date = null)
        {
            if (date > DateTime.Now.Date.AddDays(7))
                throw new ArgumentException("We don't provide forecast for more than 7 days.");
            
            _logger.LogInformation("Headers: {Headers}", HttpContext.Request.Headers);
            var forecast = new WeatherForecast
            {
                Date = date ?? DateTime.Now,
                TemperatureC = Random.Next(-20, 55),
                Summary = Summaries[Random.Next(Summaries.Length)]
            };
            _logger.LogInformation("The forecast is {@Forecast}",
                new {forecast.Date, forecast.Summary, forecast.TemperatureC});
            return forecast;
        }
    }
}