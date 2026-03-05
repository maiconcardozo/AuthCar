using AuthCar.Application.Commands.Veiculo;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class AddVeiculoCommandHandler : IRequestHandler<AddVeiculoCommand, VeiculoResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddVeiculoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VeiculoResponseDTO> Handle(AddVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = new Veiculo(
                   request.Descricao,
                   request.Marca,
                   request.Modelo,
                   request.Valor);

            await _unitOfWork.VeiculoRepository.AddAsync(veiculo);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    await _unitOfWork.VeiculoRepository.AddAsync(veiculo);
            //});

            return AuthLoginProfileMapperInitializer.Mapper.Map<VeiculoResponseDTO>(veiculo);
        }
    }
}