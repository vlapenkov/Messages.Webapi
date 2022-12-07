using System.ComponentModel;

namespace Rk.Messages.Domain.Enums
{
    /// <summary>
    /// Статус достпности товара
    /// </summary>
    public enum AvailableStatus
    {
        [Description("В наличии")]
        OnStock,

        [Description("Под заказ")]
        OnDemand
    }
}
