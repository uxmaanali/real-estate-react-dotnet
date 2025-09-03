namespace RealEstate.Shared.Models.Login;
public record LoginResponseModel
{
    public int UserId { get; set; }
    public string Token { get; set; }
}
