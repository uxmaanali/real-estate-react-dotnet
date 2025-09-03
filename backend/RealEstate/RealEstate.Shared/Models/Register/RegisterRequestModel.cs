using RealEstate.Shared.Abstraction;
using RealEstate.Shared.Enums;

namespace RealEstate.Shared.Models.Register;
public record RegisterRequestModel : BaseModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }
}
