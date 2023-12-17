using TemplateDDD.Core.InitServices.Interfaces;

namespace TemplateDDD.APP.Common
{
    public static class StartupInit
    {
        public static void Run(IApplicationBuilder app)
        {
            SeedData(app);
        }

        private static void SeedData(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
                if (dbInitializer != null)
                {
                    dbInitializer.Initialize();
                    dbInitializer.SeedData();
                }
            }
        }
    }
}