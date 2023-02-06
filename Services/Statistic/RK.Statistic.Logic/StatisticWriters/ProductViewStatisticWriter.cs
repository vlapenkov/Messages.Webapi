using Microsoft.Extensions.Logging;
using RK.Messages.Shared;
using RK.Statistic.Interfaces;
using RK.Statistic.Interfaces.StatisticWriters;

namespace RK.Statistic.Logic.StatisticWriters;

/// <inheritdoc />
public class ProductViewStatisticWriter : IProductViewStatisticWriter
{
    private readonly ILogger<ProductViewStatisticWriter> _logger;
    private readonly IClickHouseConnectionFactory _factory;

    /// <summary>
    /// </summary>
    public ProductViewStatisticWriter(ILogger<ProductViewStatisticWriter> logger, IClickHouseConnectionFactory factory)
    {
        _logger = logger;
        _factory = factory;
    }

    /// <inheritdoc />
    public async Task InsertRowAsync(ProductViewStatisticEvent data, CancellationToken token = default)
    {
        await using var connection = await _factory.GetConnectionAsync();
        await using var writer = await connection.CreateColumnWriterAsync($"INSERT INTO {Tables.ProductReadTable.TableName} VALUES", token);
        var columns = new object?[writer.FieldCount];

        columns[writer.GetOrdinal(Tables.ProductReadTable.IdColumn)] = new[]{ Guid.NewGuid() };
        columns[writer.GetOrdinal(Tables.ProductReadTable.PageColumn)] = new[] {data.Page};
        columns[writer.GetOrdinal(Tables.ProductReadTable.ProductionColumn)] = new[] {data.Production};
        columns[writer.GetOrdinal(Tables.ProductReadTable.ProductionIdColumn)] = new[] {data.ProductionId};
        columns[writer.GetOrdinal(Tables.ProductReadTable.CategoryIdColumn)] = new[] {data.CategoryId};
        columns[writer.GetOrdinal(Tables.ProductReadTable.ProducerColumn)] = new[] {data.Producer};
        columns[writer.GetOrdinal(Tables.ProductReadTable.UsernameColumn)] = new[] {data.UserName};
        columns[writer.GetOrdinal(Tables.ProductReadTable.CreatedColumn)] = new[] {data.Created };
        await writer.WriteTableAsync(columns, 1, token);
    }
}