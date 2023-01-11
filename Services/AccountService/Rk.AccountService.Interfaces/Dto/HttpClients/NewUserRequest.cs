namespace Rk.AccountService.Interfaces.Dto.HttpClients;

public record CreateUserRequest(
    string FirstName, 
    string LastName,
    string Email, 
    string Username,
    UserCredential[] Credentials, 
    string[] Groups, 
    UserAttributes Attributes,
    bool Enabled = true);
public record UserCredential(string Value, string Type = "password",  bool Temporary = false);
public record UserAttributes(string Patronymic);