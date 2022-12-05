using MediatR;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rk.Messages.Logic.NewsNS.Commands.CreateNews
{
    public class CreateNewsCommandHandler : IRequestHandler<CreateNewsCommand, long>
    {
        private readonly IAppDbContext _appDbContext;

        public CreateNewsCommandHandler(IAppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<long> Handle(CreateNewsCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var newsAdded = new News(request.Name, request.Description, request.Document?.FileId);

            _appDbContext.News.Add(newsAdded);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            return newsAdded.Id;
        }
    }
}
