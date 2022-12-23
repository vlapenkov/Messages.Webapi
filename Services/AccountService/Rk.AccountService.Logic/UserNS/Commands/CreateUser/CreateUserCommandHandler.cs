using MediatR;
using Microsoft.Extensions.Configuration;
using Rk.AccountService.Interfaces.Dto.HttpClients;
using Rk.AccountService.Interfaces.HttpClients;
using Rk.Messages.Common.Exceptions;

namespace Rk.AccountService.Logic.UserNS.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, TokenResponse?>
{
    private readonly IConfiguration _configuration;
    private readonly IKeycloakHttpClient _http;

    public CreateUserCommandHandler(IConfiguration configuration, IKeycloakHttpClient http)
    {
        _configuration = configuration;
        _http = http;
    }

    public async Task<TokenResponse?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var realm = _configuration["Oidc:Realm"];
        var clientId = _configuration["Oidc:Realm"];
        var grantType = _configuration["Oidc:GrantType"] ?? "password";
        var userName = _configuration["Oidc:AdminUserName"];
        var password = _configuration["Oidc:AdminPassword"];

        if (realm == null || clientId == null || userName == null || password == null)
            throw new RkErrorException("Отсутствуют параметры конфигурации, для создания пользователей.");

        var adminTokenResponse = await _http.GetToken(realm, new TokenRequest(grantType, clientId, userName, password));
        await _http.CreateUser(realm, adminTokenResponse.AccessToken, request.NewUserData);
        var newUserTokenResponse = await _http.GetToken(realm, 
            new TokenRequest(grantType, clientId, request.NewUserData.UserName, request.NewUserData.Password));
        return newUserTokenResponse;
    }
}