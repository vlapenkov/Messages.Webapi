using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Messages.Common.Exceptions;
using Messages.Infrastructure.EFCore;
using Messages.Interfaces.Interfaces.DAL;
using Messages.Logic.ProductsNS.Commands.CreateProduct;
using Messages.Logic.ProductsNS.Mappings;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
using Messages.Logic.SectionsNS.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;

namespace Messages.Webapi.Extensions
{
    public static class ErrorHandlingExtensions
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
                    //TODO: (ctx, ex) => env.IsDevelopment()
                    // options.IncludeExceptionDetails = (ctx, ex) => env.IsDevelopment();
                    options.IncludeExceptionDetails = (ctx, ex) => false;

                    options.OnBeforeWriteDetails = (ctx, details) =>
                    {
                        // Set the CorrelationId here                        
                        details.Extensions["correlationId"] = ctx.Items["X-Correlation-ID"];
                        
                    };

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

    }
}
