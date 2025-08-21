using RealEstate.Database.Entities;

namespace RealEstate.Services.Jwt;
public interface IJwtService
{
    string GenerateJWTToken(User user);
}
