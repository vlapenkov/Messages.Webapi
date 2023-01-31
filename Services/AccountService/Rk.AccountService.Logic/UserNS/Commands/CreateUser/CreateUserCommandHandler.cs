using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Rk.AccountService.Interfaces.Dto.HttpClients;
using Rk.AccountService.Interfaces.HttpClients;
using Rk.Messages.Common.Exceptions;

namespace Rk.AccountService.Logic.UserNS.Commands.CreateUser;

/// <summary>
/// Обработчик команды добавление нового пользователя
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, TokenResponse?>
{
    private readonly IConfiguration _configuration;
    private readonly IKeycloakHttpClient _http;
    private readonly IValidator<CreateUserCommand> _validator;

    /// <summary>
    /// Конструктор обработчика команда добавления нового пользщователя
    /// </summary>
    public CreateUserCommandHandler(IKeycloakHttpClient http, IConfiguration configuration, 
        IValidator<CreateUserCommand> validator)
    {
        _configuration = configuration;
        _validator = validator;
        _http = http;
    }

    /// <inheritdoc />
    public async Task<TokenResponse?> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var realm = _configuration["Oidc:Realm"];
        var clientId = _configuration["Oidc:ClientId"];
        var grantType = _configuration["Oidc:GrantType"] ?? "password";
        var userName = _configuration["Oidc:AdminUserName"];
        var password = _configuration["Oidc:AdminPassword"];

        if (realm == null || clientId == null || userName == null || password == null)
            throw new RkErrorException("Отсутствуют параметры конфигурации, для создания пользователей.");
        
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        var adminTokenResponse = await _http.GetToken(realm, new TokenRequest(grantType, clientId, userName, password));
        await _http.CreateUser(realm, adminTokenResponse.AccessToken, request.Request);
        var newUserTokenResponse = await _http.GetToken(realm, 
            new TokenRequest(grantType, clientId, request.Request.Username, request.Request.Credentials[0].Value));
        return newUserTokenResponse;
    }
}