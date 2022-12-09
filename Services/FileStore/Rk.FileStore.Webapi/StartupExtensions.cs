using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Mvc;
using Rk.FileStore.Infrastructure.EFCore;
using Rk.FileStore.Infrastructure.Services;
using Rk.FileStore.Interfaces.Interfaces;
using Rk.FileStore.Interfaces.Services;
using Rk.FileStore.Logic.Services;
using Rk.Messages.Common.Exceptions;
using System.Collections;

namespace Rk.FileStore.Webapi
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

            services.AddScoped<IHashProvider, Sha256HashProvider>();

            services.AddTransient<IFilesService, FilesService>();

            services.AddTransient<IProductService, ProductService>();

            services.AddTransient<IRemoteImageService, RemoteImageService>();
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
