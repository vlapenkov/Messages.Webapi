using System.Data.Common;
using Octonica.ClickHouseClient;

namespace RK.Statistic.Interfaces;

public interface IClickHouseConnectionFactory
{
    ClickHouseConnection GetConnection();
}