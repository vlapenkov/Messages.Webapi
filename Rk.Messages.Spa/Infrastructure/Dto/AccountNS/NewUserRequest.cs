namespace Rk.Messages.Spa.Infrastructure.Dto.AccountNS;

public record NewUserRequest(string FirstName, string LastName, string Email, string Username, string Password, bool Enabled = true);