namespace RealEstate.Shared.Models.Properties;

public class PropertyDto
{
    public int Id { get; set; }

    public required string Title { get; set; }

    public required float Price { get; set; }

    public string? Address { get; set; }

    public string ListingType { get; set; } = string.Empty;

    public int Bedrooms { get; set; } = 0;

    public int Bathrooms { get; set; } = 0;

    public int Carspots { get; set; } = 0;

    public string Description { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new();

    public bool IsFavorite { get; set; }
}
