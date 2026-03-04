using AuthCar.Domain.Interface.Application;
using AuthCar.Domain.Interface.Repository;

namespace AuthCar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<string?> GenerateJwtAsync(string login, string senha)
        {
            // Implementação da autenticação deve verificar login/senha,
            // mas normalmente não chama CommitAsync, pois não há persistência.
            // Se gerar refreshToken, etc., aí sim:
            // await _unitOfWork.CommitAsync();
            return null; // TODO: implementar JWT.
        }
    }
}