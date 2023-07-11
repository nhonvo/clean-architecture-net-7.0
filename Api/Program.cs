using Api.Core;
using Api.Presentation.Constants;
using Api.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var _configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException(ErrorMessageConstants.AppConfigurationMessage);

string databaseConnection = _configuration.ConnectionStrings.DatabaseConnectionDocker;
string audience = _configuration.Jwt.Audience;
string issuer = _configuration.Jwt.Issuer;
string key = _configuration.Jwt.Key;

Log.Warning("test");

builder.Services.AddSingleton(_configuration);
var app = await builder.ConfigureServices(databaseConnection, audience, issuer, key)
                        .ConfigurePipelineAsync();

NewRelic.Api.Agent.NewRelic.StartAgent();

app.Run();
