using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.SectionsNS.Dto;


namespace Rk.Messages.Logic.SectionsNS.Queries.GetAllSections
{
    public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, IReadOnlyCollection<SectionDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetAllSectionsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<IReadOnlyCollection<SectionDto>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var sectionsToFilter = _appDbContext.CatalogSections.AsQueryable();
            if(request.ParentSectionId != null)
            {
                sectionsToFilter = sectionsToFilter.Where(self => self.ParentCatalogSectionId == request.ParentSectionId);
            }
            var sectionsToReturn = sectionsToFilter
                  .Include(self => self.Children)
                  .Include(section => section.SectionDocuments)
                     .ThenInclude(pd => pd.Document)
                .ProjectTo<SectionDto>(_mapper.ConfigurationProvider);               

            return await sectionsToReturn.ToListAsync(); 
        }
    }
}
