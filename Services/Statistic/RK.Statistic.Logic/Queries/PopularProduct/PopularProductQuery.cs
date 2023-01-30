using MediatR;

namespace RK.Statistic.Logic.Queries.PopularProduct;

public class PopularProductQuery : IRequest<IReadOnlyCollection<Dto.PopularProduct>>
{
    public int Count { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}