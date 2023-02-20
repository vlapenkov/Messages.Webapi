using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.RegionsNS.Queries.GetRegions
{
    /// <summary>
    /// Получить все регионы
    /// </summary>
    public class GetRegionsQuery :IRequest<IReadOnlyCollection<string>>
    {
    }
}
