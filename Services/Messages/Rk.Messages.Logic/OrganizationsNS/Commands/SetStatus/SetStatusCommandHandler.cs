using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrganizationsNS.Commands.SetStatus
{
    public class SetStatusCommandHandler : AsyncRequestHandler<SetStatusCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public SetStatusCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(SetStatusCommand command, CancellationToken cancellationToken)
        {
            Domain.Entities.Organization organizationFound = await _appDbContext.Organizations.FirstOrDefaultAsync(self => self.Id == command.OrganizationId) ?? throw new EntityNotFoundException($"Организация с Id= {command.OrganizationId} не найден");

            organizationFound.SetStatus(command.Status);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
