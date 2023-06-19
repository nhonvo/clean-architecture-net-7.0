using Api.Core;
using Api.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
var databaseConnection = configuration.ConnectionStrings.DatabaseConnection;

builder.Services.AddSingleton(configuration);

var app = builder.ConfigureServices(databaseConnection)
    .ConfigurePipeline();
app.Run();
