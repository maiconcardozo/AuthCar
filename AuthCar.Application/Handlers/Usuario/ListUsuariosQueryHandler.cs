using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class ListUsuariosQueryHandler : IRequestHandler<ListUsuariosQuery, IEnumerable<UsuarioResponseDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListUsuariosQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UsuarioResponseDTO>> Handle(ListUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _unitOfWork.UsuarioRepository.ListAsync();
            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<IEnumerable<UsuarioResponseDTO>>(usuarios);
        }
    }
}