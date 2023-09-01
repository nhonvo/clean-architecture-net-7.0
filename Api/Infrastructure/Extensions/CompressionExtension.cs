using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

namespace Api.Infrastructure.Extensions
{
    public static class CompressionExtension
    {
        public static IServiceCollection AddCompressionCustom(this IServiceCollection services)
        {
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