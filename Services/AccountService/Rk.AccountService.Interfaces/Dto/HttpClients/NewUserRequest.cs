namespace Rk.AccountService.Interfaces.Dto.HttpClients;

/// <summary>
/// Запрос на создание нового пользователя
/// </summary>
/// <param name="FirstName">Имя</param>
/// <param name="LastName">Фамилия</param>
/// <param name="Email">email</param>
/// <param name="Username">имя пользователя</param>
/// <param name="Credentials">реквизиты для входа</param>
/// <param name="Groups">группы</param>
/// <param name="Attributes">атрибуты</param>
/// <param name="Enabled">включен</param>
public record CreateUserRequest(
    string FirstName, 
    string LastName,
    string Email, 
    string Username,
    UserCredential[] Credentials, 
    string[] Groups, 
    UserAttributes Attributes,
    bool Enabled = true);

/// <summary>
/// Реквизиты для входа
/// </summary>
/// <param name="Value">значение</param>
/// <param name="Type">тип</param>
/// <param name="Temporary">временный</param>
public record UserCredential(string Value, string Type = "password",  bool Temporary = false);

/// <summary>
/// Атрибуты пользователя
/// </summary>
/// <param name="Patronymic">Отчество</param>
public record UserAttributes(string Patronymic);