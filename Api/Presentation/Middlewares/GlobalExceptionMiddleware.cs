using Serilog;

namespace Api.Presentation.Middlewares
{
    public class GlobalExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GlobalExceptionMiddleware");
                Console.WriteLine(ex.Message);

                Log.Information("GlobalExceptionMiddleware");
                Log.Error(ex.Message);
                await context.Response.WriteAsync(ex.ToString());
            }
        }
    }
}
