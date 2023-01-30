using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.AccountNS;
using Rk.Messages.Spa.Infrastructure.Dto.StatisticsNS;

namespace Rk.Messages.Spa.Infrastructure.Services;

/// <summary>
/// Сервис получения статистики
/// </summary>
public interface IStatisticService
{
    /// <summary>
    /// Метод получения популятрных(часто просматриваемых) товаров
    /// </summary>
    /// <param name="count">колиество</param>
    /// <param name="from">с</param>
    /// <param name="to">по</param>
    /// <returns></returns>
    [Get("/api/v1/statistics/products/popular")]
    Task<IReadOnlyCollection<PopularProduct>> TopProducts([Query] int count, [Query] DateTime from, [Query] DateTime to);
}