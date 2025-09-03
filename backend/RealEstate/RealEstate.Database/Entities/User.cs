namespace RealEstate.Database.Entities;

using RealEstate.Database.Abstraction;
using RealEstate.Shared.Enums;

public class User : AuditableEntity
{
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public string Salt { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
