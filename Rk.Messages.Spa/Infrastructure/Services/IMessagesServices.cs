using Refit;

namespace Rk.Messages.Spa.Infrastructure.Services
{
    public interface IMessagesServices
    {
        [Get( "/api/v1/Messages" )]
        Task Get();
    }
}
