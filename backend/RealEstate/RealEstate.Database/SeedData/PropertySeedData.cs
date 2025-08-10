using Microsoft.Extensions.DependencyInjection;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.Shared.Enums;

namespace RealEstate.Database.SeedData;
internal static class PropertySeedData
{
    internal static async Task SeedProperies(this IServiceProvider services, IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<RealEstateContext>();

        var exist = dbContext.Properties.Any(x => x.IsActive);
        if (!exist)
        {
            var properties = new List<Property>
            {
                GetProperty("Property 1", 500, ListingType.Sale),
                GetProperty("Property 2", 100, ListingType.Rent),
                GetProperty("Property 3", 700, ListingType.Sale),
                GetProperty("Property 4", 50, ListingType.Rent),
                GetProperty("Property 5", 900, ListingType.Sale),
            };

            await dbContext.Properties.AddRangeAsync(properties);
            await dbContext.SaveChangesAsync();
        }
    }

    private static Property GetProperty(string title, float price, ListingType listingType)
    {
        return new Property
        {
            Title = title,
            Price = price,
            ListingType = listingType,
            Bedrooms = Random.Shared.Next(1, 5),
            Bathrooms = Random.Shared.Next(1, 5),
            Carspots = Random.Shared.Next(1, 3),
            Description = "What is Lorem Ipsum?\r\nLorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
            Images = ["https://dummyimage.com/300/09f.png/fff, https://dummyimage.com/300/09f.png/fff"]
        };
    }
}
