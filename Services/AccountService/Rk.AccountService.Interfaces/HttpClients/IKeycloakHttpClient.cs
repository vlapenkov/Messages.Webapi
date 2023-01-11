using Rk.AccountService.Interfaces.Dto;
using Rk.AccountService.Interfaces.Dto.HttpClients;

namespace Rk.AccountService.Interfaces.HttpClients;

public interface IKeycloakHttpClient
{
    Task<TokenResponse> GetToken(string realm, TokenRequest requestData);
    Task CreateUser(string realm, string token, CreateUserRequest requestData);
}