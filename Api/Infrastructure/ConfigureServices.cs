using Api.ApplicationLogic.Repositories;
using Api.Core;
using Api.Infrastructure.Interface;
using Api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Api.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructuresService(
            this IServiceCollection services,
            AppConfiguration configuration)
        {
            string databaseConnection = configuration.ConnectionStrings.DatabaseConnectionDocker;
            bool UseInMemoryDatabase = configuration.UseInMemoryDatabase;
            if (UseInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("Template"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(databaseConnection)
            );
            }
            services.AddScoped<ApplicationDbContextInitializer>();

            // register services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }


    }
}