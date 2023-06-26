using Api.Core;
using Api.Presentation.Constants;
using Api.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException(ErrorMessageConstants.AppConfigurationMessage);

string databaseConnection = configuration.ConnectionStrings.DatabaseConnection;
string audience = configuration.Jwt.Audience;
string issuer = configuration.Jwt.Issuer;
string key = configuration.Jwt.Key;

builder.Services.AddSingleton(configuration);
var app = await builder.ConfigureServices(databaseConnection, audience, issuer, key)
                        .ConfigurePipelineAsync();

app.Run();
