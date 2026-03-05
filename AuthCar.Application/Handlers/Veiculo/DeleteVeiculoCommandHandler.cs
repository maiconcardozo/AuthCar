using AuthCar.Application.Commands.Veiculo;
using AuthCar.Domain.Interface.Repository;
using AuthCar.Shared.Exceptions;
using MediatR;

namespace AuthCar.Application.Handlers
{
    public class DeleteVeiculoCommandHandler : IRequestHandler<DeleteVeiculoCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteVeiculoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteVeiculoCommand request, CancellationToken cancellationToken)
        {
            var veiculo = await _unitOfWork.VeiculoRepository.GetByCodigoAsync(request.Codigo);
            if (veiculo == null)
                throw new NotFoundException("Veículo não encontrado.");

            _unitOfWork.VeiculoRepository.Remove(veiculo);
            await _unitOfWork.CommitAsync();

            // Alternativamente, se o repositório suportar transações, você pode usar:
            //await _unitOfWork.ExecuteInTransactionAsync(async () =>
            //{
            //    _unitOfWork.VeiculoRepository.Remove(veiculo);
            //});

            return Unit.Value;
        }
    }
}