using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using MediatR;
using System;

namespace AuthCar.Application.Queries
{
    public class GetUsuarioByCodigoQuery : IRequest<UsuarioResponseDTO>
    {
        public Guid Codigo { get; set; }
    }
}