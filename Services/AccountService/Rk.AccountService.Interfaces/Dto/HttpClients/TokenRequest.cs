namespace Rk.AccountService.Interfaces.Dto.HttpClients;

/// <summary>
/// Запрос токена авторизации
/// </summary>
/// <param name="GrantType">тип гранта</param>
/// <param name="ClientId">ид клиента</param>
/// <param name="UserName">имя пользвателя</param>
/// <param name="Password">пароль</param>
public record TokenRequest(string GrantType, string ClientId, string UserName, string Password);