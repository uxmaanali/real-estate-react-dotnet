using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities.Context;
using RealEstate.Services.Cache;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

namespace RealEstate.Services.Properties;
public class PropertiesService : IPropertiesService, IScopedDependency
{
    private readonly RealEstateContext _dbContext;
    private readonly ICacheService _cacheService;

    public PropertiesService(RealEstateContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<List<PropertyDto>> GetPropertiesAsync(PropertyFilters filters, int? userId)
    {
        const string propertiesKey = "properties";

        var cacheProperties = await _cacheService.GetAsync<List<PropertyDto>>(propertiesKey);
        if (cacheProperties != null && cacheProperties.Count > 0)
        {
            return cacheProperties;
        }

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

        var properties = await query
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

        await _cacheService.SetAsync(propertiesKey, properties);

        return properties;
    }

    public async Task<PropertyDto?> GetProperty(int id, int? userId)
    {
        var propertiesKey = $"property-{id}";

        var cacheProperty = await _cacheService.GetAsync<PropertyDto>(propertiesKey);
        if (cacheProperty != null)
        {
            return cacheProperty;
        }

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

        await _cacheService.SetAsync(propertiesKey, property);

        return property;
    }
}
