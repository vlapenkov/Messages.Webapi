using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;
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

        private readonly IUserService _userService;

        private readonly IValidator<CreateWorkProductCommand> _validator;

        public CreateWorkProductCommandHandler(IAppDbContext dbContext, IUserService userService, IValidator<CreateWorkProductCommand> validator)
        {
            _dbContext = dbContext;
            _userService = userService;
            _validator = validator;
        }

        public async Task<long> Handle(CreateWorkProductCommand command, CancellationToken cancellationToken)
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

            if (userOrganizationFound == null) throw new EntityNotFoundException($"Для ИНН пользователя {inn} нет организации");

            var request = command.Request;
                        

            WorkProduct product = new(
                userOrganizationFound.Id,
                request.CatalogSectionId,
                request.Name,
                request.FullName,
                request.Description,
                request.Price,
                new List<AttributeValue>()
                );
                      

            var productDocuments = request.Documents.Select(fd => new ProductDocument(new Document(fd.FileName, fd.FileId))).ToArray();

            product.AddProductDocuments(productDocuments);
            product.SetAreForeignComponentsUsed(request.AreForeignComponentsUsed ?? false);
            _dbContext.WorkProducts.Add(product);

            await _dbContext.SaveChangesAsync();

            return product.Id;

        }
    }
}
