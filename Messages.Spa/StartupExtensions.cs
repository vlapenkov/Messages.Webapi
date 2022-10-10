using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
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
                public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env) {

                        services.AddProblemDetails( options =>
                        {
                                options.IncludeExceptionDetails = (ctx, ex) => !env.IsDevelopment( );

                                options.Map<ValidationApiException>(
                                       delegate (ValidationApiException exception)
                                       {
                                               return new ValidationProblemDetails( exception.Content.Errors )
                                               {

                                                       Title = exception.Content.Title,
                                                       Detail = exception.Content.Detail,
                                                       Instance = exception.Content.Instance,
                                                       Status = exception.Content.Status,
                                                       Type = exception.Content.Type
                                               };
                                       }
                                );
                        } );
                }

        }
}
