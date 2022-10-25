using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace Messages.Common.Middlewares
{
    /// <summary>
    /// Middleware для логирования пользователя
    /// </summary>
    public class LogUserNameMiddleware
    {
        private readonly RequestDelegate next;

        public LogUserNameMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("UserName", context.User.Identity.Name ?? "Anonymous"))
            {
                await next(context);
            }
        }
    }
}
