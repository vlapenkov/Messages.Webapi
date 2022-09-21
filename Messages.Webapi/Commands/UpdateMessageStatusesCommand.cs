using MediatR;
using Messages.Domain;
using Messages.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Webapi.Commands
{
    public class UpdateMessageStatusesCommand : IRequest<bool>
    {
        public  IEnumerable<int> Ids { get; set; }
    }

    public class UpdateMessageStatusesCommandHandler : IRequestHandler<UpdateMessageStatusesCommand, bool>
    {
        private readonly MessagesRepository _repo;
        private readonly IMediator _mediatr;
        private readonly MessagesDbContext _db;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ITransientDbContext _dbContext;


        public UpdateMessageStatusesCommandHandler(
            MessagesRepository repo, 
            IMediator mediatr, 
            IServiceScopeFactory scopeFactory,
            MessagesDbContext db,
            ITransientDbContext dbContext
            )
        {
            _repo = repo;
            _mediatr = mediatr;
            _scopeFactory = scopeFactory;
            _db = db;
            _dbContext = dbContext;
        }

       async Task<bool> IRequestHandler<UpdateMessageStatusesCommand, bool>.Handle(UpdateMessageStatusesCommand command, CancellationToken cancellationToken)
        {
            await Task.Delay(5000);
            Console.WriteLine("UpdateMessageStatusesCommandHandler: " + String.Join(',',command.Ids));

            //using (var scope = _scopeFactory.CreateScope())
            //{
            //    var dbContext = scope.ServiceProvider.GetRequiredService<MessagesDbContext>();

            //    var messages = dbContext.Messages.Where(message => command.Ids.Contains(message.Id));
            //    foreach (var message in messages)
            //         message.IsRead = true;
            //  await  dbContext.SaveChangesAsync();
            //}

            var messages = _dbContext.Messages.Where(message => command.Ids.Contains(message.Id));
            foreach (var message in messages)
                message.IsRead = true;
             _dbContext.SaveChanges();

            return true;
        }
    }
}
