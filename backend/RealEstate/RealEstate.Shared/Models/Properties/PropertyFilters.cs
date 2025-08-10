using System.ComponentModel.DataAnnotations;

using RealEstate.Shared.Enums;

namespace RealEstate.Shared.Models.Properties;
public class PropertyFilters
{
    [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string? Title { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Price must be positive")]
    public float? MinPrice { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Price must be positive")]
    public float? MaxPrice { get; set; }

    public int? Bedrooms { get; set; }

    public int? Bathrooms { get; set; }

    public int? Carspots { get; set; }

    public ListingType? ListingType { get; set; }
}
