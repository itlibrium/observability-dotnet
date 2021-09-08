using Serilog;
using Serilog.Configuration;

namespace CustomExtensions.Serilog
{
    public static class SerilogConfigurationExtensions
    {
        public static LoggerConfiguration WithoutProperties(this LoggerEnrichmentConfiguration configuration,
            params string[] propertiesToRemove) =>
            configuration.With(new RemovePropertiesEnricher(propertiesToRemove));
        
        public static LoggerConfiguration WithTracing(this LoggerEnrichmentConfiguration configuration) =>
            configuration.With<ActivityEnricher>();
    }
}