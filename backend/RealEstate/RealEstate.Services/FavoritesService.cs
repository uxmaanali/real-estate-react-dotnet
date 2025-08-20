using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services;

public class FavoritesService : IScopedDependency
{
    private readonly RealEstateContext _dbContext;

    public FavoritesService(RealEstateContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PropertyDto>> GetFavorites(int userId)
    {
        var properties = await _dbContext.Favorites
            .Where(x => x.UserId == userId)
            .Select(x => new PropertyDto
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
            .ToListAsync();

        return properties;
    }

    public async Task<(bool success, string message)> AddRemoveFavorite(int userId, int propertyId)
    {
        try
        {
            var favorite = await _dbContext.Favorites
            //.AsTracking()
            .SingleOrDefaultAsync(x => x.UserId == userId && x.PropertyId == propertyId);

            if (favorite != null)
            {
                _dbContext.Favorites.Remove(favorite);
                await _dbContext.SaveChangesAsync();

                return (success: true, message: "Property is removed from favorites.");
            }

            favorite = new Favorite
            {
                PropertyId = propertyId,
                UserId = userId,
            };

            await _dbContext.Favorites.AddAsync(favorite);
            await _dbContext.SaveChangesAsync();

            return (success: true, message: "Property is added to favorites.");
        }
        catch (Exception)
        {
            return (success: true, message: "Some error has occured while processing.");
        }

    }
}
