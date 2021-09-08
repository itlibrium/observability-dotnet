using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Observability.CustomExtensions.Serilog;
using Serilog;
using Serilog.Formatting.Json;

namespace Observability.ServiceA
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .UseSerilog((context, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithTracing()
                .Enrich.WithoutProperties(
                    "RequestId", // because we have TraceId and SpanId 
                    "ConnectionId", // because it's not needed
                    "ActionId", // because it isn't useful and change after each deploy
                    "ActionName", // because it's not important in every log and requests data is also present in traces 
                    "RequestPath") // as above
                .WriteTo.Console(new JsonFormatter()))
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}