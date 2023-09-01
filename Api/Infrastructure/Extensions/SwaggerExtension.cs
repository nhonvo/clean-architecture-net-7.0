using Microsoft.OpenApi.Models;

namespace Api.Infrastructure.Extensions
{
    /// <summary>
    /// Swagger document extension
    /// </summary>
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Template",
                    Version = "v1",
                    Description = "API template project",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://google.com")
                    }
                });

                // Add JWT authentication support in Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
            {
                securityScheme, new[] { "Bearer" }
            }
                };

                options.AddSecurityRequirement(securityRequirement);
            });
            return services;
        }
    }
}