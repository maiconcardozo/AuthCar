using System;
using MediatR;

namespace AuthCar.Application.Commands.Veiculo
{
    public class DeleteVeiculoCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}