using System;
using System.Linq;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rk.Messages.Common.Exceptions;

namespace Rk.AccountService.WebApi.Extensions
{
    /// <summary>
    /// Расширения для подключения обработки ошибок
    /// </summary>
    public static class ErrorHandlingExtensions
    {
       
        /// <summary>
        /// Обработка ошибок refit на фронте
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env)
        {

            services.AddProblemDetails(options =>
                {
                    //TODO: (ctx, ex) => env.IsDevelopment()
                    // options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
                    options.IncludeExceptionDetails = (_, _) => false;

                    options.Map(

                           delegate (ValidationException exception)
                           {
                               var result = new ValidationProblemDetails(exception.Errors.GroupBy(x => x.PropertyName).ToDictionary(
                                x => x.Key, x => x.Select(z => z.ErrorMessage).ToArray()))
                               {
                                   Title = "Ошибка",
                                   Type = nameof(ValidationException),
                                   Status = StatusCodes.Status400BadRequest
                               };

                               return result;
                           }
                    );

                    options.Map<EntityNotFoundException>(exception => new ProblemDetails
                    {
                        Type = nameof(EntityNotFoundException),
                        Title = "Объект не найден",
                        Detail = exception.Message,
                        Status = StatusCodes.Status404NotFound
                    });

                    options.Map<RkUnauthorizedAccessException>(exception => new ProblemDetails
                    {
                        Type = nameof(RkUnauthorizedAccessException),
                        Title = "Ошибка авторизации",
                        Detail = exception.Message,
                        Status = StatusCodes.Status401Unauthorized
                    });


                    options.Map<RkErrorException>(exception => new ProblemDetails
                    {
                        Type = nameof(RkErrorException),
                        Title = "Ошибка логики",
                        Detail = exception.Message,
                        Status = StatusCodes.Status500InternalServerError
                    });

                    options.Map<Exception>(exception => new ProblemDetails
                    {
                        Type = nameof(Exception),
                        Title = "Ошибка приложения",
                        Detail = exception.Message,
                        Status = StatusCodes.Status500InternalServerError
                    });


                }

                );
        }


    }
}
