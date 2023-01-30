using RK.Messages.Shared.Contracts;

namespace RK.Statistic.Interfaces.StatisticWriters;

public interface IProductViewStatisticWriter
{
    Task InsertRowAsync(ProductViewStatisticEvent data, CancellationToken token = default);
}