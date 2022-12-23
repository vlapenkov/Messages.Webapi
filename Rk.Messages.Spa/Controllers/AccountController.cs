using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rk.Messages.Spa.Infrastructure.Dto.AccountNS;
using Rk.Messages.Spa.Infrastructure.Services;

namespace Rk.Messages.Spa.Controllers;


/// <summary>
/// Управление Учетными записями
/// </summary>
/// 
[Route("api/[controller]")]
[ApiController]    
public class AccountController : ControllerBase
{
    private readonly IAccountService _account;

    public AccountController(IAccountService account)
    {
        _account = account;
    }
    
    /// <summary>
    /// Создать Пользователя
    /// </summary>
    [AllowAnonymous] 
    [HttpPost]
    public async Task<TokenResponse> Register([FromBody] NewUserRequest request)
    {
        return await _account.CreateUser(request);
    }
}