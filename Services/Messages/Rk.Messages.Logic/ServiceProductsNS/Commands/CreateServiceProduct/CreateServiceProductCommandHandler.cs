﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Interfaces.Services;

namespace Rk.Messages.Logic.ServiceProductsNS.Commands.CreateServiceProduct
{
    public class CreateServiceProductCommandHandler : IRequestHandler<CreateServiceProductCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IUserService _userService;

        private readonly IValidator<CreateServiceProductCommand> _validator;

        public CreateServiceProductCommandHandler(IAppDbContext dbContext, IUserService userService, IValidator<CreateServiceProductCommand> validator)
        {
            _dbContext = dbContext;
            _userService = userService;
            _validator = validator;
        }

        public async Task<long> Handle(CreateServiceProductCommand command, CancellationToken cancellationToken)
        {
            //1. валидация запроса

            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }


            //2. получение организации

            string inn = _userService.GetClaimValue("inn");

            if (inn == null) throw new RkUnauthorizedAccessException("У данного пользователя не задан ИНН");

            var userOrganizationFound = await _dbContext.Organizations.FirstOrDefaultAsync(x => x.Inn == inn, cancellationToken: cancellationToken);

            if (userOrganizationFound == null) throw new EntityNotFoundException($"Для ИНН пользователя {inn} нет организации");

            var request = command.Request;
            
            ServiceProduct product = new(
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

            _dbContext.ServiceProducts.Add(product);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return product.Id;

        }
    }
}
