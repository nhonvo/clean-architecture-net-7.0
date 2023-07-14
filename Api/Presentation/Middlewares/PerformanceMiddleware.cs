using System.Diagnostics;
using Serilog;

namespace Api.Presentation.Middlewares
{
    public class PerformanceMiddleware : IMiddleware
    {
        private readonly Stopwatch _stopwatch;

        public PerformanceMiddleware(Stopwatch stopwatch)
        {
            _stopwatch = stopwatch;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Restart();
            _stopwatch.Start();
            Console.WriteLine("Start performance record");
            await next(context);
            Console.WriteLine("End performance record");
            _stopwatch.Stop();
            TimeSpan timeTaken = _stopwatch.Elapsed;

            Log.Information("Start performance record \n Time taken: " + timeTaken.ToString(@"m\:ss\.fff") + "\n End performance record");
            Console.WriteLine("Start performance record \n Time taken: " + timeTaken.ToString(@"m\:ss\.fff") + "\n End performance record");

        }
    }
}
