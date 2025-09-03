namespace RealEstate.Database.Abstraction;

public class AuditableEntity
{
    public int Id { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    public string? ModifiedBy { get; set; }

    public bool IsActive { get; set; } = true;
}