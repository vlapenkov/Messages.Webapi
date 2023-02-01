using MediatR;
using Rk.AccountService.Interfaces.Dto.HttpClients;

namespace Rk.AccountService.Logic.UserNS.Commands.CreateUser;

/// <summary>
/// Команда на создание нового пользователя
/// </summary>
public class CreateUserCommand : IRequest<TokenResponse?>
{
    /// <summary></summary>
#pragma warning disable CS8618
    public CreateUserRequest Request { get; init; }
#pragma warning restore CS8618
}