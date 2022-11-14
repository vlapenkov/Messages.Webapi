﻿using FluentValidation;
using MediatR;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.ProductsNS.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IValidator<CreateProductCommand> _validator;

        private readonly IFileStoreService _fileService;

        private static readonly long _defaultOrganizationId = 1L;

        public CreateProductCommandHandler(IAppDbContext dbContext, IValidator<CreateProductCommand> validator, IFileStoreService fileService)
        {
            _dbContext = dbContext;
            _validator = validator;
            _fileService = fileService;
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

            Product product = new Product
                (
                _defaultOrganizationId, 
                request.CatalogSectionId, 
                request.Name, 
                request.FullName, 
                request.Description, 
                request.Price, 
                attributeValues
                );

            //  await CreateDocuments(request.Documents);

            var productDocuments = request.Documents.Select(fd => new ProductDocument(new Document(fd.FileName, fd.FileId))).ToArray();

            product.AddProductDocuments(productDocuments);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }


    }
}
