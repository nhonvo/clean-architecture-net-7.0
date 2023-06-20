using Api.ApplicationLogic.Repositories;
using Api.ApplicationLogic.Services;
using Api.Infrastructure.Interface;
using Api.Infrastructure.IService;
using Microsoft.EntityFrameworkCore;
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
            // System.Console.WriteLine("1Herere!!!!" + databaseConnection);

            // register services
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ApplicationDbContextInitializer>();

            return services;

        }


    }
}