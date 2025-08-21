using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Favorites;
public interface IFavoritesService
{
    Task<List<PropertyDto>> GetFavorites(int userId);

    Task<(bool success, string message)> AddRemoveFavorite(int userId, int propertyId);
}
