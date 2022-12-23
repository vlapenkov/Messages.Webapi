namespace Rk.AccountService.Interfaces.Dto.HttpClients;

public record TokenRequest(string GrantType, string ClientId, string UserName, string Password);