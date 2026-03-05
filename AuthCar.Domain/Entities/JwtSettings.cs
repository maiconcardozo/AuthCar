using AuthCar.Domain.Interfaces;

namespace AuthCar.Domain.Entities
{
    public class JwtSettings : IJwtSettings
    {
        public required string Issuer { get; set; }

        public required string Audience { get; set; }

        public required string SecretKey { get; set; }

        public int ExpirationSeconds { get; set; } = 3600;
    }
}
