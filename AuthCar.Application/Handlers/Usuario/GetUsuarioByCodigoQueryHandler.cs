using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class GetUsuarioByCodigoQueryHandler : IRequestHandler<GetUsuarioByCodigoQuery, UsuarioResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsuarioByCodigoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponseDTO> Handle(GetUsuarioByCodigoQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetByCodigoAsync(request.Codigo);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado.");

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}