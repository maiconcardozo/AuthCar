using AuthCar.Application.DTOs;
using MediatR;

namespace AuthCar.Application.Queries
{
    public class GetVeiculoByIdQuery : IRequest<VeiculoResponseDTO?>
    {
        public Guid Id { get; set; }
    }
}