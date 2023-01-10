
using Microsoft.Extensions.DependencyInjection;

namespace Rk.AccountService.WebApi.Extensions;

public static class RegisterServicesExtensions
{

    /// <summary>
    /// Загруза внутренних зависимостей
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {

        return services;
    }
}