namespace RealEstate.Shared.Services.AuthorizedContext;

using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Enums;
using RealEstate.Shared.Extensions;

public class AuthorizedContextService : IAuthorizedContextService, IScopedDependency, IService
{
    private IHttpContextAccessor _httpContextAccessor;

    public AuthorizedContextService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int? GetUserId()
    {
        var sid = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value;
        return !string.IsNullOrEmpty(sid) ? Convert.ToInt32(sid) : null;
    }

    public string? GetUserEmail()
    {
        var email = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email)?.Value;
        return email;
    }

    public UserRole GetUserRole()
    {
        var role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
        var userRole = EnumExtensions.ParseOrDefault<UserRole>(role);
        return userRole != null ? userRole.Value : UserRole.Public;
    }
}
