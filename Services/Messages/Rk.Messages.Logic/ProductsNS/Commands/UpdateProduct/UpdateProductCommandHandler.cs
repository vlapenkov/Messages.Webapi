using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Rk.Messages.Logic.ProductsNS.Commands.UpdateProductAttributes
{
    public class UpdateProductAttributesCommandHandler : AsyncRequestHandler<UpdateProductCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateProductAttributesCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var productFound = await _appDbContext.Products
                .Include(product => product.AttributeValues)
                .FirstOrDefaultAsync(self => self.Id == command.ProductId) ?? throw new EntityNotFoundException($"Товар с Id= {command.ProductId} не найден.");

            var attributeValues = command.Request.AttributeValues.Select(av => new AttributeValue(av.AttributeId, av.Value)).ToArray();

            productFound.UpdateAttributeValues(attributeValues);

            productFound.Update(request.CatalogSectionId, request.Name, request.FullName, request.Description, request.Price);


            productFound
            .SetCodeTnVed(request.CodeTnVed)                            
            .SetCodeOkpd2(request.CodeOkpd2)            
            .SetAddress(request.Address);

            productFound.SetArticle(request.Article);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
