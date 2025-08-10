using RealEstate.Shared.Enums;

namespace RealEstate.Shared.Models.Register;
public class RegisterRequestDto
{
    public string Email { get; set; }

    public string Password { get; set; }

    public UserRole Role { get; set; }
}
