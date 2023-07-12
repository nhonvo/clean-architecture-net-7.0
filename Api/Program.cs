using Api.Core;
using Api.Presentation.Constants;
using Api.Presentation.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException(ErrorMessageConstants.AppConfigurationMessage);

builder.Services.AddSingleton(configuration);
var app = await builder.ConfigureServices(configuration)
                        .ConfigurePipelineAsync(configuration);

NewRelic.Api.Agent.NewRelic.StartAgent();

app.Run();
