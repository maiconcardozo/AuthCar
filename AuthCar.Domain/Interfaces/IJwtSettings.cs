using System;
using System.Collections.Generic;
using System.Text;

namespace AuthCar.Domain.Interfaces
{
    public interface IJwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpirationSeconds { get; set; }
    }
}
