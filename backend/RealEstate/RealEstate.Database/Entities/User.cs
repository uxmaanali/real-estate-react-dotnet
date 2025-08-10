using RealEstate.Database.Abstraction;
using RealEstate.Shared.Enums;

namespace RealEstate.Database.Entities;
public class User : BaseEntity
{
    public required string Email { get; set; }

    public required string PasswordHash { get; set; }

    public string Salt { get; set; }

    public UserRole Role { get; set; }

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
