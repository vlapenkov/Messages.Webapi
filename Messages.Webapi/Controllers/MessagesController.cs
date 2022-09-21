using MediatR;
using Messages.Domain;
using Messages.Webapi.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messages.Webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private MessagesRepository _repo;
        private MessageTypeFactory _factory;
        private IMediator _mediator;

        public MessagesController(MessagesRepository repo, MessageTypeFactory factory, IMediator mediator)
        {
            _repo = repo;
            _factory = factory;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<string> Get(string messageType = "MessageType1Processor")
        {

           var messages  =await _mediator.Send(new GetMessagesByOrganizationQuery { ReceiverId = 1 });

           
                      
            Console.WriteLine("Main thread");
            var messageProcessor = _factory.GetProcessorByName(messageType);



            var result = messages
                .Select(message => messageProcessor.Process(message)).ToArray();



            var jsonString = JsonSerializer.Serialize<IEnumerable<object>>(result);

            return jsonString;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessage(int id)
        {

            return Ok(id);
        }

        [HttpPost]
        [Route("/{senderId}/{receiverId}")]
        public async Task<IActionResult> PostBook( Message messageDto)
        {

            return Ok(messageDto);


        }

        
    }
}