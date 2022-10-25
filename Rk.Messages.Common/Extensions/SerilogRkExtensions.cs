using Microsoft.AspNetCore.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Messages.Common.Extensions
{
    /// <summary>
    /// Подключение Serilog
    /// </summary>
    public static class SerilogRkExtensions
    {
        public static IWebHostBuilder UseTneSerilog(this IWebHostBuilder hostBuilder)
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
