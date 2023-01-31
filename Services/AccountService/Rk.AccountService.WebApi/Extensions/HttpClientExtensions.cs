using System;
using System.Linq;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rk.AccountService.Infrastructure.HttpClients;
using Rk.AccountService.Interfaces.HttpClients;

namespace Rk.AccountService.WebApi.Extensions
{
    /// <summary>
    /// Расширение для регистрации Http клиентов
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Регистрация Http клиентов
        /// </summary>
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpClient<IKeycloakHttpClient, KeycloakHttpClient>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(config.GetBaseAddress());
                httpClient.Timeout = new TimeSpan(0, 1, 30);
                httpClient.DefaultRequestHeaders.Connection.Add("keep-alive");
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            });
            return services;
        }

        private static string GetBaseAddress(this IConfiguration configuration)
        {
            var url = configuration.GetValue<string>("Oidc:BaseUrl") ?? "";
            if (url.LastOrDefault() != '/') url += '/';
            return url;
        }
    }
}
