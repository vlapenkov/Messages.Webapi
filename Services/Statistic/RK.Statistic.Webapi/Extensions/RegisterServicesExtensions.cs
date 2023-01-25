using System.Reflection;
using MediatR;
using RK.Statistic.Infrastructure.ClickHouse;
using RK.Statistic.Interfaces;
using RK.Statistic.Interfaces.StatisticWriters;
using RK.Statistic.Logic.Consumers;
using RK.Statistic.Logic.Queries.PopularProduct;
using RK.Statistic.Logic.StatisticWriters;

namespace RK.Statistic.Webapi.Extensions;

public static class RegisterServicesExtensions
{
    // <summary>
    /// Загруза внутренних зависимостей
    /// </summary>
    /// <param name="services"></param>
    public static void AddDependencies(this IServiceCollection services)
    {
        services.AddMediatR(typeof(PopularProductQuery).GetTypeInfo().Assembly);
        services.AddSingleton<IClickHouseConnectionFactory, ClickHouseConnectionFactory>();
        services.AddTransient<IProductViewStatisticWriter, ProductViewStatisticWriter>();
        services.AddHostedService<ProductReadEventConsumer>();
    }
}