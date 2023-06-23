using Api.ApplicationLogic.Mapper;
using Api.Presentation.Filters;
using Api.Presentation.Middlewares;
using Microsoft.AspNetCore.ResponseCompression;
using System.Diagnostics;
using System.IO.Compression;

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

            // Compression
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "application/json",
                    "application/xml",
                    "text/plain",
                    "image/png",
                    "image/jpeg"
                });
                options.Providers.Add<BrotliCompressionProvider>();
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.SmallestSize;
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            return services;
        }

    }
}