using Api.Core;
using Api.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException("AppConfiguration cannot be null");

var databaseConnection = configuration.ConnectionStrings.DatabaseConnectionDocker;

builder.Services.AddSingleton(configuration);
var app = await builder.ConfigureServices(databaseConnection)
                        .ConfigurePipelineAsync();
app.Run();
