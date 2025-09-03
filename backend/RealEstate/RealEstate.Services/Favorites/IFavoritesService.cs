using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Favorites;
public interface IFavoritesService
{
    Task<List<PropertyModel>> GetFavorites(BaseModel request);

    Task<(bool success, string message)> AddRemoveFavorite(BaseModel request);
}
