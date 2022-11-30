using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Rk.Messages.Logic.ProductsNS.Commands.UpdateProductAttributes
{
    public class UpdateProductAttributesCommandHandler : AsyncRequestHandler<UpdateProductAttributesCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public UpdateProductAttributesCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(UpdateProductAttributesCommand command, CancellationToken cancellationToken)
        {
            var productFound = await _appDbContext.Products
                .Include(product=>product.AttributeValues)
                .FirstOrDefaultAsync(self => self.Id == command.ProductId) ?? throw new EntityNotFoundException($"Продукция с Id= {command.ProductId} не найдена");

            var attributeValues = command.AttributeValues.Select(av => new AttributeValue(av.AttributeId, av.Value)).ToArray();

            productFound.UpdateAttributeValues(attributeValues);

           await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
