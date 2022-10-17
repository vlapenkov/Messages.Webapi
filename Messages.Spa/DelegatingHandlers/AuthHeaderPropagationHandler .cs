using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Messages.Spa.DelegatingHandlers;

/// <summary>
/// Хэндлер для проброса токена в следующие вызовы API
/// </summary>

public class AuthHeaderPropagationHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _contextAccessor;
    private const string AuthHeaderName = "Authorization";

    public AuthHeaderPropagationHandler(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (_contextAccessor.HttpContext == null) 
            return base.SendAsync(request, cancellationToken);

        var auth = request.Headers.Authorization;
        if (auth == null)
            return base.SendAsync(request, cancellationToken);

        var headerValue = this._contextAccessor.HttpContext.Request.Headers[AuthHeaderName];
        request.Headers.TryAddWithoutValidation(AuthHeaderName, (string[])headerValue);
        return base.SendAsync(request, cancellationToken);
    }

}