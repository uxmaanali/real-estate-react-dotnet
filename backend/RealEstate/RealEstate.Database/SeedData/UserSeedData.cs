using Microsoft.Extensions.DependencyInjection;

using RealEstate.Database.Entities;
using RealEstate.Database.Entities.Context;
using RealEstate.Shared.Constants;
using RealEstate.Shared.Enums;
using RealEstate.Shared.Utils;

namespace RealEstate.Database.SeedData;
internal static class UserSeedData
{
    internal static async Task SeedUsers(this IServiceProvider services, IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<RealEstateContext>();

        var buyerUserExist = dbContext.Users.Any(x => x.Email == UserData.BuyerUser && x.Role == UserRole.Buyer);
        if (!buyerUserExist)
        {
            var salt = PasswordHasher.GeneratePasswordSalt();
            var passwordHash = PasswordHasher.HashPassword("Abcd!234", salt);

            var user = new User
            {
                IsActive = true,
                Email = UserData.BuyerUser,
                Role = UserRole.Buyer,
                PasswordHash = passwordHash,
                Salt = salt
            };
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }
    }
}
