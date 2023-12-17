using Microsoft.Extensions.DependencyInjection;

namespace TemplateDDD.Shared.Services.ApiCaller
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiCaller(this IServiceCollection services)
        {
            services.AddSingleton<IApiCaller, ApiCaller>();

            return services;
        }
    }
}