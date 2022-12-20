using System.ComponentModel;

namespace Rk.Messages.Domain.Enums
{
    public enum ProductExchangeType
    {
        [Description("")]
        Unknown = 0,

        [Description("Excel")]
        Excel = 1,

        [Description("1С")]
        OneS = 2
    }
}
