using AuthCar.Application.Commands.Usuario;
using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using AuthCar.Domain.Interface.Repository;
using Authentication.Application.Mappers;
using Authentication.Shared.Exceptions;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class AddUsuarioCommandHandler : IRequestHandler<AddUsuarioCommand, UsuarioResponseDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddUsuarioCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UsuarioResponseDTO> Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
        {
            var existing = await _unitOfWork.UsuarioRepository.GetByLoginAsync(request.Login);
            if (existing != null)
                throw new ConflictException("Este login de usuário já existe.");

            var usuario = new Usuario
            {
                Nome = request.Nome,
                Login = request.Login,
                Senha = request.Senha
            };

            await _unitOfWork.UsuarioRepository.AddAsync(usuario);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    await _unitOfWork.UsuarioRepository.AddAsync(usuario);
            //});

            return AuthenticationLoginProfileMapperInitializer.Mapper.Map<UsuarioResponseDTO>(usuario);
        }
    }
}