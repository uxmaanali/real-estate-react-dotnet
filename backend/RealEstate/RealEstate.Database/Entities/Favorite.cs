using RealEstate.Database.Abstraction;

namespace RealEstate.Database.Entities;
public class Favorite : AuditableEntity
{
    public required int UserId { get; set; }

    public required int PropertyId { get; set; }

    public User User { get; set; }

    public Property Property { get; set; }
}
