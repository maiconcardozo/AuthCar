using AuthCar.Domain.Entities;

namespace AuthCar.Domain.Interface.Application
{
    public interface IUsuarioService
    {
        Task<Usuario?> GetByCodigoAsync(Guid codigo);
        Task<IEnumerable<Usuario>> ListAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Guid Codigo);
    }
}

