﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Telemetry.ServiceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ExternalService _externalService;
        private readonly ILogger<TestsController> _logger;

        public TestsController(ExternalService externalService, ILogger<TestsController> logger)
        {
            _externalService = externalService;
            _logger = logger;
        }

        [HttpGet("simple")]
        public IActionResult Simple()
        {
            _logger.LogInformation("Executing simple scenario");
            return Ok(Activity.Current);
        }

        [HttpGet("with-db")]
        public Task<IActionResult> WithDb() => throw new NotImplementedException();
        
        [HttpGet("with-external-service-sync")]
        public async Task<IActionResult> WithExternalServiceSync()
        {
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