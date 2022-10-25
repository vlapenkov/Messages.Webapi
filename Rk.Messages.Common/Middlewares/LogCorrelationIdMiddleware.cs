using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Messages.Common.Middlewares
{
    /// <summary>
    /// Middleware для логирования CorrelationId в формате Serilog
    /// </summary>
    public class LogCorrelationIdMiddleware
    {
        private readonly RequestDelegate next;

        public LogCorrelationIdMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("CorrelationId", context.Items["X-Correlation-ID"]))
            {
                await next(context);
            }
        }
    }
}
