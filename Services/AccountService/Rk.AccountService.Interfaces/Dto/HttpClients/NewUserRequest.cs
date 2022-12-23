namespace Rk.AccountService.Interfaces.Dto.HttpClients;

public record NewUserRequest(string FirstName, string LastName, string Email, string UserName, string Password, bool Enabled = true);