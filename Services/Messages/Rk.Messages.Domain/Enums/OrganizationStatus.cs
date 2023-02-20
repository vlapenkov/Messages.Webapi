using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Enums
{
    /// <summary>
    /// Статусы организаций
    /// </summary>
    public enum OrganizationStatus
    {
        [Description("Новая")]
        New = 0,

        [Description("Активная")]
        Working = 1,


        [Description("Закрыта")]
        Closed = 10,
    }
}
