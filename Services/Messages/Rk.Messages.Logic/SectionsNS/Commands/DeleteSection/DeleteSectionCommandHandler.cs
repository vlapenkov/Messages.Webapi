using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities.Products;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.SectionsNS.Commands.DeleteSection
{
    public class DeleteSectionCommandHandler : AsyncRequestHandler<DeleteSectionCommand>
    {
        private readonly IAppDbContext _appDbContext;

        public DeleteSectionCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(DeleteSectionCommand command, CancellationToken cancellationToken)
        {
            bool hasChildSections = await _appDbContext.CatalogSections.Where(self => self.ParentCatalogSectionId == command.SectionId).AnyAsync();

            if (hasChildSections)
                throw new ValidationException(new[] { new ValidationFailure(nameof(command.SectionId), $"В разделе Id={command.SectionId} есть другие разделы.") });

            bool hasProducts = await _appDbContext.Products.Where(self => self.CatalogSectionId == command.SectionId).AnyAsync();

            if (hasProducts)
                throw new ValidationException(new[] { new ValidationFailure(nameof(command.SectionId), $"В разделе Id={command.SectionId} есть продукция.") });

            var sectionFound = await _appDbContext.CatalogSections.FirstOrDefaultAsync(self => self.Id == command.SectionId) ?? throw new EntityNotFoundException($"Секция с Id= {command.SectionId} не найдена");

            _appDbContext.CatalogSections.Remove(sectionFound);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
