using Messages.Webapi.Dto;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Spa
{
    public interface IMessagesServices
    {
        [Get( "/api/Messages" )]
        Task<IEnumerable<MessageDto>> Get();
    }
}