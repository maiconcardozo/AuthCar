using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class GetVeiculoByIdQueryHandler : IRequestHandler<GetVeiculoByIdQuery, VeiculoResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetVeiculoByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<VeiculoResponseDTO> Handle(GetVeiculoByIdQuery request, CancellationToken cancellationToken)
        {
            var veiculo = await _unitOfWork.VeiculoRepository.GetByIdAsync(request.Id);
            if (veiculo == null)
                throw new NotFoundException("Veículo não encontrado.");

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<VeiculoResponseDTO>(veiculo);
        }
    }
}