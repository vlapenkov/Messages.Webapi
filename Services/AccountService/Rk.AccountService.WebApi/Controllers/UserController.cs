using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rk.AccountService.Interfaces.Dto.HttpClients;
using Rk.AccountService.Logic.UserNS.Commands.CreateUser;

namespace Rk.AccountService.WebApi.Controllers;

/// <summary>
/// Контроллер для работы с пользователями
/// </summary>
[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <inheritdoc />
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    [HttpPost]
    public async Task<TokenResponse> CreateUser(CreateUserRequest request)
    {
        var result = await _mediator.Send(new CreateUserCommand
        {
            Request = request
        });
        return result;
    }
    
}