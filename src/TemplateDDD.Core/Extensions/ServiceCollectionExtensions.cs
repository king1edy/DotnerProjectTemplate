using Microsoft.Extensions.DependencyInjection;
using TemplateDDD.Core.Services;
using TemplateDDD.Core.Services.Interfaces;

namespace TemplateDDD.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IMonitoringFailedHNITxnService, MonitoringFailedHNITxnService>();

            return services;
        }

        public static IServiceCollection AddDbInit(this IServiceCollection services)
        {
            //services.AddScoped<IDbInitializer, DbInitializer>();

            return services;
        }
    }
}