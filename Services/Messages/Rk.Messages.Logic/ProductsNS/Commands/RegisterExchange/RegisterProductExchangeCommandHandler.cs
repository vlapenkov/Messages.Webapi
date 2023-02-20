using MediatR;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.AddExchange
{
    internal class RegisterProductExchangeCommandHandler : AsyncRequestHandler<RegisterProductExchangeCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public RegisterProductExchangeCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(RegisterProductExchangeCommand request, CancellationToken cancellationToken)
        {

            _appDbContext.ProductsExchanges.Add(new ProductsExchange(request.ExchangeType, request.ProductsLoaded));

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
