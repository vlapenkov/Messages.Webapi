using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Infrastructure.EFCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using Rk.Messages.Logic.ProductsNS.Mappings;
using Rk.Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Rk.Messages.Logic.SectionsNS.Validations;

namespace Rk.Messages.Webapi.Extensions
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

        /// <summary>
        /// Загруза внутренних зависимостей
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencies(this IServiceCollection services)
        {           

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

            services.AddScoped<IValidator<CreateSectionCommand>, CreateSectionValidator>();
            services.AddScoped<IValidator<CreateProductCommand>, CreateProductValidator>();

           

            services.AddMediatR(typeof(CreateSectionCommand).GetTypeInfo().Assembly);
            services.AddAutoMapper(typeof(ProductsMappingProfile).GetTypeInfo().Assembly);
        }

    }
}
