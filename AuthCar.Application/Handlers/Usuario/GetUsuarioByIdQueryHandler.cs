using AuthCar.Application.DTOs;
using AuthCar.Application.Queries;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUsuarioByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponseDTO> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.Id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado.");

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}