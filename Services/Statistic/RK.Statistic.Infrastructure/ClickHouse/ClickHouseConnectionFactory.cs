using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Octonica.ClickHouseClient;
using RK.Statistic.Interfaces;

namespace RK.Statistic.Infrastructure.ClickHouse;

public class ClickHouseConnectionFactory : IClickHouseConnectionFactory
{
    private readonly string _connectionString;

    public ClickHouseConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("ClickHouse") 
                            ?? throw new NullReferenceException("Отсутствует строка подключения");
    }

    public ClickHouseConnection GetConnection()
    {
        var connection = new ClickHouseConnection(_connectionString);
        connection.Open();
        return connection;
    }

    public async Task<ClickHouseConnection> GetConnectionAsync()
    {
        var connection = new ClickHouseConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}