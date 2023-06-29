using Serilog.Context;

namespace Api.Presentation.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await next(context);
        }

        private static IDisposable AddParameter(string key, string value, bool addToNewRelic = false)
        {
            if (addToNewRelic)
            {
                NewRelic.Api.Agent.NewRelic.GetAgent().CurrentTransaction.AddCustomAttribute(key, value);
            }

            return LogContext.PushProperty(key, value);
        }
    }
}
