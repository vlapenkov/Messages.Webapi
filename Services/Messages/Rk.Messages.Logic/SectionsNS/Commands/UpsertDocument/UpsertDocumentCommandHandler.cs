using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.SectionsNS.Commands.AddDocument
{
    public class UpsertDocumentCommandHandler : AsyncRequestHandler<UpsertDocumentCommand>
    {
        private readonly IAppDbContext _dbContext;

        public UpsertDocumentCommandHandler(IAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task Handle(UpsertDocumentCommand command, CancellationToken cancellationToken)
        {
            var sectionFound = await _dbContext.CatalogSections.FirstOrDefaultAsync(self => self.Id == command.SectionId) ?? throw new EntityNotFoundException($"Не найдена секция по Id={command.SectionId}");

            if (command.Document == null) throw new ArgumentNullException(nameof(command.Document));

            Document document = new(command.Document.FileName, command.Document.FileId);

            SectionDocument sectionDocument = new(document);

            sectionFound.UpsertDocument(sectionDocument);

            await _dbContext.SaveChangesAsync(cancellationToken);

        }
    }
}
