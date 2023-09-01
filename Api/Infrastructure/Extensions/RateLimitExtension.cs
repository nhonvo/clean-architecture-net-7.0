using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Infrastructure.Extensions
{
    public static class RateLimitExtension
    {
        /// <summary>
        /// PermitLimit: 10 request allowed per time window.
        /// Window: Time window of 1 second.
        /// QueueProcessingOrder: Oldest requests will be processed first.
        /// QueueLimit: Up to 2 requests can wait in the queue if the limit is reached
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRateLimit(this IServiceCollection services)
        {
            services.AddRateLimiter(p => p.AddFixedWindowLimiter(policyName: "rate", o =>
            {
                o.PermitLimit = 10;
                o.Window = TimeSpan.FromSeconds(1);
                o.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                o.QueueLimit = 5;
            }));
            return services;
        }

    }

}