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

namespace Messages.Logic.SectionsNS.Commands.CreateSectionCommand
{
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, long>
    {        

        private readonly IAppDbContext _dbContext;

        private readonly IRepository<CatalogSection> _catalogRepository;

        public CreateSectionCommandHandler(IAppDbContext dbContext, IRepository<CatalogSection> catalogRepository)
        {
            _dbContext = dbContext;

            _catalogRepository = catalogRepository;
        }

        public async Task<long> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var catalogSection  = new CatalogSection(request.ParentSectionId, request.Name);            

           var doublesByNameExists = await _dbContext.CatalogSections.AnyAsync(catalog => catalog.Name == request.Name);

            if (doublesByNameExists) throw new RkErrorException($"Раздел с наименованием {request.Name} уже существует"); ;

            _dbContext.CatalogSections.Add(catalogSection);

            await _dbContext.SaveChangesAsync();

            return catalogSection.Id;

        }
    }
}
