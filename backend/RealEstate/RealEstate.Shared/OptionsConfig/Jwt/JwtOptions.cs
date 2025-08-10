using System.ComponentModel.DataAnnotations;

namespace RealEstate.Shared.OptionsConfig.Jwt;
public class JwtOptions
{
    [Required]
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    public int ExpiryInMinutes { get; set; } = 60;
}
