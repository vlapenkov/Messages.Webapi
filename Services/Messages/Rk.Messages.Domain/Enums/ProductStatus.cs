using System.ComponentModel;

namespace Rk.Messages.Domain.Enums
{
    /// <summary>
    /// Статус товара
    /// </summary>
    public enum ProductStatus
    {
        [Description("Черновик")]
        Draft,

        [Description("Опубликован")]
        Active,

        [Description("Архивный")]
        Archive
    }
}
