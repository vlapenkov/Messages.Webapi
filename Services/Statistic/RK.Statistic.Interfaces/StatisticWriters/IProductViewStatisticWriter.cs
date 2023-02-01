using RK.Messages.Shared;

namespace RK.Statistic.Interfaces.StatisticWriters;

/// <summary>
/// Сервис для записи информации о просмотре товаров в БД
/// </summary>
public interface IProductViewStatisticWriter
{
    /// <summary>
    /// Метод вставки строки в БД
    /// </summary>
    Task InsertRowAsync(ProductViewStatisticEvent data, CancellationToken token = default);
}