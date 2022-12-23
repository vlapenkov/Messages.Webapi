using Rk.AccountService.Infrastructure.HttpClients;
using Rk.AccountService.Interfaces.HttpClients;
using Rk.Messages.Common.DelegatingHandlers;

namespace Rk.AccountService.WebApi.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<AuthHeaderPropagationHandler>();
            services.AddTransient<CorrelationIdDelegatingHandler>();
            services.AddHttpClient<IKeycloakHttpClient, KeycloakHttpClient>(c => BaseSettingsHttpClient(config, c));
            return services;
        }
        
        private static void BaseSettingsHttpClient(IConfiguration config, HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(config.GetBaseAddress());
            httpClient.Timeout = new TimeSpan(0, 1, 30);
        }
        
        private static string GetBaseAddress(this IConfiguration? configuration)
        {
            var url = configuration.GetValue<string>("Oidc:BaseUrl") ?? "";
            if (url.LastOrDefault() != '/') url += '/';
            return url;
        }
    }
}
