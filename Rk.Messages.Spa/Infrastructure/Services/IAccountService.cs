using Refit;
using Rk.Messages.Spa.Infrastructure.Dto.AccountNS;

namespace Rk.Messages.Spa.Infrastructure.Services;

public interface IAccountService
{
    [Post("/api/v1/user")]
    Task<TokenResponse> CreateUser([Body] CreateUserRequest request);
}