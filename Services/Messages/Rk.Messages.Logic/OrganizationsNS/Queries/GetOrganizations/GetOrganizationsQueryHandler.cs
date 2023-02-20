using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizations
{
    public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, PagedResponse<OrganizationDto>>
    {
        private readonly IAppDbContext _appDbContext;
        private readonly IMapper _mapper;
                

        public GetOrganizationsQueryHandler(IAppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PagedResponse<OrganizationDto>> Handle(GetOrganizationsQuery request, CancellationToken cancellationToken)
        {           


            IPagedList<Organization> queryResult = await _appDbContext.Organizations.
                OrderBy(order => order.Id)
                .ToPagedListAsync(1, PagedRequest.DefaultPageSize);


            // преобразование из IPagedList<Order> -> PagedResponse<OrderShortDto>

            var sourceList = _mapper.Map<IEnumerable<OrganizationDto>>(queryResult);

            var result = _mapper.Map<PagedResponse<OrganizationDto>>(queryResult);

            result.Rows = sourceList;

            return result;
        }
    }
}
