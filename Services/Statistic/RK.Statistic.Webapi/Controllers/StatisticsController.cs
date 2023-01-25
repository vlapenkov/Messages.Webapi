using MediatR;
using Microsoft.AspNetCore.Mvc;
using RK.Statistic.Logic.Queries.Dto;
using RK.Statistic.Logic.Queries.PopularProduct;

namespace RK.Statistic.Webapi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StatisticsController(IMediator mediator)
    {
        _mediator = mediator;
    }

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