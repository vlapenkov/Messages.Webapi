using MediatR;
using Messages.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Messages.Domain.Models;
using Messages.Common;
using Microsoft.EntityFrameworkCore;
using Messages.Common.Exceptions;
using FluentValidation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Messages.Logic.SectionsNS.Commands.CreateSectionCommand
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, long>
    {        

        private readonly IAppDbContext _dbContext;

        private readonly IValidator<CreateSectionCommand> _validator;

        public CreateSectionCommandHandler(IAppDbContext dbContext, IValidator<CreateSectionCommand> validator)
        {
            _dbContext = dbContext;

            _validator = validator;
        }

        public async Task<long> Handle(CreateSectionCommand command, CancellationToken cancellationToken)
        {
           var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            await ValidateSection(command);

            var catalogSection  = new CatalogSection(command.ParentSectionId, command.Name);       

            _dbContext.CatalogSections.Add(catalogSection);

            await _dbContext.SaveChangesAsync();

            return catalogSection.Id;

        }

        /// <summary>
        /// Проверки на уровне бизнес -логики
        /// </summary>        
        public async Task ValidateSection(CreateSectionCommand command) {

            var doublesByNameExists = await _dbContext.CatalogSections.AnyAsync(catalog => catalog.Name == command.Name);

            if (doublesByNameExists) throw new RkErrorException($"Раздел с наименованием {command.Name} уже существует");

            if (command.ParentSectionId != null)
            {
                bool parentIdSectionExists = await _dbContext.CatalogSections.AnyAsync(catalog => catalog.Id == command.ParentSectionId);

                if (!parentIdSectionExists) throw new RkErrorException($"Родительского раздела  {command.ParentSectionId} не существует");
            }


        }
    }
}
