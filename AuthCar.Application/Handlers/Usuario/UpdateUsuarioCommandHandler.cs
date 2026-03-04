using AuthCar.Application.Commands.Usuario;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using Authentication.Application.Mappers;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, UsuarioResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateUsuarioCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponseDTO> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetByIdAsync(request.Id);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado.");

            usuario.Nome = request.Nome;
            usuario.Login = request.Login;
            usuario.Senha = request.Senha;

            await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
            await _unitOfWork.CommitAsync();

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}