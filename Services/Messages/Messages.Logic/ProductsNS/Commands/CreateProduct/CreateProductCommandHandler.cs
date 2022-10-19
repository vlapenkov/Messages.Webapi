using MediatR;
using Messages.Domain.Models;
using Messages.Domain.Models.Products;
using Messages.Interfaces.Interfaces.DAL;
using Messages.Logic.ProductsNS.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Logic.ProductsNS.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        public CreateProductCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var attributeValues = request.AttributeValues.Select(av => new AttributeValue(av.BaseProductId, av.AttributeId, av.Value)).ToArray();

            Product product = new Product(request.CatalogSectionId, request.Name, request.Description, attributeValues);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }
    }
}
