using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities.Context;
using RealEstate.Services.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services;
public class PropertiesService : IScopedDependency
{
    private readonly RealEstateContext _dbContext;

    public PropertiesService(RealEstateContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PropertyDto>> GetPropertiesAsync(PropertyFilters filters, int? userId)
    {
        var query = _dbContext.Properties.AsQueryable();

        if (!string.IsNullOrEmpty(filters.Title))
            query = query.Where(p => p.Title.Contains(filters.Title));

        if (filters.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filters.MinPrice);

        if (filters.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filters.MaxPrice);

        if (filters.Bedrooms.HasValue)
            query = query.Where(p => p.Bedrooms == filters.Bedrooms);

        if (filters.Bathrooms.HasValue)
            query = query.Where(p => p.Bathrooms == filters.Bathrooms);

        if (filters.Carspots.HasValue)
            query = query.Where(p => p.Carspots == filters.Carspots);

        if (filters.ListingType.HasValue)
            query = query.Where(p => p.ListingType == filters.ListingType);

        return await query
            .Select(p => new PropertyDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Address = p.Address,
                Bathrooms = p.Bathrooms,
                Bedrooms = p.Bathrooms,
                Carspots = p.Carspots,
                Description = p.Description,
                Images = p.Images,
                ListingType = p.ListingType.ToString(),
                IsFavorite = userId.HasValue ? p.Favorites.Any(x => x.UserId == userId) : false,
            })
            .ToListAsync();
    }

    public async Task<PropertyDto?> GetProperty(int id, int? userId)
    {
        var property = await _dbContext.Properties
            .Select(p => new PropertyDto
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Address = p.Address,
                Bathrooms = p.Bathrooms,
                Bedrooms = p.Bathrooms,
                Carspots = p.Carspots,
                Description = p.Description,
                Images = p.Images,
                ListingType = p.ListingType.ToString(),
                IsFavorite = userId.HasValue ? p.Favorites.Any(x => x.UserId == userId) : false,
            })
            .FirstOrDefaultAsync(x => x.Id == id);

        return property;
    }
}
