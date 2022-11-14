using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Domain.Entities
{
    public enum OrganizationStatus
    {
        [Description("Активная")]
        Working,

        [Description("Закрыта")]
        InActive
    }
}
