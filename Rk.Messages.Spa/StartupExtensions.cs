using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Rk.Messages.Spa.DelegatingHandlers;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Обработка ошибок рефит на фронте
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env)
        {

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (ctx, ex) => !env.IsDevelopment();

                options.Map<ValidationApiException>(
                                   delegate (ValidationApiException exception)
                                   {
                                       return new ValidationProblemDetails(exception.Content.Errors)
                                       {

                                           Title = exception.Content.Title,
                                           Detail = exception.Content.Detail,
                                           Instance = exception.Content.Instance,
                                           Status = exception.Content.Status,
                                           Type = exception.Content.Type
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

            services.AddRefitClient<IMessagesServices>()
                .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
                .AddHttpMessageHandler<AuthHeaderPropagationHandler>();

            services.AddRefitClient<ISectionsServices>()
                .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
                .AddHttpMessageHandler<AuthHeaderPropagationHandler>();

            services.AddRefitClient<IProductsService>()
               .ConfigureHttpClient(c => c.BaseAddress = new System.Uri(config["Services:Messages:BaseUrl"]))
               .AddHttpMessageHandler<AuthHeaderPropagationHandler>();

            return services;
        }


    }
}
