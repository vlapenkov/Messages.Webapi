using MediatR;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganization
{
    public class GetOrganizationQuery :IRequest<OrganizationDto>
    {
        public long Id { get; init; }
    }
}
