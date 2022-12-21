using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
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

        private readonly IUserService _userService;

        private readonly IValidator<CreateProductCommand> _validator;        

        
        public CreateProductCommandHandler(IAppDbContext dbContext, IUserService userService, IValidator<CreateProductCommand> validator)
        {
            _dbContext = dbContext;
            _userService = userService;
            _validator = validator;
        }

        public async Task<long> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //1. валидация запроса
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            //2. получение организации

            string inn = _userService.GetClaimValue("inn");

            if (inn == null) throw new RkUnauthorizedAccessException("У данного пользователя не задан ИНН");

            var userOrganizationFound = await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Inn == inn);

            if (userOrganizationFound == null) throw new EntityNotFoundException($"Для ИНН пользователя {inn} нет организации") ;

            // 3. Создание товара

            var request = command.Request;

            var attributeValues = request.AttributeValues.Select(av => new AttributeValue(av.AttributeId, av.Value)).ToArray();

            Product product = new(
                userOrganizationFound.Id,
                request.CatalogSectionId,
                request.Name,
                request.FullName,
                request.Description,
                request.Price,
                attributeValues
                );

            product
            .SetCodeTnVed(request.CodeTnVed)
            .SetCodeOkpd2(request.CodeOkpd2)            
            .SetAddress(request.Address);

            product.SetArticle(request.Article);

            var productDocuments = request.Documents.Select(fd => new ProductDocument(new Document(fd.FileName, fd.FileId))).ToArray();

            product.AddProductDocuments(productDocuments);

            _dbContext.Products.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }

    }
}
