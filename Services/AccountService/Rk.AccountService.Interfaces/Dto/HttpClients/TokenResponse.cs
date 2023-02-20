namespace Rk.AccountService.Interfaces.Dto.HttpClients;

/// <summary>
/// Ответ на запрос токена акторизации
/// </summary>
/// <param name="AccessToken">токен доступа</param>
/// <param name="ExpiresIn">Время жизни</param>
/// <param name="RefreshExpiresIn">время жизни токена обновления</param>
/// <param name="RefreshToken">токен обновления</param>
/// <param name="TokenType">тип токена</param>
/// <param name="NotBeforePolicy"></param>
/// <param name="SessionState"></param>
/// <param name="Scope"></param>
public record TokenResponse(string AccessToken, int ExpiresIn, int RefreshExpiresIn, string RefreshToken,
    string TokenType, int NotBeforePolicy, string SessionState, string Scope);