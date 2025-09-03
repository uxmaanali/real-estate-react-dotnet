using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Properties;
public interface IPropertiesService
{
    Task<List<PropertyModel>> GetPropertiesAsync(PropertyFiltersRequestModel filters);

    Task<PropertyModel?> GetProperty(BaseModel request);
}
