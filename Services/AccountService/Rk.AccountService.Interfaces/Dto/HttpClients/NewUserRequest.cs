namespace Rk.AccountService.Interfaces.Dto.HttpClients;

public record NewUserRequest(string FirstName, string LastName, string Email, string Username, string Password, bool Enabled = true);
public record UserCredential(string Value, string Type = "password",  bool Temporary = false);
public record NewUserWithCredentials(UserCredential[] Credentials, string FirstName, string LastName, string Email, string Username,  bool Enabled);