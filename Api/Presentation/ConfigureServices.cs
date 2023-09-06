using Api.Core;
using Api.Infrastructure.Extensions;
using Api.Presentation.Filters;
using Api.Presentation.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Diagnostics;
using System.Reflection;

namespace Api.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIService(
            this IServiceCollection services,
            AppConfiguration configuration)
        {
            services.AddControllers(options =>
           {
               options.Filters.Add<ApiExceptionFilterAttribute>();
           });
            services.AddEndpointsApiExplorer();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Middleware
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddSingleton<LoggingMiddleware>();

            // Extension classes
            services.AddHealthChecks();
            services.AddCompressionCustom();
            services.AddCorsCustom();
            services.AddHttpClient();
            services.AddRateLimit();
            services.AddSwaggerCustom();
            services.AddJWTCustom(configuration);

            return services;
        }

    }
}