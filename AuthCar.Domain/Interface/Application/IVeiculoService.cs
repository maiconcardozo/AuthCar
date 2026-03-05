using AuthCar.Domain.Entities;

namespace AuthCar.Domain.Interface.Application
{
    public interface IVeiculoService
    {
        Task<Veiculo?> GetByCodigoAsync(Guid codigo);
        Task<IEnumerable<Veiculo>> ListAsync();
        Task AddAsync(Veiculo veiculo);
        Task UpdateAsync(Veiculo veiculo);
        Task DeleteAsync(Guid codigo);
    }
}
