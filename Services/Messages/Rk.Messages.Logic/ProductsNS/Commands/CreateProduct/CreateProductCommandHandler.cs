using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Contracts;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
using Rk.Messages.Logic.ProductsNS.Dto;

namespace Rk.Messages.Logic.ProductsNS.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IValidator<CreateProductCommand> _validator;

        private readonly IFileStoreService _fileService;       

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

            Product product = new Product(request.CatalogSectionId, request.Name, request.Description, request.Price, attributeValues);

            await CreateDocuments(request.Documents);

            var productDocuments = request.Documents.Select(fd => new ProductDocument(new Document(fd.FileName,  fd.FileId))).ToArray();

            product.AddProductDocuments(productDocuments);           

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }

        /// <summary>
        /// Создать файлы в FileStoreService
        /// </summary>
        /// <param name="documents">документы для файлов</param>
        /// <returns></returns>
        private async Task CreateDocuments(List<FileDataDto> documents)
        {
            var tasks = new Task<Guid>[documents.Count];


            for (int i = 0; i < documents.Count; i++)
            {
                tasks[i] = _fileService.CreateFile(new CreateFileRequest { FileName = documents[i].FileName, Data = documents[i].Data });
            }

            var resultFiles = await Task.WhenAll(tasks);

            int counter = 0;

            documents.ForEach(fileData => fileData.FileId = resultFiles[counter++]);

        }
    }
}
