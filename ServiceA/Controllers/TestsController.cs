using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Observability.ServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly Service _service;
        private readonly ExternalService _externalService;
        private readonly ILogger<TestsController> _logger;

        public TestsController(Service service, ExternalService externalService, ILogger<TestsController> logger)
        {
            _service = service;
            _externalService = externalService;
            _logger = logger;
        }

        [HttpGet("simple")]
        public IActionResult Simple()
        {
            _logger.LogInformation("Message with {Int}, {String} and {@Object}",
                123,
                "lorem ipsum",
                new MyClass
                {
                    Date = DateTime.Now,
                    Text = "bla bla bla"
                });
            _service.DoSth();
            return Ok(Activity.Current);
        }

        private class MyClass
        {
            public DateTime Date { get; set; }
            public string Text { get; set; }
        }

        [HttpGet("with-db")]
        public Task<IActionResult> WithDb() => throw new NotImplementedException();

        [HttpGet("with-external-service-sync")]
        public async Task<IActionResult> WithExternalServiceSync()
        {
            _logger.LogInformation("Test with external service sync started");
            var forecast = await _externalService.GetWeatherForecast();
            return Ok(forecast);
        }

        [HttpGet("with-external-service-async")]
        public Task<IActionResult> WithExternalServiceAsync() => throw new NotImplementedException();

        [HttpGet("with-exception")]
        public Task<IActionResult> WithException() => throw new NotImplementedException();

        [HttpGet("with-exception-in-external-service")]
        public Task<IActionResult> WithExceptionInExternalService() => throw new NotImplementedException();

        [HttpGet("complex")]
        public Task<IActionResult> Complex() => throw new NotImplementedException();
    }
}