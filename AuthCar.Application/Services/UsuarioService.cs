using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Application;
using AuthCar.Domain.Interface.Repository;

namespace AuthCar.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
        {
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Usuario?> GetByIdAsync(Guid id) => _usuarioRepository.GetByIdAsync(id);

        public Task<IEnumerable<Usuario>> ListAsync() => _usuarioRepository.ListAsync();

        public async Task AddAsync(Usuario usuario)
        {
            await _usuarioRepository.AddAsync(usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Usuario usuario)
        {
            await _usuarioRepository.UpdateAsync(usuario);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _usuarioRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}