using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class ListVeiculosQueryHandler : IRequestHandler<ListVeiculosQuery, IEnumerable<VeiculoResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListVeiculosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<VeiculoResponseDTO>> Handle(ListVeiculosQuery request, CancellationToken cancellationToken)
        {
            var veiculos = await _unitOfWork.VeiculoRepository.ListAsync();
            return AuthLoginProfileMapperInitializer.Mapper.Map<IEnumerable<VeiculoResponseDTO>>(veiculos);
        }
    }
}