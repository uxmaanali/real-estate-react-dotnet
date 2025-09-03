using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Favorites;

public class FavoritesService : IFavoritesService, IScopedDependency
{
    private readonly RealEstateContext _dbContext;

    public FavoritesService(RealEstateContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PropertyModel>> GetFavorites(BaseModel request)
    {
        var properties = await _dbContext.Favorites
            .Where(x => x.UserId == request.UserId)
            .Select(x => new PropertyModel
            {
                Id = x.Id,
                Title = x.Property.Title,
                Price = x.Property.Price,
                Address = x.Property.Address,
                Bathrooms = x.Property.Bathrooms,
                Bedrooms = x.Property.Bathrooms,
                Carspots = x.Property.Carspots,
                Description = x.Property.Description,
                Images = x.Property.Images,
                ListingType = x.Property.ListingType.ToString(),
                IsFavorite = true,
            })
            .ToListAsync(request.CancellationToken);

        return properties;
    }

    public async Task<(bool success, string message)> AddRemoveFavorite(BaseModel request)
    {
        try
        {
            var favorite = await _dbContext.Favorites
                .SingleOrDefaultAsync(x => x.UserId == request.UserId && x.PropertyId == request.Id, request.CancellationToken);

            if (favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                await _dbContext.SaveChangesAsync(request.CancellationToken);

                return (success: true, message: "Property is removed from favorites.");
            }

            favorite = new Favorite
            {
                PropertyId = request.Id,
                UserId = request.UserId!.Value,
            };

            await _dbContext.Favorites.AddAsync(favorite, request.CancellationToken);
            await _dbContext.SaveChangesAsync(request.CancellationToken);

            return (success: true, message: "Property is added to favorites.");
        }
        catch (Exception)
        {
            return (success: true, message: "Some error has occured while processing.");
        }

    }
}
