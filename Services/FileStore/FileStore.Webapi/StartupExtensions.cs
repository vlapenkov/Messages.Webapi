using FileStore.Infrastructure.EFCore;
using FileStore.Interfaces;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Messages.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FileStore.Webapi
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Загруза внутренних зависимостей
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            
         //   services.AddAutoMapper(typeof(ProductsMappingProfile).GetTypeInfo().Assembly);
        }
        public static void AddErrorHandling(this IServiceCollection services, IHostEnvironment env)
        {

            services.AddProblemDetails(options =>
            {
                //TODO: (ctx, ex) => env.IsDevelopment()
                // options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
                options.IncludeExceptionDetails = (ctx, ex) => false;

                options.Map<ValidationException>(

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
