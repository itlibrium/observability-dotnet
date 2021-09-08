using System;
using Microsoft.Extensions.Logging;

namespace Observability.ServiceA.Controllers
{
    public class Service
    {
        private readonly ILogger<Service> _logger;

        public Service(ILogger<Service> logger) => _logger = logger;

        public void DoSth() => _logger.LogInformation("Information from internal service at {Time}", DateTime.Now);
    }
}