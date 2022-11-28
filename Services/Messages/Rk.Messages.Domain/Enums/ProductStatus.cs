using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Enums
{
    public enum ProductStatus
    {
        [Description("В наличии")]
        OnStock,

        [Description("Под заказ")]
        OnDemand
    }
}
