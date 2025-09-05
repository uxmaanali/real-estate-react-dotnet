namespace RealEstate.Shared.OptionsConfig.RabbitMq;

using System.ComponentModel.DataAnnotations;

public record RabbitMqOptions
{
    [Required]
    public string VirtualHost { get; set; } = string.Empty;

    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
