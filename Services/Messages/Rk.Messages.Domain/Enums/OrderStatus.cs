using System.ComponentModel;

namespace Rk.Messages.Domain.Enums
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    public enum OrderStatus
    {
        [Description("Новый")]
        New = 0,

        [Description("В обработке")]
        Processed = 1,

        [Description("Завершен")]
        Complete = 10
    }
}
