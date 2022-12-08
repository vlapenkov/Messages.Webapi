using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.WorkProductsNS.Commands.CreateWorkProduct
{
    public class CreateWorkProductCommandHandler : IRequestHandler<CreateWorkProductCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IValidator<CreateWorkProductCommand> _validator;

        public CreateWorkProductCommandHandler(IAppDbContext dbContext, IValidator<CreateWorkProductCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        private static readonly long _defaultOrganizationId = 1L;

        public async Task<long> Handle(CreateWorkProductCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var request = command.Request;
                        

            WorkProduct product = new(
                _defaultOrganizationId,
                request.CatalogSectionId,
                request.Name,
                request.FullName,
                request.Description,
                request.Price,
                new List<AttributeValue>()
                );
                      

            var productDocuments = request.Documents.Select(fd => new ProductDocument(new Document(fd.FileName, fd.FileId))).ToArray();

            product.AddProductDocuments(productDocuments);

            _dbContext.WorkProducts.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }
    }
}
