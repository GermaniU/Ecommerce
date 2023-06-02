
namespace Ecommerce.Model.Token.Application;

public class JwtSettings{
    public string? key { get; set; }
    public TimeSpan ExpireTime { get; set; }
    public double DurationMinutes { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
}