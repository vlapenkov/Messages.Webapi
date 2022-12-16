using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.OrganizationsNS.Dto;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizationByInn
{
    public class GetOrganisationByInnQueryHandler : IRequestHandler<GetOrganizationByInnQuery, OrganizationDto>
    {
        private readonly IAppDbContext _appDbContext;

        private readonly IMapper _mapper;

        public GetOrganisationByInnQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> Handle(GetOrganizationByInnQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Inn))
            {
                throw new EntityNotFoundException($"Организация с ИНН={request.Inn} не найдена");
            }

            var organizationFound = await _appDbContext.Organizations.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Inn == request.Inn, cancellationToken: cancellationToken)
                    ?? throw new EntityNotFoundException($"Организация с ИНН={request.Inn} не найдена");
            
            return _mapper.Map<OrganizationDto>(organizationFound);
        }
    }
}