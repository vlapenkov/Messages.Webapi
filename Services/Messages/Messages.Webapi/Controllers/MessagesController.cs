using MediatR;
using Messages.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Messages.Webapi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private IMediator _mediator;
        private ILogger _logger;


        [HttpGet]
        public async Task Get()
        {

            throw new RkErrorException("Тестовая ошибка");

            /*  
                        throw new ValidationException( new[] { new ValidationFailure( "propertyName1", "errorMessage1" ) ,
                        new ValidationFailure( "propertyName2", "errorMessage2" ) } );
            */


        }

    }
}
