using Polly;

namespace Api.Infrastructure.Extensions
{
    /// <summary>
    /// Http client extension include: sample uri and retry process.
    /// </summary>
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClientCustom(this IServiceCollection services)
        {
            services.AddHttpClient("Sample", options =>
            {
                options.BaseAddress = new Uri("Sample_URL");
            })
            .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(new[]{
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            }));
            return services;
        }
    }
}