using Api.Core;
using Api.Presentation.Extensions;
using NLog;
using NLog.Common;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration.Get<AppConfiguration>()
                    ?? throw new ArgumentNullException("AppConfiguration cannot be null");

var databaseConnection = configuration.ConnectionStrings.DatabaseConnection;

builder.Services.AddSingleton(configuration);
var app = await builder.ConfigureServices(databaseConnection)
                        .ConfigurePipelineAsync();
var serviceCollection = new ServiceCollection();

var serviceProvider = ContainerConfiguration.Configure();

var logger = serviceProvider.GetService<ILogger<Program>>();
logger.LogInformation($"Test splunk log {DateTime.Now}");

app.Run();

internal static class ContainerConfiguration
{
    public static IServiceProvider Configure()
    {
        var serviceCollection = new ServiceCollection();

        LogManager.LoadConfiguration($"log.config");
        InternalLogger.LogToConsole = true;
        InternalLogger.LogLevel = NLog.LogLevel.Trace;

        serviceCollection.AddLogging(l =>
        {
            l.AddNLog();
        }).Configure<LoggerFilterOptions>(c => c.MinLevel = Microsoft.Extensions.Logging.LogLevel.Trace);

        return serviceCollection.BuildServiceProvider();
    }
}

// internal static class ContainerConfiguration
// {
//     public static IServiceProvider Configure()
//     {
//         var serviceCollection = new ServiceCollection();

//         LogManager.LoadConfiguration(@"./nlog.xml");

//         serviceCollection.AddLogging(l =>
//         {
//             l.AddNLog();
//         }).Configure<LoggerFilterOptions>(c => c.MinLevel = Microsoft.Extensions.Logging.LogLevel.Trace);

//         // Add Splunk target to NLog configuration
//         var splunkTarget = new SplunkHttpEventCollectorTarget
//         {
//             Uri = "http://your-splunk-instance:8088/services/collector",
//             Token = "your-splunk-token",
//             BatchSize = 100,
//             BatchInterval = TimeSpan.FromSeconds(2),
//             IncludeEventProperties = true
//         };

//         LogManager.Configuration.AddTarget("splunk", splunkTarget);
//         LogManager.Configuration.AddRuleForAllLevels("splunk");

//         LogManager.ReconfigExistingLoggers();

//         return serviceCollection.BuildServiceProvider();
//     }
// }