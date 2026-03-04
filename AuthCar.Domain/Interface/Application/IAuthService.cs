using System;
using System.Collections.Generic;
using System.Text;

namespace AuthCar.Domain.Interface.Application
{
    public interface IAuthService
    {
        Task<string?> GenerateJwtAsync(string login, string senha);
    }
}
