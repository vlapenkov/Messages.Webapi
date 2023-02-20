using Rk.AccountService.Interfaces.Dto;
using Rk.AccountService.Interfaces.Dto.HttpClients;

namespace Rk.AccountService.Interfaces.HttpClients;

/// <summary>
/// Клиент для взаимодействия с Keycloack
/// </summary>
public interface IKeycloakHttpClient
{
    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    Task<TokenResponse> GetToken(string realm, TokenRequest requestData);
    
    /// <summary>
    /// Получение ответа авторизации
    /// </summary>
    Task CreateUser(string realm, string token, CreateUserRequest requestData);
}