namespace RealEstate.Messaging.Models;

public record SendWelcomeEmail
{
    public int UserId { get; set; }

    public string Email { get; set; } = string.Empty;
}
