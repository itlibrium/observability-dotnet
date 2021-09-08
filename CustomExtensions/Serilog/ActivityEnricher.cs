using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace CustomExtensions.Serilog
{
    public class ActivityEnricher : ILogEventEnricher
    {
        private const string TraceId = "TraceId";
        private const string SpanId = "SpanId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var activity = Activity.Current;
            if (activity is null)
                return;
            // TODO: performance optimization
            logEvent.AddPropertyIfAbsent(new LogEventProperty(TraceId, new ScalarValue(activity.TraceId.ToString())));
            logEvent.AddPropertyIfAbsent(new LogEventProperty(SpanId, new ScalarValue(activity.SpanId.ToString())));
        }
    }
}