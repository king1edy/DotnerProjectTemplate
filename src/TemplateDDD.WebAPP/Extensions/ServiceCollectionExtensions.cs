using Microsoft.AspNetCore.Cors.Infrastructure;

namespace TemplateDDD.APP.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Template DDD App",
                    Version = "v1",
                    Description = "Template DDD App"
                });
            });

            return services;
        }

        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services, string authUrl, string audience)
        {
            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.Authority = authUrl;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidIssuer = authUrl,
            //            ValidateAudience = true,
            //            ValidAudience = audience,
            //            ValidateLifetime = true
            //        };
            //    });

            return services;
        }

        public static CorsOptions ConfigureCorsPolicy(this CorsOptions corsOptions, string[] allowedOrigins)
        {
            if (allowedOrigins.Any())
            {
                corsOptions.AddPolicy("CORSAllowedOrigins",
                                  corsBuilder => corsBuilder
                                  .WithOrigins(allowedOrigins.ToArray())
                                  .AllowAnyHeader()
                                  .AllowAnyMethod()
                                 );
            }
            else
            {
                corsOptions.AddPolicy("CORSAllowedOrigins",
                              corsBuilder => corsBuilder
                                                .AllowAnyHeader()
                                                .AllowAnyMethod());
            }

            return corsOptions;
        }
    }
}