using System.Data.Common;
using Octonica.ClickHouseClient;

namespace RK.Statistic.Interfaces;

/// <summary>
/// ФАбрика подключений к ClickHouse
/// </summary>
public interface IClickHouseConnectionFactory
{
    /// <summary>
    /// Получть открытый коннект к БД
    /// </summary>
    /// <returns></returns>
    ClickHouseConnection GetConnection();

    /// <summary>
    /// Получить открытй конект к БД асинхронно
    /// </summary>
    /// <returns></returns>
    Task<ClickHouseConnection> GetConnectionAsync();
}