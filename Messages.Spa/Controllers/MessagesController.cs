using Messages.Webapi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Messages.Spa.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMessagesServices _messagesServices;

        public MessagesController(IMessagesServices messagesServices)
        {
            _messagesServices = messagesServices;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageDto>> GetMessages()
        {
            var result = await _messagesServices.Get( );
            return result;
        }
    }
}
