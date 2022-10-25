using Microsoft.Extensions.Hosting;
using Serilog;

namespace Rk.Messages.Common.Extensions
{
    /// <summary>
    /// Подключение Serilog
    /// </summary>
    public static class SerilogRkExtensions
    {
        public static IHostBuilder UseTneSerilog(this IHostBuilder hostBuilder)
        {

            hostBuilder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                 .ReadFrom.Configuration(hostingContext.Configuration) // на 10.01.2020 берем только MinimumLevel, остальное определяем в коде. 
                 .Enrich.FromLogContext()
                 .Enrich.WithMachineName()
            );

            return hostBuilder;
        }
    }
}
