using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.StatisticsNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers;

/// <summary>
/// Контроллер получения статистики
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statistic;

    /// <inheritdoc />
    public StatisticController(IStatisticService statistic)
    {
        _statistic = statistic;
    }

    /// <summary>
    /// Получения популярных товаров
    /// </summary>
    /// <param name="count">количество</param>
    /// <param name="from">с</param>
    /// <param name="to">по</param>
    [HttpGet("topProducts")]
<<<<<<< HEAD
    public async Task<IReadOnlyCollection<PopularProduct>> TopProduct(int count, DateTime from, DateTime to)
=======
    public async Task<IReadOnlyCollection<PopularProduct>> TopProducts(int count, DateTime from, DateTime to)
>>>>>>> develop
    {
        var result = await _statistic.TopProducts(count, from, to);
        return result;
    }
}