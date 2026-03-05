using System;
using MediatR;

namespace AuthCar.Application.Commands.Usuario
{
    public class DeleteUsuarioCommand : IRequest<Unit>
    {
        public Guid Codigo { get; set; }
    }
}