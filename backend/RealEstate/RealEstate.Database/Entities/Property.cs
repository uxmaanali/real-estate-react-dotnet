using RealEstate.Database.Abstraction;
using RealEstate.Shared.Enums;


namespace RealEstate.Database.Entities;
public class Property : BaseEntity
{
    public required string Title { get; set; }

    public required float Price { get; set; }

    public string? Address { get; set; }

    public ListingType ListingType { get; set; }

    public int Bedrooms { get; set; } = 0;

    public int Bathrooms { get; set; } = 0;

    public int Carspots { get; set; } = 0;

    public string Description { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();

    public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
