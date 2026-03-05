using AuthCar.Application.Commands.Veiculo;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class UpdateVeiculoCommandHandler : IRequestHandler<UpdateVeiculoCommand, VeiculoResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVeiculoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VeiculoResponseDTO> Handle(UpdateVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = await _unitOfWork.VeiculoRepository.GetByCodigoAsync(request.Codigo);
            if (veiculo == null)
                throw new NotFoundException("Veículo não encontrado.");

            veiculo.Descricao = request.Descricao;
            veiculo.Marca = request.Marca;
            veiculo.Modelo = request.Modelo;
            veiculo.Valor = request.Valor;

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                await _unitOfWork.VeiculoRepository.UpdateAsync(veiculo);
            });

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<VeiculoResponseDTO>(veiculo);
        }
    }
}