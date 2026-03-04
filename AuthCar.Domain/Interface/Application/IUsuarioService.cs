using AuthCar.Domain.Entities;

namespace AuthCar.Domain.Interface.Application
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<IEnumerable<Usuario>> ListAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Guid id);
    }
}

