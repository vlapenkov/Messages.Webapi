using MediatR;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System.Threading;
using System.Threading.Tasks;


namespace Rk.Messages.Logic.NewsNS.Commands.DeleteNews
{
    public class DeleteNewsCommandHandler : AsyncRequestHandler<DeleteNewsCommand>
    {

        private readonly IAppDbContext _appDbContext;

        public DeleteNewsCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        protected override async Task Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var newsFound = await _appDbContext.News.FirstOrDefaultAsync(self => self.Id == request.Id) ?? throw new EntityNotFoundException($"Новость с Id= {request.Id} не найдена");

            _appDbContext.News.Remove(newsFound);

            await _appDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
