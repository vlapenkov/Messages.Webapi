using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Octonica.ClickHouseClient;
using RK.Statistic.Interfaces;

namespace RK.Statistic.Infrastructure.ClickHouse;

/// <inheritdoc />
public class ClickHouseConnectionFactory : IClickHouseConnectionFactory
{
    private readonly string _connectionString;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="config">конфигурация</param>
    /// <exception cref="NullReferenceException">отсуствет строка подключения</exception>
    public ClickHouseConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("ClickHouse") 
                            ?? throw new NullReferenceException("Отсутствует строка подключения");
    }

    /// <inheritdoc />
    public ClickHouseConnection GetConnection()
    {
        var connection = new ClickHouseConnection(_connectionString);
        connection.Open();
        return connection;
    }

    /// <inheritdoc />
    public async Task<ClickHouseConnection> GetConnectionAsync()
    {
        var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}