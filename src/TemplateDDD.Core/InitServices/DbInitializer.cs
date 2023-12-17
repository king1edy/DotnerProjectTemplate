using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TemplateDDD.Core.InitServices.Interfaces;

namespace TemplateDDD.Core.InitServices
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TemplateDDD.Core.AppDbContext>())
                {
                    if (context != null)
                    {
                        context.Database.Migrate();  // EnsureCreated();
                    }
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TemplateDDD.Core.AppDbContext>())
                {
                    if (context != null)
                    {
                        //BusinessOptionSeeder.Execute(context);
                    }
                }
            }
        }
    }
}