using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Messages.Common;
using Messages.Domain;
using Messages.Webapi.Commands;
using Messages.Webapi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messages.Webapi.Controllers
{
    [Route( "api/v1/[controller]" )]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private MessagesRepository _repo;
        private MessageTypeFactory _factory;
        private IMediator _mediator;
        private ILogger _logger;

        public MessagesController(MessagesRepository repo, MessageTypeFactory factory, IMediator mediator, ILogger<MessagesController> logger)
        {
            _repo = repo;
            _factory = factory;
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<MessageDto>> Get(string messageType = "MessageType1Processor")
        {

            _logger.LogWarning( "Предупреждение системы" );

          
            throw new RkErrorException( "Тестовое сообщение1" );

                        /*  
                                    throw new ValidationException( new[] { new ValidationFailure( "propertyName1", "errorMessage1" ) ,
                                    new ValidationFailure( "propertyName2", "errorMessage2" ) } );
                        */

                        var messages = await _mediator.Send( new GetMessagesByOrganizationQuery { ReceiverId = 1 } );

            return messages.Select( message => new MessageDto
            {
                Id = message.Id,
                Name = message.Name,
                Description = message.Description
            } );


        }

        [HttpGet( "{id}" )]
        public async Task<IActionResult> GetMessage(int id)
        {
            throw new RkErrorException( "Тест не работает" );

            return Ok( id );
        }

        [HttpPost]
        [Route( "/{senderId}/{receiverId}" )]
        public async Task<IActionResult> PostBook(Message messageDto)
        {

            return Ok( messageDto );


        }


    }
}
