using FluentValidation;
using MediatR;
using Messages.Domain.Models;
using Messages.Domain.Models.Products;
using Messages.Interfaces.Interfaces.DAL;
using Messages.Logic.ProductsNS.Dto;
using Messages.Logic.SectionsNS.Commands.CreateSectionCommand;
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

        private readonly IValidator<CreateProductCommand> _validator;

        public CreateProductCommandHandler(IAppDbContext dbContext, IValidator<CreateProductCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var request = command.Request;

            var attributeValues = request.AttributeValues.Select(av => new AttributeValue(av.BaseProductId, av.AttributeId, av.Value)).ToArray();

            Product product = new Product(request.CatalogSectionId, request.Name, request.Description, request.Price, attributeValues);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }
    }
}
