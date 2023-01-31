using MediatR;
using RK.Statistic.Interfaces;

namespace RK.Statistic.Logic.Queries.PopularProduct;

/// <inheritdoc />
public class PopularProductQueryHandler : IRequestHandler<PopularProductQuery, IReadOnlyCollection<Dto.PopularProduct>>
{
    private readonly IClickHouseConnectionFactory _factory;

    /// <summary>
    /// </summary>
    public PopularProductQueryHandler(IClickHouseConnectionFactory factory)
    {
        _factory = factory;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Dto.PopularProduct>> Handle(PopularProductQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await _factory.GetConnectionAsync();
        var cmd = connection.CreateCommand(@$"
SELECT 
    production, 
    categoryId, 
    producer, 
    count(*) count 
FROM 
    {Tables.ProductReadTable} 
WHERE 
    created BETWEEN @from AND @to 
GROUP BY 
    productionId, production, categoryId, producer 
ORDER BY 
    count 
LIMIT 
    {request.Count} BY count");
        cmd.Parameters.AddWithValue("from", request.From);
        cmd.Parameters.AddWithValue("to", request.To);
        await using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        var result = new List<Dto.PopularProduct>();
        while(await reader.ReadAsync(cancellationToken))
        {
            var production = reader.GetString(0);
            var categoryId = reader.GetInt64(1);
            var producer = reader.GetString(2);
            var count = (long)reader.GetUInt64(3);
            result.Add(new Dto.PopularProduct(production, categoryId, producer, count));
        }
        return result;
    }
}