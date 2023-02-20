namespace Rk.Messages.Spa.Infrastructure.Dto.AccountNS;

public record TokenResponse(string AccessToken, int ExpiresIn, int RefreshExpiresIn, string RefreshToken,
    string TokenType, int NotBeforePolicy, string SessionState, string Scope);