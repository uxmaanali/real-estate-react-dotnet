namespace RealEstate.Services.Properties;

using Microsoft.EntityFrameworkCore;

using RealEstate.Database.Entities.Context;
using RealEstate.Services.Cache;
using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Models.Properties;

public class PropertiesService : IPropertiesService, IScopedDependency, IService
{
    private readonly RealEstateContext _dbContext;
    private readonly ICacheService _cacheService;

    public PropertiesService(RealEstateContext dbContext, ICacheService cacheService)
    {
        _dbContext = dbContext;
        _cacheService = cacheService;
    }

    public async Task<List<PropertyModel>> GetPropertiesAsync(PropertyFiltersRequestModel filters)
    {
        const string propertiesKey = "properties";

        var cacheProperties = await _cacheService.GetAsync<List<PropertyModel>>(propertiesKey);
        if (cacheProperties != null && cacheProperties.Count > 0)
        {
            return cacheProperties;
        }

        var userId = filters.UserId;

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
            .Select(p => new PropertyModel
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
            .ToListAsync(filters.CancellationToken);

        await _cacheService.SetAsync(propertiesKey, properties);

        return properties;
    }

    public async Task<PropertyModel?> GetProperty(BaseModel request)
    {
        var propertiesKey = $"property-{request.Id}";

        var cacheProperty = await _cacheService.GetAsync<PropertyModel>(propertiesKey);
        if (cacheProperty != null)
        {
            return cacheProperty;
        }

        var property = await _dbContext.Properties
            .Select(p => new PropertyModel
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
                IsFavorite = request.UserId.HasValue ? p.Favorites.Any(x => x.UserId == request.UserId) : false,
            })
            .FirstOrDefaultAsync(x => x.Id == request.Id, request.CancellationToken);

        await _cacheService.SetAsync(propertiesKey, property);

        return property;
    }
}
