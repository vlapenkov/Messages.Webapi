using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messages.Spa
{
    public interface IMessagesServices
    {
        [Get( "/api/v1/Messages" )]
        Task Get();
    }
}
