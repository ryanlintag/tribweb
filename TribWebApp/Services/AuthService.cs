using Microsoft.Identity.Web;

public interface IAuthService
{
    Task<bool> IsAuthenticatedAsync();
    Task<string> GetUserNameAsync();
}

public class AuthService : IAuthService
{
    private readonly ITokenAcquisition _tokenAcquisition;
    private readonly IHttpContextAccessor _contextAccessor;

    public AuthService(
        ITokenAcquisition tokenAcquisition,
        IHttpContextAccessor contextAccessor)
    {
        _tokenAcquisition = tokenAcquisition;
        _contextAccessor = contextAccessor;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        return _contextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public async Task<string> GetUserNameAsync()
    {
        return _contextAccessor.HttpContext?.User?.Identity?.Name ?? "Anonymous";
    }
}