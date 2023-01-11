using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Rk.AccountService.Interfaces.Dto.HttpClients;
using Rk.AccountService.Interfaces.HttpClients;
using Rk.Messages.Common.Exceptions;
using Rk.Messages.Common.Json;

namespace Rk.AccountService.Infrastructure.HttpClients;

public class KeycloakHttpClient : BaseHttpClient, IKeycloakHttpClient
{
    public KeycloakHttpClient(HttpClient httpClient, ILogger<KeycloakHttpClient> logger) : base(httpClient, logger)
    {
    }
    
    public async Task<TokenResponse> GetToken(string realm, TokenRequest requestData)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"auth/realms/{realm}/protocol/openid-connect/token");
        var queryParams = new Dictionary<string, string>()
        {
            {"grant_type", requestData.GrantType},
            {"client_id", requestData.ClientId},
            {"username", requestData.UserName},
            {"password", requestData.Password},
        };
        request.Content = new FormUrlEncodedContent(queryParams);
        var response = await GetResponse<TokenResponse>(request, GetJsonOption(new SnakeCaseNamingPolicy()));
        if (response == null) throw new RkErrorException($"Ошибка при аутентификации {requestData.UserName}");
        return response;
    }

    public async Task CreateUser(string realm, string token, CreateUserRequest requestData)
    {
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"auth/admin/realms/{realm}/users");
        var bodyView = JsonSerializer.Serialize(requestData, GetJsonOption());
        request.Content = new StringContent(bodyView, Encoding.UTF8, "application/json");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await Send(request);
    }
}