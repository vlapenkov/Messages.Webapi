using MediatR;
using Rk.AccountService.Interfaces.Dto.HttpClients;

namespace Rk.AccountService.Logic.UserNS.Commands.CreateUser;

public class CreateUserCommand : IRequest<TokenResponse?>
{
    public CreateUserRequest Request { get; set; }
}