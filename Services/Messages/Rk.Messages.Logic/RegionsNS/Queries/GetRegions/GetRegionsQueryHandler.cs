using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.RegionsNS.Queries.GetRegions
{
    /// <summary>
    /// Получить все регионы
    /// </summary>
    public class GetRegionsQueryHandler : IRequestHandler<GetRegionsQuery, IReadOnlyCollection<string>>
    {
        private readonly IAppDbContext _appDbContext;

        public GetRegionsQueryHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IReadOnlyCollection<string>> Handle(GetRegionsQuery request, CancellationToken cancellationToken)
        {
            return await _appDbContext.Organizations.Select(self => self.Region).Distinct().OrderBy(self=>self).ToArrayAsync(cancellationToken);
        }
    }
}
