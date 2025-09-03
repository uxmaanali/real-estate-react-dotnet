namespace RealEstate.Shared.Models.Login;
using RealEstate.Shared.Abstraction;

public record LoginRequestModel : BaseModel
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}
