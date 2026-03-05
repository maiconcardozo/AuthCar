using AuthCar.Application.Commands.Usuario;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using AuthCar.Application.Mappers;
using Foundation.Shared.Helpers;
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
            var usuario = await _unitOfWork.UsuarioRepository.GetByCodigoAsync(request.Codigo);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado.");

            usuario.Nome = request.Nome;
            usuario.Login = request.Login;
            usuario.Senha = StringHelper.ComputeArgon2Hash(request.Senha);

            await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    await _unitOfWork.UsuarioRepository.UpdateAsync(usuario);
            //});

            return AuthLoginProfileMapperInitializer.Mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}