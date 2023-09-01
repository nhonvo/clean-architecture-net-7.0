using Api.Core;
using Api.Core.Entities;
using Api.Infrastructure.Persistence;
using Api.Presentation.Filters;
using Api.Presentation.Middlewares;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Polly;
using System.Diagnostics;
using System.IO.Compression;
using System.Reflection;
using System.Text;

namespace Api.Presentation
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebAPIService(
            this IServiceCollection services,
            AppConfiguration configuration)
        {
            string audience = configuration.Jwt.Audience;
            string issuer = configuration.Jwt.Issuer;
            string key = configuration.Jwt.Key;
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            // Middleware
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddSingleton<LoggingMiddleware>();
            services.AddHealthChecks();

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            // swagger
            services.AddEndpointsApiExplorer();
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

            // cors
            // services.AddCors(options =>
            // {
            //     options.AddPolicy(name: "_myAllowSpecificOrigins",
            //     policy =>
            //     {
            //         policy.AllowAnyHeader()
            //             .AllowAnyMethod()
            //             .AllowAnyOrigin();
            //         // policy.WithOrigins("localhost:5256", "template.com")
            //         //     .AllowAnyHeader()
            //         //     .AllowAnyOrigin()
            //         ;
            //     });
            // });

            // http client
            services.AddHttpClient("Azure_Translate", options =>
            {
                options.BaseAddress = new Uri("");
            });
            // .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]{
            //     TimeSpan.FromSeconds(1),
            //     TimeSpan.FromSeconds(2),
            //     TimeSpan.FromSeconds(3)
            // }));

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
            // jwt
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });
            return services;
        }

    }
}