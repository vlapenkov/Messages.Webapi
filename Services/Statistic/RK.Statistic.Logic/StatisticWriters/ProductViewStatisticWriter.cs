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
        await using var writer = await connection.CreateColumnWriterAsync($"INSERT INTO {Tables.ProductReadTable} VALUES", token);
        var columns = new object?[writer.FieldCount];

        columns[writer.GetOrdinal("id")] = new List<Guid>(1) { Guid.NewGuid() };
        columns[writer.GetOrdinal("page")] = new List<string?>(1) {data.Page};
        columns[writer.GetOrdinal("production")] = new List<string?>(1) {data.Production};
        columns[writer.GetOrdinal("productionId")] = new List<long>(1) {data.ProductionId};
        columns[writer.GetOrdinal("categoryId")] = new List<long>(1) {data.CategoryId};
        columns[writer.GetOrdinal("producer")] = new List<string?>(1) {data.Producer};
        columns[writer.GetOrdinal("username")] = new List<string?>(1) {data.UserName};
        columns[writer.GetOrdinal("created")] = new List<DateTime>(1) {data.Created };
        await writer.WriteTableAsync(columns, 1, token);
    }
}