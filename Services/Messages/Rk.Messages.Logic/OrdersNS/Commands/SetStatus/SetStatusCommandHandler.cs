using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.OrdersNS.Commands.SetStatus 
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
            var orderFound = await _appDbContext.Orders.FirstOrDefaultAsync(self => self.Id == command.OrderId) ?? throw new EntityNotFoundException($"Заказ с Id= {command.OrderId} не найден");

            orderFound.SetStatus(command.Status);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
