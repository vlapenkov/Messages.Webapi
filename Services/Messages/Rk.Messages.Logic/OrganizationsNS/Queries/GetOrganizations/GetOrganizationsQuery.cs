using MediatR;
using Rk.Messages.Logic.CommonNS.Dto;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizations
{
    public class GetOrganizationsQuery : IRequest<PagedResponse<OrganizationDto>>
    {
    }
}
