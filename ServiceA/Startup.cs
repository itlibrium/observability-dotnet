using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Observability.ServiceA.Controllers;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Observability.ServiceA
{
    public class Startup
    {
        private IConfiguration _configuration;
        
        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ServiceA", Version = "v1"});
            });
            services.AddOpenTelemetryTracing(config => config
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("ServiceA"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddSqlClientInstrumentation()
                .AddConsoleExporter()
                .AddJaegerExporter()
                .AddOtlpExporter(options => options.Endpoint = new Uri("http://localhost:8200")));
            services.AddScoped<Service>();
            services.AddHttpClient<ExternalService>(config => config.BaseAddress = new Uri("http://localhost:5001"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ServiceA v1"));
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}