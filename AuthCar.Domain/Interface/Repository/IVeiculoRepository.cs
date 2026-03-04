using AuthCar.Domain.Entities;
using Foundation.Domain.Interfaces.Repositories;

namespace AuthCar.Domain.Interface.Repository
{
    public interface IVeiculoRepository : IEntityRepository<Veiculo>
    {
        Task<Veiculo?> GetByIdAsync(Guid id);
        Task<IEnumerable<Veiculo>> ListAsync();
        Task AddAsync(Veiculo veiculo);
        Task UpdateAsync(Veiculo veiculo);
        Task DeleteAsync(Guid id);
    }
}
