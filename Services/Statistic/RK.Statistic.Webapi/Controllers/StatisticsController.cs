using MediatR;
using Microsoft.AspNetCore.Mvc;
using RK.Statistic.Logic.Queries.Dto;
using RK.Statistic.Logic.Queries.PopularProduct;

namespace RK.Statistic.Webapi.Controllers;

/// <summary>
/// Получение статистики
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Популярные продукты
    /// </summary>
    /// <param name="count">количество</param>
    /// <param name="from">с</param>
    /// <param name="to">по</param>
    [HttpGet("products/popular")]
    public async Task<IReadOnlyCollection<PopularProduct>> TopProduct(int count, DateTime from, DateTime to)
    {
        var result = await _mediator.Send(new PopularProductQuery
        {
            Count = count,
            From = from,
            To = to
        });

        return result;
    }
}