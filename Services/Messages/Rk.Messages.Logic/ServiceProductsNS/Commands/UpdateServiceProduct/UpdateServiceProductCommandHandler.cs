using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;

namespace Rk.Messages.Logic.ServiceProductsNS.Commands.UpdateServiceProduct
{
    public class UpdateServiceProductCommandHandler : AsyncRequestHandler<UpdateServiceProductCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateServiceProductCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        protected override async Task Handle(UpdateServiceProductCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var productFound = await _appDbContext.ServiceProducts
            .Include(product => product.AttributeValues)
            .FirstOrDefaultAsync(self => self.Id == command.ProductId, cancellationToken: cancellationToken) ?? throw new EntityNotFoundException($"Услуга с Id= {command.ProductId} не найдена.");

            productFound.Update(request.CatalogSectionId, request.Name, request.FullName, request.Description, request.Price);
            productFound.SetAreForeignComponentsUsed(request.AreForeignComponentsUsed ?? false);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
