using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Rk.Messages.Common.DelegatingHandlers;
using Rk.Messages.Interfaces.Services;

namespace Rk.Messages.Webapi.Extensions
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<AuthHeaderPropagationHandler>();
            services.AddTransient<CorrelationIdDelegatingHandler>();           

            services.AddRefitClient<IFileStoreService>()
              .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:FileStore:BaseUrl"]))
              .AddHttpMessageHandler<AuthHeaderPropagationHandler>()
               .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

            return services;
        }
    }
}
