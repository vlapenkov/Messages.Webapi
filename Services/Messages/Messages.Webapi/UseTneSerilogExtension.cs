using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Messages.Webapi.Extensions
{
    public static partial class SerilogExtensions
    {

        /// <summary>
        /// Подключение Serilog с преднастроенными параметрами для логирвания.
        /// В качестве URL сервиса ElasticSearch - используется параметр 'Services:Elasticsearch:BaseUri' из appsettings.json
        /// В качестве applicationName - используется EntryAssemblyName[1] Имя DLL должно быть формата TNE.ServiceName.Other
        /// </summary>
        /// <param name="hostBuilder">IWebHostBuilder</param>
        /// <returns>IWebHostBuilder</returns>
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
