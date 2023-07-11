using Api.ApplicationLogic.Repositories;
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
            string databaseConnection)
        {
            // Add services to the container.
            services.AddDbContext<ApplicationDbContext>(o =>
                o.UseNpgsql(databaseConnection)
            );
            services.AddScoped<ApplicationDbContextInitializer>();

            // register services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;

        }


    }
}