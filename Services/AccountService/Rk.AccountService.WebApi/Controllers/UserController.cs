using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.AccountService.Interfaces.Dto.HttpClients;
using Rk.AccountService.Logic.UserNS.Commands.CreateUser;

namespace Rk.AccountService.WebApi.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<TokenResponse> CreateUser(NewUserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand
        {
            NewUserData = request
        });
        return result;
    }
    
}