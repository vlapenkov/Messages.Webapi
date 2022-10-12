using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagesServices _messagesServices;

        public MessagesController(IMessagesServices messagesServices)
        {
            _messagesServices = messagesServices;
        }

        [HttpGet]
        public async Task GetMessages()
        {
            //            throw new Exception( "Som etext" );
            await _messagesServices.Get();

        }
    }
}
