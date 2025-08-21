using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Properties;
public interface IPropertiesService
{
    Task<List<PropertyDto>> GetPropertiesAsync(PropertyFilters filters, int? userId);

    Task<PropertyDto?> GetProperty(int id, int? userId);
}
