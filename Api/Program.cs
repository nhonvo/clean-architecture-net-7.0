using Api.Core;
using Api.Presentation.Constants;
using Api.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException(ErrorMessageConstants.AppConfigurationMessage);

var databaseConnection = configuration.ConnectionStrings.DatabaseConnection;

builder.Services.AddSingleton(configuration);
var app = await builder.ConfigureServices(databaseConnection)
                        .ConfigurePipelineAsync();

app.Run();
