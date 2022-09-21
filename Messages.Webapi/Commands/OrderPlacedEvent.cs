using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Messages.Webapi.Commands
{
    public class OrderPlacedEvent:INotification
    {
        public IEnumerable<int> Ids { get; set; }
    }

    public class OrderingEventsClientDispatcher : INotificationHandler<OrderPlacedEvent>
    {
        private readonly IMediator _mediatr;
        public OrderingEventsClientDispatcher(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine("OrderingEventsClientDispatcher" + notification.Ids);
            _mediatr.Send(new UpdateMessageStatusesCommand { Ids = notification.Ids });
        }
    }
}
