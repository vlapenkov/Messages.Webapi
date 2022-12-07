using FluentValidation;
using MediatR;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Domain.Enums;
using Rk.Messages.Interfaces.Interfaces.DAL;
using Rk.Messages.Logic.OrganizationsNS.Dto;
using Rk.Messages.Logic.ProductsNS.Commands.CreateProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Rk.Messages.Logic.OrganizationsNS.Commands.CreateOrganization
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, long>
    {
        private readonly IAppDbContext _dbContext;

        private readonly IValidator<CreateOrganizationRequest> _validator;

        public CreateOrganizationCommandHandler(IAppDbContext dbContext, IValidator<CreateOrganizationRequest> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<long> Handle(CreateOrganizationCommand command, CancellationToken cancellationToken)
        {

            var request = command.Request;

            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            Organization organization = new(               
                request.Name,
                request.FullName,
                request.Ogrn,
                request.Inn,
                request.Kpp,
                request.Region,
                request.City,
                request.Address,
                request.Site,
                request.Okved,
                request.Okved2,
                request.Phone,
                request.Email,
                OrganizationStatus.New
                );

            _dbContext.Organizations.Add(organization);

           await _dbContext.SaveChangesAsync(cancellationToken);

            return organization.Id;
        }
    }
}
