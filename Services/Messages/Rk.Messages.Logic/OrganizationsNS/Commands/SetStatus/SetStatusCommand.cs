using MediatR;
using Rk.Messages.Domain.Enums;

namespace Rk.Messages.Logic.OrganizationsNS.Commands.SetStatus
{
    /// <summary>
    /// Установить статус
    /// </summary>
    public class SetStatusCommand : IRequest
    {
        public long OrganizationId { get; set; }

        public OrganizationStatus Status { get; set; }
    }
}
