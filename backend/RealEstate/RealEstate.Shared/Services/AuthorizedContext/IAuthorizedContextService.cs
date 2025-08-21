using RealEstate.Shared.Enums;

namespace RealEstate.Shared.Services.AuthorizedContext;
public interface IAuthorizedContextService
{
    int? GetUserId();

    string? GetUserEmail();

    UserRole GetUserRole();
}
