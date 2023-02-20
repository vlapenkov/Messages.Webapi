using MediatR;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrganizationsNS.Commands.CreateOrganization
{
    /// <summary>
    /// Команда для создания организации
    /// </summary>
    public class CreateOrganizationCommand :IRequest<long>
    {
        public CreateOrganizationRequest Request { get; set; }
    }
}
