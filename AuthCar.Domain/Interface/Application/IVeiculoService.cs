using AuthCar.Domain.Entities;

namespace AuthCar.Domain.Interface.Application
{
    public interface IVeiculoService
    {
        Task<Veiculo?> GetByIdAsync(Guid id);
        Task<IEnumerable<Veiculo>> ListAsync();
        Task AddAsync(Veiculo veiculo);
        Task UpdateAsync(Veiculo veiculo);
        Task DeleteAsync(Guid id);
    }
}
