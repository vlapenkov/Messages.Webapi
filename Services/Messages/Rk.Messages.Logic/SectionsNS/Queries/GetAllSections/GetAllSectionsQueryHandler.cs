using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rk.Messages.Logic.SectionsNS.Dto;

namespace Rk.Messages.Logic.SectionsNS.Queries.GetAllSections
{
    public class GetAllSectionsQueryHandler :IRequestHandler<GetAllSectionsQuery, IReadOnlyCollection<SectionDto>>
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
            var result =await _appDbContext.CatalogSections
                .Where(self => request.ParentSectionId == null || self.ParentCatalogSectionId == request.ParentSectionId)
                .ProjectTo<SectionDto>(_mapper.ConfigurationProvider).
                ToListAsync(cancellationToken);

                return result;
        }
    }
}
