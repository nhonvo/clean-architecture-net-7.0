using Api.ApplicationLogic.Mapper;
using Api.Presentation.Filters;
using Api.Presentation.Middlewares;
using System.Diagnostics;

namespace Api.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services)
        {

            services.AddAutoMapper(typeof(MapProfile));

            // Middleware
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddHealthChecks();

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            // swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            return services;
        }

    }
}