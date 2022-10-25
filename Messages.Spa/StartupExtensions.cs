using Hellang.Middleware.ProblemDetails;
using Messages.Common.DelegatingHandlers;
using Messages.Spa.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Messages.Spa
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Обработка ошибок рефит на фронте
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env, global::Serilog.ILogger logger)
        {

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => !env.IsDevelopment();

                options.OnBeforeWriteDetails = (ctx, details) =>
                {
                    // Set the CorrelationId here                   
                    details.Extensions["correlationId"] = ctx.Items["X-Correlation-ID"];                    
                    //logger.Error(details.Detail);
                    //logger.Error(details.Extensions.FirstOrDefault().Value.ToString());
                };

                options.Map<ValidationApiException>(
                                   delegate (ValidationApiException exception)
                                   {
                                       return new ValidationProblemDetails(exception.Content.Errors)
                                       {

                                           Title = exception.Content.Title,
                                           Detail = exception.Content.Detail,
                                           Instance = exception.Content.Instance,
                                           Status = exception.Content.Status,
                                           Type = exception.Content.Type,
                                           
                                       };
                                   }
                            );
            });
        }

        /// <summary>
        /// Регистрация обработчиков и клиентов Http
        /// </summary>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<AuthHeaderPropagationHandler>();
            services.AddTransient<CorrelationIdDelegatingHandler>();

            services.AddRefitClient<IMessagesServices>()
                .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
                .AddHttpMessageHandler<AuthHeaderPropagationHandler>()
                .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

            services.AddRefitClient<ISectionsServices>()
                .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
                .AddHttpMessageHandler<AuthHeaderPropagationHandler>()
                 .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

            services.AddRefitClient<IProductsService>()
               .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
               .AddHttpMessageHandler<AuthHeaderPropagationHandler>()
                .AddHttpMessageHandler<CorrelationIdDelegatingHandler>();

            return services;
        }


    }
}
