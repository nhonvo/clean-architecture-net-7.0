using Api.ApplicationLogic.Services;
using Api.Infrastructure.IService;
namespace Api.ApplicationLogic
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IUserReadService, UserReadService>();
            services.AddScoped<IUserWriteService, UserWriteService>();
            services.AddScoped<IBookReadService, BookReadService>();
            services.AddScoped<IBookWriteService, BookWriteService>();

            return services;
        }
    }
}