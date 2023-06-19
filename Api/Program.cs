using Api.Core;
using Api.Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>();
var databaseConnection = configuration.ConnectionStrings.DatabaseConnection;
// System.Console.WriteLine("Herere!!!!" + databaseConnection);
builder.Services.AddSingleton(configuration);
var app = builder.ConfigureServices(databaseConnection)
    .ConfigurePipeline();
app.Run();
