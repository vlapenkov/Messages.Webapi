using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.SetStatus
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
            var productFound = await _appDbContext.Products.FirstOrDefaultAsync(self => self.Id == command.ProductId) ?? throw new EntityNotFoundException($"Продукция с Id= {command.ProductId} не найдена");

            productFound.SetStatus(command.Status);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
