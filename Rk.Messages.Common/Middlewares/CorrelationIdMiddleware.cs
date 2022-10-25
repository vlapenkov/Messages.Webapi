using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Rk.Messages.Common.Options;

namespace Rk.Messages.Common.Middlewares
{
    /// <summary>
    /// Middleware для сохрания correlationId в  context.Items
    /// </summary>
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CorrelationIdOptions _options;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(
            RequestDelegate next,
            IOptions<CorrelationIdOptions> options,
            ILogger<CorrelationIdMiddleware> logger
            )
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            _next = next ?? throw new ArgumentNullException(nameof(next));

            _options = options.Value;
            _logger = logger;
        }

        public Task Invoke(HttpContext context)
        {

            string corId = Guid.NewGuid().ToString();
            if (context.Request.Headers.TryGetValue(_options.Header, out StringValues correlationId))
            {

                corId = correlationId.ToString();
            }

            if (!context.Items.ContainsKey(_options.Header))
                context.Items.Add(_options.Header, corId);
            

            if (_options.IncludeInResponse)
            {
                // apply the correlation ID to the response header for client side tracking
                context.Response.OnStarting(() =>
                {
                    context.Response.Headers.Add(_options.Header, new[] { corId });
                    return Task.CompletedTask;
                });
            }


            return _next(context);
        }
    }
}
