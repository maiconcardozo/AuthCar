using AuthCar.Application.DTOs;
using AuthCar.Domain.Entities;
using MediatR;
using System;

namespace AuthCar.Application.Queries
{
    public class GetUsuarioByIdQuery : IRequest<UsuarioResponseDTO>
    {
        public Guid Id { get; set; }
    }
}