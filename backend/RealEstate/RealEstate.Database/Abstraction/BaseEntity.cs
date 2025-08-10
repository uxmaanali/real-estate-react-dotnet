namespace RealEstate.Database.Abstraction;

public class BaseEntity
{
    public int Id { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset ModifiedAt { get; set; }

    public string? UserName { get; set; }

    public bool IsActive { get; set; } = true;
}