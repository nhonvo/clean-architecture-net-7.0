using System.Runtime.InteropServices;
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

            // cors
            services.AddCors(options =>
            {
                options.AddPolicy(name: "_myAllowSpecificOrigins",
                policy =>
                {
                    policy.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                    // policy.WithOrigins("localhost:5256", "template.com")
                    //     .AllowAnyHeader()
                    //     .AllowAnyOrigin()
                    ;
                });
            });

            // http client

            services.AddHttpClient("name_client", options =>
            {
                options.BaseAddress = new Uri("http://localhost:5256");
            });

            return services;
        }

    }
}