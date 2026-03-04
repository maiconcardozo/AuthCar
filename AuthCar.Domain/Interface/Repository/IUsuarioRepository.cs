using AuthCar.Domain.Entities;
using Foundation.Domain.Interfaces.Repositories;

namespace AuthCar.Domain.Interface.Repository
{
    public interface IUsuarioRepository : IEntityRepository<Usuario>
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<Usuario?> GetByLoginAsync(string login);
        Task<IEnumerable<Usuario>> ListAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Guid id);
    }
}
