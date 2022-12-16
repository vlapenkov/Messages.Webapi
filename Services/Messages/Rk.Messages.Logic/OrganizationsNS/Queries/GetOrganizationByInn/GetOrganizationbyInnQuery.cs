using MediatR;
using Rk.Messages.Logic.OrganizationsNS.Dto;

namespace Rk.Messages.Logic.OrganizationsNS.Queries.GetOrganizationByInn
{
    public class GetOrganizationByInnQuery :IRequest<OrganizationDto>
    {
        public string Inn { get; init; }
    }
}
