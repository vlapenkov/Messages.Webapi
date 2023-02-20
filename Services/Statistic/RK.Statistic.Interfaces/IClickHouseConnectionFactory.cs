using Octonica.ClickHouseClient;

namespace RK.Statistic.Interfaces;

/// <summary>
/// Фабрика подключений к ClickHouse
/// </summary>
public interface IClickHouseConnectionFactory
{
    /// <summary>
    /// Получить открытый коннект к БД
    /// </summary>
    /// <returns></returns>
    ClickHouseConnection GetConnection();

    /// <summary>
    /// Получить открытый конект к БД асинхронно
    /// </summary>
    /// <returns></returns>
    Task<ClickHouseConnection> GetConnectionAsync();
}