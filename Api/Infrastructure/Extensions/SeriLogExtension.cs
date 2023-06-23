using Microsoft.AspNetCore.Builder;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using System.Globalization;

namespace Api.Infrastructure.Extensions
{
    public static class SeriLogExtension
    {
        public static void AddSerilog(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                        .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
                        .Enrich.FromLogContext()
                        .WriteTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {CorrelationId} {SourceContext} {Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}",
                                        restrictedToMinimumLevel: LogEventLevel.Information,
                                        formatProvider: CultureInfo.InvariantCulture,
                                        standardErrorFromLevel: LogEventLevel.Error,
                                        theme: AnsiConsoleTheme.Literate)
                        .WriteTo.File(
                            "../../../Loggings/logs.log",
                            rollingInterval: RollingInterval.Day,
                            outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {CorrelationId} {SourceContext} {Level:u3}] {Message:lj}{NewLine}{Exception}{NewLine}")
                        .CreateLogger();

            builder.Host.UseSerilog(Log.Logger);
        }
    }
}