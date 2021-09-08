using System;
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
            var traceId = GetOrAddProperty(activity, TraceId, a => a.TraceId.ToString());
            logEvent.AddPropertyIfAbsent(traceId);
            var spanId = GetOrAddProperty(activity, SpanId, a => a.SpanId.ToString());
            logEvent.AddPropertyIfAbsent(new LogEventProperty(SpanId, new ScalarValue(activity.SpanId.ToString())));
        }

        private static LogEventProperty GetOrAddProperty(Activity activity, string name,
            Func<Activity, string> valueFactory)
        {
            var property = activity.GetCustomProperty(name);
            if (property is LogEventProperty logEventProperty)
                return logEventProperty;
            logEventProperty = new LogEventProperty(name, new ScalarValue(valueFactory(activity)));
            activity.SetCustomProperty(name, logEventProperty);
            return logEventProperty;
        }
    }
}