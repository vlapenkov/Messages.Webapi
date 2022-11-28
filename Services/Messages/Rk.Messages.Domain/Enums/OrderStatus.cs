using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Enums
{
    public enum OrderStatus
    {
        [Description("Новый")]
        New =0,

        [Description("Завершенный")]
        Complete = 1
    }
}
