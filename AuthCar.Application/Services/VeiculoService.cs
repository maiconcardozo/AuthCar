using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Application;
using AuthCar.Domain.Interface.Repository;

namespace AuthCar.Application.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _veiculoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VeiculoService(IVeiculoRepository veiculoRepository, IUnitOfWork unitOfWork)
        {
            _veiculoRepository = veiculoRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Veiculo?> GetByCodigoAsync(Guid codigo) => _veiculoRepository.GetByCodigoAsync(codigo);

        public Task<IEnumerable<Veiculo>> ListAsync() => _veiculoRepository.ListAsync();

        public async Task AddAsync(Veiculo veiculo)
        {
            await _veiculoRepository.AddAsync(veiculo);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(Veiculo veiculo)
        {
            await _veiculoRepository.UpdateAsync(veiculo);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _veiculoRepository.DeleteAsync(id);
            await _unitOfWork.CommitAsync();
        }
    }
}