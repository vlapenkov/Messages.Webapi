using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Common.Exceptions;

namespace RK.Statistic.Webapi.Extensions
{
    public static class ErrorHandlingExtensions
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
                    //TODO: (ctx, ex) => env.IsDevelopment()
                    // options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
                    options.IncludeExceptionDetails = (ctx, ex) => false;

                    options.Map(

                           delegate (ValidationException exception)
                           {
                               var result = new ValidationProblemDetails(exception.Errors.GroupBy(x => x.PropertyName).ToDictionary(
                                x => x.Key, x => x.Select(x => x.ErrorMessage).ToArray()))
                               {
                                   Title = "Ошибка",
                                   Type = nameof(ValidationException),
                                   Status = StatusCodes.Status400BadRequest
                               };

                               return result;
                           }
                    );

                    options.Map<RkUnauthorizedAccessException>(exception => new ProblemDetails
                    {
                        Type = nameof(RkUnauthorizedAccessException),
                        Title = "Ошибка aвторизации",
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
