using AuthCar.Application.Commands.Veiculo;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using Authentication.Application.Mappers;
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
            var veiculo = new Veiculo
            {
                Descricao = request.Descricao,
                Marca = request.Marca,
                Modelo = request.Modelo,
                Valor = request.Valor
            };

            await _unitOfWork.VeiculoRepository.AddAsync(veiculo);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    await _unitOfWork.VeiculoRepository.AddAsync(veiculo);
            //});

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<VeiculoResponseDTO>(veiculo);
        }
    }
}