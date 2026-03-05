using AuthCar.Application.Commands.Usuario;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUsuarioCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _unitOfWork.UsuarioRepository.GetByCodigoAsync(request.Codigo).ConfigureAwait(false);
            if (usuario == null)
                throw new NotFoundException("Usuário não encontrado.");

            _unitOfWork.UsuarioRepository.Remove(usuario);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    _unitOfWork.UsuarioRepository.Remove(usuario);
            //});

            return Unit.Value;
        }
    }
}