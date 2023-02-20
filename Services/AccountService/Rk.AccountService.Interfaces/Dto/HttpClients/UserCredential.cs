namespace Rk.AccountService.Interfaces.Dto.HttpClients;

/// <summary>
/// Реквизиты для входа
/// </summary>
/// <param name="Value">значение</param>
/// <param name="Type">тип</param>
/// <param name="Temporary">временный</param>
public record UserCredential(string Value, string Type = "password",  bool Temporary = false);