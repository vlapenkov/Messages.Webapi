using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers
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

        /// <summary>        /// 
        /// </summary>
        
        [HttpGet]
        public async Task GetMessages()
        {
            //            throw new Exception( "Som etext" );
            await _messagesServices.Get();

        }
    }
}
