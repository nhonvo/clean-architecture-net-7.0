using Api.ApplicationLogic;
using Api.Infrastructure;

namespace Api.Presentation.Extensions
{
    public static class HostingExtensions
    {
        public static WebApplication ConfigureServices(
            this WebApplicationBuilder builder,
            string databaseConnection)
        {
            builder.Services.AddInfrastructuresService(databaseConnection);
            builder.Services.AddApplicationService();
            builder.Services.AddWebAPIService();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.MapHealthChecks("/hc");

            app.UseAuthorization();

            app.MapControllers();
            return app;
        }
    }
}