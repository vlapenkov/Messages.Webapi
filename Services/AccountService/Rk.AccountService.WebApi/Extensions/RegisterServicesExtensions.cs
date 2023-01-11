
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rk.AccountService.Logic.UserNS.Commands.CreateUser;
using Rk.AccountService.Logic.UserNS.Validations;
using Rk.Messages.Common.DelegatingHandlers;

namespace Rk.AccountService.WebApi.Extensions;

public static class RegisterServicesExtensions
{

    /// <summary>
    /// Загрузка внутренних зависимостей
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddTransient<AuthHeaderPropagationHandler>();
        services.AddTransient<CorrelationIdDelegatingHandler>();
        services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);
        services.AddTransient<IValidator<CreateUserCommand>, CreateUserValidator>();
        return services;
    }
}