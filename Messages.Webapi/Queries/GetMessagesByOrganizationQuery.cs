using MediatR;
using Messages.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Webapi.Commands
{
    public class GetMessagesByOrganizationQuery:  IRequest<IEnumerable<Message>>
    {
        public int ReceiverId { get; set; }

        public DateTime? SDate { get; set; }

        public DateTime? EDate { get; set; }


    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetMessagesByOrganizationQuery, IEnumerable<Message>>
    {
        private readonly MessagesRepository _repo;
        private readonly IMediator _mediator;
       
        public GetAllProductsQueryHandler(MessagesRepository repo, IMediator mediatr)
        {
            _repo = repo;
            _mediator = mediatr;
        }

        public async Task<IEnumerable<Message>> Handle(GetMessagesByOrganizationQuery request, CancellationToken cancellationToken)
        {
            Console.WriteLine("query handler");
            IEnumerable<Message> result = _repo.GetAll().Where(m => m.ReceiverId == request.ReceiverId).AsEnumerable();

            var idMessagesGot =result.Select(p => p.Id).Distinct();

            _mediator.Publish(new OrderPlacedEvent { Ids = idMessagesGot });
          return result;
        }
        
    }

}
