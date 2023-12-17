using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TemplateDDD.Shared.UnitOfWork
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();

            return services;
        }
    }
}