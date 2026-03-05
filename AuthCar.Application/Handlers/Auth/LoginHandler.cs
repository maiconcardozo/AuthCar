using AuthCar.Application.Commands.Auth;
using AuthCar.Application.DTOs.Auth;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Domain.Interfaces;
using Foundation.Domain.Interfaces.Shared;
using Foundation.Shared.Helpers;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace AuthCar.Application.Handlers.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtSettings jwtSettings;

        public LoginHandler(
            IUnitOfWork unitOfWork,
            IJwtSettings jwtSettings)
        {
            this.unitOfWork = unitOfWork;
            this.jwtSettings = jwtSettings;
        }

        public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var usauario = await unitOfWork.UsuarioRepository.GetByLoginAsync(request.Login).ConfigureAwait(false);

            if (usauario == null)
            {
                throw new InvalidOperationException("Account not found");
            }

            if (!StringHelper.VerifyArgon2Hash(request.Senha, usauario.Senha))
            {
                throw new UnauthorizedAccessException("Invalid password");
            }

            //Caso for usar Redis para cachear o token, descomente a parte abaixo. Lembre-se de que o token deve ser invalidado no Redis quando o usuário fizer logout ou quando o token expirar.
            //var redisKey = $"auth:token:{existingAccount.Login}";
            //var cachedToken = await redisService.GetAsync<AuthResponseDTO>(redisKey).ConfigureAwait(false);

            //if (cachedToken != null && (cachedToken.ExpiraEm > DateTime.Now))
            //{
            //    cachedToken.TempoExpiracao = (int)(cachedToken.ExpiraEm - DateTime.Now).TotalSeconds;
            //    return cachedToken;
            //}

            //Parte dos Claims foi comentada para simplificar o exemplo, mas em um cenário real, você pode querer incluir as permissões do usuário no token JWT.
            //    var accountClaimActions = unitOfWork.AccountClaimActionRepository
            //        .GetByIdAccount(existingAccount.Id)
            //        .ToList();

            //    var claims = new List<System.Security.Claims.Claim>
            //{
            //    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, existingAccount.UserName),
            //};

            //    claims.AddRange(accountClaimActions.Select(aca =>
            //        new System.Security.Claims.Claim("permission", $"{aca.ClaimAction.Claim.Value}:{aca.ClaimAction.Action.Name}")));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.Now.AddSeconds(jwtSettings.ExpirationSeconds);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                //claims: claims,
                expires: expiresAt,
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var remainingSeconds = (int)(expiresAt - DateTime.Now).TotalSeconds;

            var tokenDto = new AuthResponseDTO
            {
                Token = tokenString,
                TokenType = "Bearer",
                TempoExpiracao = remainingSeconds,
                Login = usauario.Login,
                CriadoEm = DateTime.Now,
                ExpiraEm = expiresAt,
            };

            //await redisService.SetAsync(
            //    $"auth:token:{usauario.Login}",
            //    tokenDto,
            //    TimeSpan.FromSeconds(remainingSeconds)).ConfigureAwait(false);

            return tokenDto;
        }
    }
}
