using MediatR;

namespace RK.Statistic.Logic.Queries.PopularProduct;

/// <inheritdoc />
public class PopularProductQuery : IRequest<IReadOnlyCollection<Dto.PopularProduct>>
{
    /// <summary>
    /// Сколько товаров взять
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Начала периода
    /// </summary>
    public DateTime From { get; set; }
    
    /// <summary>
    /// Конец периода
    /// </summary>
    public DateTime To { get; set; }
}