using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization
{
    public class GetOrganisationQueryHandler : IRequestHandler<GetOrganizationQuery, OrganizationDto>
    {
       private readonly IAppDbContext _appDbContext;
       
       private readonly IMapper _mapper;

        public GetOrganisationQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<OrganizationDto> Handle(GetOrganizationQuery request, CancellationToken cancellationToken)
        {
           var organizationFound =  await _appDbContext.Organizations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id) 
                ?? throw new EntityNotFoundException($"Организация с Id={request.Id} не найдена");

            return _mapper.Map<OrganizationDto>(organizationFound);

        }
    }
}
