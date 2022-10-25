using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Messages.Spa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagesServices _messagesServices;
        private IHttpContextAccessor _contextAccessor;

        public MessagesController(IMessagesServices messagesServices, IHttpContextAccessor contextAccessor)
        {
            _messagesServices = messagesServices;
            _contextAccessor = contextAccessor;
        }

        /// <summary>        /// 
        /// </summary>
        
        [HttpGet]
        public async Task GetMessages()
        {
            Console.WriteLine(_contextAccessor.HttpContext.Items["X-Correlation-ID"]);
            //            throw new Exception( "Som etext" );
            await _messagesServices.Get();

        }
    }
}
