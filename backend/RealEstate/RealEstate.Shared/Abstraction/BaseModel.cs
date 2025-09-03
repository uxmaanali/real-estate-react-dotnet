namespace RealEstate.Shared.Abstraction;
public record BaseModel
{
    public CancellationToken CancellationToken { get; set; }

    public int? UserId { get; set; }

    public int Id { get; set; } = default;

}
