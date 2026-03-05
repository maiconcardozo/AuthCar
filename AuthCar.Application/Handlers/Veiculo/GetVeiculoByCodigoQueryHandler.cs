using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class GetVeiculoByCodigoQueryHandler : IRequestHandler<GetVeiculoByCodigoQuery, VeiculoResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVeiculoByCodigoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VeiculoResponseDTO> Handle(GetVeiculoByCodigoQuery request, CancellationToken cancellationToken)
        {
            var veiculo = await _unitOfWork.VeiculoRepository.GetByCodigoAsync(request.Codigo);
            if (veiculo == null)
                throw new NotFoundException("Veículo não encontrado.");

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<VeiculoResponseDTO>(veiculo);
        }
    }
}