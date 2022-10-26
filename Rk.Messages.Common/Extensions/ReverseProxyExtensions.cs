﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;

namespace Rk.Messages.Common.Extensions
{
    public static class ReverseProxyExtensions
    {
        /// <summary>
        /// Добавить реверсивный прокси
        /// </summary>
        /// <param name="app">приложение</param>
        /// <param name="config">конфигурация</param>
        /// <returns></returns>
        public static WebApplication UseReverseProxy(this WebApplication app, IConfiguration config)
        {
            _ = bool.TryParse(config["USE_REVERSE_PROXY"], out var behindReverseProxy);
            if (!behindReverseProxy) return app;

            var forwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };
            forwardedHeaderOptions.KnownNetworks.Clear();
            forwardedHeaderOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardedHeaderOptions);
            var subDirPath = config["SUBDIR"];

            if (!string.IsNullOrWhiteSpace(subDirPath)) app.UsePathBase(new PathString(subDirPath));

            return app;
        }
    }


}
