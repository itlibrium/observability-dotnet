using Serilog.Core;
using Serilog.Events;

namespace Observability.CustomExtensions.Serilog
{
    public class RemovePropertiesEnricher : ILogEventEnricher
    {
        private readonly string[] _propertiesToRemove;

        public RemovePropertiesEnricher(params string[] propertiesToRemove) => _propertiesToRemove = propertiesToRemove;

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            foreach (var property in _propertiesToRemove)
                logEvent.RemovePropertyIfPresent(property);
        }
    }
}