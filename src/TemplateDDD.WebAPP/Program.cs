using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TemplateDDD.APP.Extensions;
using TemplateDDD.Core;
using TemplateDDD.Core.Extensions;
using TemplateDDD.Core.Services.Interfaces;
using TemplateDDD.Data;
using TemplateDDD.Shared.Constants;
using TemplateDDD.Shared.Models;
using TemplateDDD.Shared.UnitOfWork;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(hostingContext.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}");
    });

    // Add services to the container.
    string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    IConfigurationSection appSettingsSection = builder.Configuration.GetSection(ApplicationConstants.AppSettingsKey);
    string[] AllowedCORSOrigins = builder.Configuration.GetSection(ApplicationConstants.AllowedCORSOrigins).Get<string[]>();

    builder.Services.Configure<Appsettings>(appSettingsSection);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(connectionString, o =>
        {
            o.EnableRetryOnFailure();
        }));

    builder.Services
        .AddUnitOfWork<AppDbContext>()
        .AddCoreServices()
        .AddDbInit();

    var mappingConfig = new MapperConfiguration(mc =>
    {
        mc.AddProfile(new AutoMapperProfile());
    });
    IMapper mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);

    builder.Services.AddCors(options => options.ConfigureCorsPolicy(AllowedCORSOrigins));

    #region Hangfire configuration
    // Configure hangfire here when needed...
    //builder.Services.AddHangfire(c => c.UseSqlServerStorage(connectionString));
    //GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString).WithJobExpirationTimeout(TimeSpan.FromDays(7));

    // Add Hangfire services.
    //builder.Services.AddHangfire(configuration => configuration
    //    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    //    .UseSimpleAssemblyNameTypeSerializer()
    //    .UseRecommendedSerializerSettings()
    //    .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
    //    {
    //        CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
    //        SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
    //        QueuePollInterval = TimeSpan.Zero,
    //        UseRecommendedIsolationLevel = true,
    //        DisableGlobalLocks = true
    //    }));

    // Add the processing server as IHostedService
    //builder.Services.AddHangfireServer();
    # endregion

    var app = builder.Build();

    Log.Information("Template DDD service has started ...");

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Template DDD service v1");
        options.RoutePrefix = string.Empty;
    });

    #region Hangfire middleware pipline config.

    // add Hangfire to middleware build pipeline if using hangfire...
    
    //app.UseHangfireDashboard();

    // Add recurring Job here...
    //RecurringJob.AddOrUpdate<IMonitoringFailedHNITxnExService>(e => e.SendEmailAsync(), Cron.MinuteInterval(5));

    #endregion

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shut down completed");
    Log.CloseAndFlush();
}
